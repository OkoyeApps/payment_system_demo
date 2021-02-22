using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Payment_System.Domain.DbContexts;
using Payment_System.Domain.Dtos;
using Payment_System.Domain.Entities;
using Payment_System.Domain.Interfaces;
using Payment_System.Domain.Interfaces.Generic;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Payment_System.Domain.Repositories
{
    public class PaymentRepository : IPaymentInterface
    {
        private readonly ILoggerInterface _logger;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Payment> _paymentContext;
        private readonly IGenericRepository<PaymentState> _paymentStatusContext;
        private readonly IDbContextFactory<BackgroundDbContex> _contextFactory;
        private readonly IPaymentCounterInterface _counter;

        public PaymentRepository(
            IGenericRepository<Payment> paymentRepo,
            IGenericRepository<PaymentState> paymentStatusContext,
            ILoggerInterface logger, IMapper mapper,
            IDbContextFactory<BackgroundDbContex> factory,
            IPaymentCounterInterface counter
            )
        {
            if (logger is null)
                throw new ArgumentNullException(nameof(logger));

            _paymentContext = paymentRepo;
            _paymentStatusContext = paymentStatusContext;
            _logger = logger;
            _mapper = mapper;
            _contextFactory = factory;
            _counter = counter;
        }

        public async Task<PaymentResponse> ChoosePaymentHandler(Payment_Dto payment_dto)
        {
            var pendingState = await GetPaymentStatusByStatus((int)PaymentStatusEnum.PENDING);

            if (pendingState is null)
                throw new NullReferenceException(nameof(pendingState));

            payment_dto.Status = pendingState.Id;
            Payment PaymentModel = _mapper.Map<Payment_Dto, Payment>(payment_dto);

            switch (payment_dto.amount)
            {
                case decimal amount when payment_dto.amount <= 20:
                    _logger.LogInformation("Using ICheapPaymentGateway for processing payment");
                    return await ICheapPaymentGateway(PaymentModel, 0);
                case decimal amount when payment_dto.amount > 20 && payment_dto.amount < 500:
                    _logger.LogInformation("Using IExpensivePaymentGateway for processing payment");
                    return await IExpensivePaymentGateway(PaymentModel,false, 0);
                default:
                    _logger.LogInformation("Using PremiumPaymentService for processing payment");
                    return await IExpensivePaymentGateway(PaymentModel, true, 0);
            }
        }

        public async Task<PaymentState> GetPaymentStatusByStatus(int status)
        {
            return await _paymentStatusContext.GetByParam(x => x.Status == (PaymentStatusEnum)status);
        }

        public async Task<Payment> GetPaymentByIdAsync(Guid id) =>
            await _paymentContext.Get(x => x.Id == id).Include(x=>x.PaymentStatus).FirstOrDefaultAsync();



        public async Task<PaymentResponse> ICheapPaymentGateway(Payment payment_model, int retry)
        {
            try
            {
                _counter.ProcessCheapPaymentCount++;
                return await AddPayment(payment_model);
            }
            catch (Exception ex)
            {
                _logger.LogError("transaction failed", ex);
                return null;
            }
        }

        public async Task<PaymentResponse> IExpensivePaymentGateway(Payment payment_model, bool isPremium, int retry)
        {
            try
            {
                if (isPremium)
                {
                    if (_counter.PocessedExpensivePaymentCount % 3 == 0 && retry < 3)
                    {
                        //simulate failure for retry of expensive payment gateway
                        return await IExpensivePaymentGateway(payment_model, true, ++retry);
                    }
                    else
                    {
                        _counter.PocessedExpensivePaymentCount++;
                        return await AddPayment(payment_model);
                    }
                }
                else
                {
                    //simulate failure for expensive gateway not availble
                    if (_counter.PocessedExpensivePaymentCount % 2 == 0)
                    {
                        _logger.LogInformation("IExpensivePaymentGateway not availble, switching to ICheapPaymentGateway");
                        return await ICheapPaymentGateway(payment_model, 0);
                    }
                    _counter.PocessedExpensivePaymentCount++;
                    return await AddPayment(payment_model);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured in IExpensivePaymentGateway", ex);
                return null;
            }
        }


        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e, Guid Id)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                UpdatePaymentState(Id, context);
            }
        }

        private async Task<PaymentResponse> AddPayment(Payment payment_model)
        {
            await _paymentContext.AddAsync(payment_model);
            _logger.LogInformation("Processing payment {0}", payment_model.Id);

            var timer = new System.Timers.Timer { AutoReset = false, Interval = 30 * 1000, Enabled = true };
            timer.Elapsed += delegate (object sender, System.Timers.ElapsedEventArgs e)
            {

                this.Timer_Elapsed(sender, e, payment_model.Id);
            };

            return new PaymentResponse
            {
                Id = payment_model.Id,
                Status = PaymentStatusEnum.PENDING
            };
        }

        public void UpdatePaymentState(Guid paymentId, BackgroundDbContex context)
        {
            var payment = context.Payment.Find(paymentId);
            if (payment is null) return;

            var paymentstate = context.PaymentStatus.Where(x => x.Status == PaymentStatusEnum.PROCESSED).FirstOrDefault();
            if (paymentstate is null) return;

            payment.PaymentStatusId = paymentstate.Id;
            context.Payment.Update(payment);
            context.SaveChanges();
            _logger.LogInformation("payment successfully processed for {0}", paymentId);
        }
    }
}
