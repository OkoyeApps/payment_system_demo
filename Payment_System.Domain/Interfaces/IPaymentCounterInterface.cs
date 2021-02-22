using System;
using System.Collections.Generic;
using System.Text;

namespace Payment_System.Domain.Interfaces
{
    public interface IPaymentCounterInterface
    {
        int TotalPayment { get; set; }
        int PocessedExpensivePaymentCount { get; set; }
        int ProcessCheapPaymentCount { get; set; }
    }
}
