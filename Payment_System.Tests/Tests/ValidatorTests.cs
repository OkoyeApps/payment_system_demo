using Payment_System.Domain.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace Payment_System.Tests.Tests
{
    public class ValidatorTests
    {
        public readonly PaymentModelValidators _validator = new PaymentModelValidators();

        [Theory]
        [InlineData("2021/02/20")]
        public void Validator_validateExpiryDate_Successful(string expirtyDate)
        {
            bool expected = true;
            var actual = _validator.validateExpiryDate(expirtyDate, new DateTime(2020, 02, 21));
            Assert.Equal(expected, actual.Item1);
        }
        [Theory]
        [InlineData("2021/02/20")]
        public void Validator_validateExpiryDate_Failure(string expirtyDate)
        {
            var actual = _validator.validateExpiryDate(expirtyDate, new DateTime(2021, 03, 21));
            Assert.False(actual.Item1);
        }
        [Theory]
        [InlineData("2021/02/20")]
        public void Validator_validateExpiryDate_Returns_Errors(string expirtyDate)
        {
            var actual = _validator.validateExpiryDate(expirtyDate, new DateTime(2021, 03, 21));
            Assert.NotNull(actual.Item2);
        }

        [Theory]
        [InlineData(4, 8)]
        public void Validator_doubleNumber_Success(int number, int expected)
        {
            Type type = typeof(PaymentModelValidators);
            var payment = Activator.CreateInstance(type);
            MethodInfo method = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(x => x.Name == "doubleNumber" && x.IsPrivate).FirstOrDefault();
            var aa = type.GetMethods();
            var actual =(int) method.Invoke(payment, new object[] { number });

            Assert.Equal(expected, actual);
        }
        [Theory]
        [InlineData(9, 9)]
        public void Validator_doubleNumber_And_Add_When_Number_Is_GreaterThan_Nine_Success(int number, int expected)
        {
            Type type = typeof(PaymentModelValidators);
            var payment = Activator.CreateInstance(type);
            MethodInfo method = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(x => x.Name == "doubleNumber" && x.IsPrivate).FirstOrDefault();
            var aa = type.GetMethods();
            var actual = (int)method.Invoke(payment, new object[] { number });

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("4847352989263094")]
        public void Validator_ValidateCreditCard_Success(string cardNumber)
        {
            Assert.True(_validator.ValidateCreditCard(cardNumber));
        }

        [Theory]
        [InlineData("4847352989263091")]
        public void Validator_ValidateCreditCard_Failure(string cardNumber)
        {
            Assert.False(_validator.ValidateCreditCard(cardNumber));
        }
    }
}
