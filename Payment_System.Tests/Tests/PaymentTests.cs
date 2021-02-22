using Moq;
using Payment_System.Domain.Dtos;
using Payment_System.Domain.Entities;
using Payment_System.Domain.Interfaces;
using Payment_System.Domain.Interfaces.Generic;
using Payment_System.Domain.Repositories;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Payment_System.Tests.Tests
{
    public class PaymentTests
    {
        private readonly Mock<ILoggerInterface> _loggerMock = new Mock<ILoggerInterface>();
        private readonly Mock<IGenericRepository<Payment>> _paymentContext = new Mock<IGenericRepository<Payment>>();
        private readonly Mock<IGenericRepository<PaymentState>> _paymentStatusContext = new Mock<IGenericRepository<PaymentState>>();
        private readonly PaymentRepository paymentRepository;

        public PaymentTests()
        {
            paymentRepository = new PaymentRepository(_paymentContext.Object, _paymentStatusContext.Object, _loggerMock.Object, null, null, null);
        }

        [Fact]
        public async Task GetPaymentStatusByStatus_should_return_an_object()
        {
            int id = 1;
            Guid expectedId = Guid.NewGuid();

          var result =   _paymentStatusContext.Setup(x => x.GetByParam(x => x.Status == (PaymentStatusEnum)id))
                .ReturnsAsync(new PaymentState
                {
                    Id = expectedId,
                    Status = PaymentStatusEnum.PENDING,
                    Name = Enum.GetName(typeof(PaymentStatusEnum), id)
                }) ;

            var paymentStatus = await paymentRepository.GetPaymentStatusByStatus(id);

            Assert.Equal(expectedId, paymentStatus.Id);
            Assert.Equal(id, (int)paymentStatus.Status);
        }

        [Fact]
        public async Task GetPaymentStatusByStatus_should_return_null_object()
        {
            int id = 1;
            Guid expectedId = Guid.NewGuid();

            var result = _paymentStatusContext.Setup(x => x.GetByParam(x => x.Status == (PaymentStatusEnum)id))
                  .ReturnsAsync(() => null);

            var paymentStatus = await paymentRepository.GetPaymentStatusByStatus(id);

            Assert.Null(paymentStatus);
        }
    }
}
