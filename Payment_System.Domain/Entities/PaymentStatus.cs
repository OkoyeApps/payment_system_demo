using System;
using System.ComponentModel.DataAnnotations;

namespace Payment_System.Domain.Entities
{
    public class PaymentStatus
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public PaymentStatusEnum Status { get; set; }
        public string Name { get; set; }
    }

    public enum PaymentStatusEnum
    {
        PENDING = 1,
        PROCESSED,
        FAILED
    }
}
