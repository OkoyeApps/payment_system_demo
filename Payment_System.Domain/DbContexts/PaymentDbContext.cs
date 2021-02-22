using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Payment_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Payment_System.Domain.DbContexts
{
    interface DbSetInterfaces
    {
        DbSet<Payment> Payment { get; set; }
        DbSet<PaymentState> PaymentStatus { get; set; }
    }
    public class PaymentDbContext :  DbContext, DbSetInterfaces
    {
        public PaymentDbContext() : base()
        {

        }
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
        {
            
        }

        public DbSet<Payment> Payment { get; set; }
        public DbSet<PaymentState> PaymentStatus { get; set; }
    }

    public class BackgroundDbContex : DbContext, DbSetInterfaces
    {
        public BackgroundDbContex(DbContextOptions<BackgroundDbContex> options) : base(options)
        {

        }

        public DbSet<Payment> Payment { get; set; }
        public DbSet<PaymentState> PaymentStatus { get; set; }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PaymentDbContext>
    {
        public PaymentDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(System.IO.Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../Payment_System.Api/appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<PaymentDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
            return new PaymentDbContext(builder.Options);
        }
    }
}
