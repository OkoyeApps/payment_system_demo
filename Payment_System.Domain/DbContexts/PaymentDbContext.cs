using Microsoft.EntityFrameworkCore;
using Payment_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment_System.Domain.DbContexts
{
    public class PaymentDbContext : DbContext
    {
        public PaymentDbContext(DbContextOptions<DbContext> options) : base(options)
        {
            
        }

        public DbSet<Payment> Payment { get; set; }
        public DbSet<PaymentStatus> PaymentStatus { get; set; }
    }
}
