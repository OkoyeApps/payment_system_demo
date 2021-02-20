using Microsoft.Extensions.DependencyInjection;
using Payment_System.Domain.Interfaces;
using Payment_System.Domain.Repositories;


namespace Payment_System.Api.Extensions
{
    public static class EntityRegistrationExtensions
    {
        public static void AddEntityServices(this IServiceCollection services)
        {
            services.AddTransient<IPaymentInterface, PaymentRepository>();
        }
    }
}
