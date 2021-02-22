using Microsoft.Extensions.DependencyInjection;
using Payment_System.Domain;
using Payment_System.Domain.Interfaces;
using Payment_System.Domain.Interfaces.Generic;
using Payment_System.Domain.Repositories;


namespace Payment_System.Api.Extensions
{
    public static class EntityRegistrationExtensions
    {
        public static void AddEntityServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddTransient<IPaymentInterface, PaymentRepository>();
            services.AddTransient<ILoggerInterface, LoggerRepository>();
            services.AddTransient(typeof(PaymentServiceFactory));
            services.AddSingleton<IPaymentCounterInterface, PaymentCounterRepository>();
        }
    }
}
