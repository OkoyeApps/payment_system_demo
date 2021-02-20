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
        public Guid Id { get; set; }
        public string CreditCardNumber { get; set; }
        public string CardHolder { get; set; }
        public string ExpirationDate { get; set; }
        public string SecurityCode { get; set; }
        [Column(TypeName = "decimal(18,2")]
        public decimal Amount { get; set; }
        public int PaymentStatusId { get; set; }
        public PaymentStatus PaymentStatus { get; set; }

        internal Payment()
        {
        }

        public Payment(string card_number, string card_owner, string expiry_date, string security_code, decimal amount)
        {
            CreditCardNumber = card_number;
            CardHolder = card_owner;
            ExpirationDate = expiry_date;
            SecurityCode = security_code;
            Amount = amount;
        }


    }
}
