using Microsoft.Extensions.Logging;
using Payment_System.Domain.DbContexts;
using Payment_System.Domain.Dtos;
using Payment_System.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Payment_System.Domain.Repositories
{
    public class PaymentRepository : IPaymentInterface
    {
        private readonly ILogger<PaymentRepository> _logger;
        private readonly PaymentDbContext _context;

        public PaymentRepository(PaymentDbContext context, ILogger<PaymentRepository> logger)
        {
            if (logger is null)
                throw new ArgumentNullException(nameof(logger));
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            _context = context;
        }

        public int ChoosePaymentHandler(Payment_Dto payment_dto)
        {
            switch (payment_dto.amount)
            {
                case decimal amount when payment_dto.amount <= 20:
                    Console.WriteLine("current amount", amount);
                    return ICheapPaymentGateway(payment_dto, 0);
                case decimal amount when payment_dto.amount > 20 && payment_dto.amount < 500:
                    return IExpensivePaymentGateway(payment_dto, 0);
                default:
                    return PremiumPaymentService(payment_dto, 0);
            }
        }

        public int ICheapPaymentGateway(Payment_Dto payment_model, int retry)
        {
            return 1;
        }

        public int IExpensivePaymentGateway(Payment_Dto payment_model, int retry)
        {
            return 2;
        }

        public int PremiumPaymentService(Payment_Dto payment_model, int retry)
        {
            return 3;
        }

        private void Wait()
        {
            Thread.Sleep(TimeSpan.FromSeconds(10));
        }

        public (bool,KeyValuePair<string, string>?) validateExpiryDate(string expiryDate)
        {
            var validDate = DateTime.TryParse(expiryDate, out DateTime parsedDate);
            if (!validDate)
                return (false, new KeyValuePair<string, string>("expiry_date", "invalid expiry date"));

            bool isPresentOrFutureDate = parsedDate > DateTime.UtcNow;
            if (!isPresentOrFutureDate)
                return (false, new KeyValuePair<string, string>("expiry_date", "expiry date must be greater than today"));

            return (true, null);

        }

        public bool ValidateCreditCard(string cardnumber)
        {
            int sum = 0;
            for (var i = cardnumber.Length - 1; i >= 0; i--)
            {
                if (i % 2 == 0)
                {
                    sum += doubleNumber(int.Parse(cardnumber[i].ToString()));
                }
                else
                {
                    sum += int.Parse(cardnumber[i].ToString());
                }
            }
            return (sum % 10) == 0 ? true : false;
        }
        private int doubleNumber(int number)
        {
            var square = number * 2;
            if (square > 9)
            {
                var squareString = square.ToString().ToCharArray();
                square = int.Parse(squareString[0].ToString()) + int.Parse(squareString[1].ToString());
            }
            return square;
        }

        public (bool, List<KeyValuePair<string, string>>) ValidateModel(Payment_Dto model)
        {
            bool isValid = true;
            List<KeyValuePair<string, string>> errors = new List<KeyValuePair<string, string>>();
            bool validNumber = ValidateCreditCard(model.card_number);
            if (!validNumber)
            {
                isValid = false;
                errors.Add(new KeyValuePair<string, string>("card_number", "invalid credit card"));
            }
            var validExpiryDate = validateExpiryDate(model.expiry_date);
            if (!validExpiryDate.Item1)
            {
                isValid = validExpiryDate.Item1;
                errors.Add(validExpiryDate.Item2.Value);
            }
            return (isValid, errors);
        }
    }
}
