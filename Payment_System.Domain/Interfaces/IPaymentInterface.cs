using Payment_System.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment_System.Domain.Interfaces
{
    public interface IPaymentInterface
    {
        int IExpensivePaymentGateway(Payment_Dto payment_model, int retry);
        int ICheapPaymentGateway(Payment_Dto payment_model, int retry);
        int PremiumPaymentService(Payment_Dto payment_model, int retry);
        int ChoosePaymentHandler(Payment_Dto payment_dto);
        bool ValidateCreditCard(string cardnumber);
        (bool, List<KeyValuePair<string, string>>) ValidateModel(Payment_Dto model);
        (bool, KeyValuePair<string, string>?) validateExpiryDate(string expiryDate);
    }
}
