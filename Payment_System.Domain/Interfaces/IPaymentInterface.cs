using Payment_System.Domain.Dtos;
using Payment_System.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Payment_System.Domain.Interfaces
{
    public interface IPaymentInterface
    {
        Task<PaymentResponse> IExpensivePaymentGateway(Payment payment_model, bool isPremium, int retry);
        Task<PaymentResponse> ICheapPaymentGateway(Payment payment_model, int retry);
        Task<PaymentResponse> ChoosePaymentHandler(Payment_Dto payment_dto);
        Task<Payment> GetPaymentByIdAsync(Guid id);
        Task<PaymentState> GetPaymentStatusByStatus(int status);
    }
}
