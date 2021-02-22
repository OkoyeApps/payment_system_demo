using Payment_System.Domain.Interfaces;

namespace Payment_System.Domain
{
    public class PaymentCounterRepository : IPaymentCounterInterface
    {
        public int TotalPayment { get; set; }
        public int PocessedExpensivePaymentCount { get; set; }
        public int ProcessCheapPaymentCount { get; set; }
    }
}
