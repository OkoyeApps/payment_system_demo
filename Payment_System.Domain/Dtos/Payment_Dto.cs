﻿using Payment_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Payment_System.Domain.Dtos
{
    public class Payment_Dto
    {
        [Required(ErrorMessage ="Card number is required")]
        public string card_number { get; set; }
        [Required(ErrorMessage ="Card holder is required ")]
        public string card_holder { get; set; }
        [Required(ErrorMessage ="Card expiration date is required")]
        public string expiry_date { get; set; }
        [MaxLength(3, ErrorMessage ="security code can't be more than three (3) letters")]
        public string security_code { get; set; }
        [Required(ErrorMessage ="Amount is required")]
        [Range(0, int.MaxValue, ErrorMessage ="Amount can't be a negative number")]
        public decimal amount { get; set; }
        public Guid Status { get; set; }
    }

    public class PaymentResponse
    {
        public Guid Id { get; set; }
        public PaymentStatusEnum  Status { get; set; }
        public string Message { get; set; } = "Processing Payment...";
    }
    public class PaymentDetailResponse : PaymentResponse
    {
        public string card_number { get; set; }
        public string card_holder { get; set; }
        public decimal amount { get; set; }
    }
}
