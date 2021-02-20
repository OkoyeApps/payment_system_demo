using Payment_System.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment_System.Domain.Interfaces
{
    public interface IPaymentInterface
    {
        void IExpensivePaymentGateway(Payment_Dto payment_model, int retry);
        void ICheapPaymentGateway(Payment_Dto payment_model, int retry);
        void PremiumPaymentService(Payment_Dto payment_model, int retry);
    }
}
