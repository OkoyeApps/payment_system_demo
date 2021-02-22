using Payment_System.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment_System.Domain.Validators
{
    public class PaymentModelValidators
    {

        public PaymentModelValidators()
        {

        }
        public (bool, KeyValuePair<string, string>?) validateExpiryDate(string expiryDate, DateTime currentDate)
        {
            var validDate = DateTime.TryParse(expiryDate, out DateTime parsedDate);
            if (!validDate)
                return (false, new KeyValuePair<string, string>("expiry_date", "invalid expiry date"));

            bool isPresentOrFutureDate = parsedDate > currentDate;
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
            var validExpiryDate = validateExpiryDate(model.expiry_date, DateTime.UtcNow);
            if (!validExpiryDate.Item1)
            {
                isValid = validExpiryDate.Item1;
                errors.Add(validExpiryDate.Item2.Value);
            }
            return (isValid, errors);
        }
    }
}
