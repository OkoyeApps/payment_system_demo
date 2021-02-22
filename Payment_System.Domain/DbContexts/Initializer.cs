﻿using Payment_System.Domain.Entities;
using System;
using System.Linq;

namespace Payment_System.Domain.DbContexts
{
    public static class Initializer
    {
        public static void Seed(PaymentDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.PaymentStatus.Any())
            {
                return;
            }
            var values = Enum.GetNames(typeof(PaymentStatusEnum));

            PaymentState[] array_of_status = new PaymentState[3];
            for (var i = 0; i < values.Length; i++)
            {
                array_of_status[i] = new PaymentState
                {
                    Status = (PaymentStatusEnum)Enum.Parse(typeof(PaymentStatusEnum),
                    values[i]),
                    Name = values[i]
                };
            }
            context.PaymentStatus.AddRange(array_of_status);
            context.SaveChanges();
        }
    }
}
