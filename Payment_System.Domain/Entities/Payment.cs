using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Payment_System.Domain.Entities
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; protected set; }
        public string CreditCardNumber { get; protected set; }
        public string CardHolder { get; protected set; }
        public string ExpirationDate { get; protected set; }
        public string SecurityCode { get; protected set; }
        [Column(TypeName = "decimal(18,2")]
        public decimal Amount { get; protected set; }
        public Guid PaymentStatusId { get;  set; }
        public PaymentState PaymentStatus { get; protected set; }

        internal Payment()
        {
        }

        public Payment(string card_number, string card_owner, string expiry_date, string security_code, decimal amount, Guid status)
        {
            CreditCardNumber = card_number;
            CardHolder = card_owner;
            ExpirationDate = expiry_date;
            SecurityCode = security_code;
            Amount = amount;
            PaymentStatusId = status;
        }

    }
}
