using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Payment_System.Domain
{
    public class PaymentServiceFactory
    {
        private readonly IServiceProvider provider;

        public PaymentServiceFactory(IServiceProvider _provider)
        {
            provider = _provider;
        }

        public TEntity GetServices<TEntity>(params Type[] services) where TEntity : class
        {
            var service = provider.GetService<TEntity>();
            return (TEntity)service;
        }

        public static IServiceProvider ServiceProvider { get; }

        //static ServiceProviderFactory()
        //{
        //    HostingEnvironment env = new HostingEnvironment();
        //    env.ContentRootPath = Directory.GetCurrentDirectory();
        //    env.EnvironmentName = "Development";

        //    Startup startup = new Startup(env);
        //    ServiceCollection sc = new ServiceCollection();
        //    startup.ConfigureServices(sc);
        //    ServiceProvider = sc.BuildServiceProvider();
        //}
    }
}
