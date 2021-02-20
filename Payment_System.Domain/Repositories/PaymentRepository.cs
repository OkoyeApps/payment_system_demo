using Microsoft.Extensions.Logging;
using Payment_System.Domain.Dtos;
using Payment_System.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment_System.Domain.Repositories
{
    public class PaymentRepository : IPaymentInterface
    {
        private readonly ILogger<PaymentRepository> _logger;
        public PaymentRepository(ILogger<PaymentRepository> logger)
        {
            if(logger is null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
        }
        public void ICheapPaymentGateway(Payment_Dto payment_model, int retry)
        {
            throw new NotImplementedException();
        }

        public void IExpensivePaymentGateway(Payment_Dto payment_model, int retry)
        {
            throw new NotImplementedException();
        }

        public void PremiumPaymentService(Payment_Dto payment_model, int retry)
        {
            throw new NotImplementedException();
        }
    }
}
