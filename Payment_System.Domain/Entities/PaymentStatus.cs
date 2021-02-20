using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Payment_System.Domain.Entities
{
    public class PaymentStatus
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public int PaymentId { get; set; }
        public PaymentStatusEnum Status { get; set; } = PaymentStatusEnum.PENDING;
        public Payment Payment { get; set; }
    }

    public enum PaymentStatusEnum
    {
        PENDING = 0,
        PROCESSED,
        FAILED 
    }
}
