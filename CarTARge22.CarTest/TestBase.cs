using CarTARge22.ApplicationServices.Services;
using CarTARge22.CarTest.Macros;
using CarTARge22.CarTest.Mock;
using CarTARge22.Core.ServiceInterface;
using CarTARge22.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTARge22.CarTest
{
    public abstract class TestBase
    {
        protected IServiceProvider ServiceProvider { get; }
        protected TestBase() { 
        var services = new ServiceCollection();
            SetupServices(services);
            ServiceProvider = services.BuildServiceProvider();
        }

        public void Dispose()
        { }

        protected T Svc<T>()
        {
            return ServiceProvider.GetService<T>();
        }

        protected T Macro<T>() where T : IMacros
        {
            return ServiceProvider.GetService<T>();
        }

        public virtual void SetupServices(IServiceCollection services)
        {
            services.AddScoped<ICarsServices, CarsServices>();
            services.AddScoped<IHostEnvironment, MockIHostEnvironment>();

            services.AddDbContext<CarTARge22Context>(x =>
            {
                x.UseInMemoryDatabase("TEST");
                x.ConfigureWarnings(e => e.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                RegisterMacros(services);
            });
        }

        private void RegisterMacros(IServiceCollection services)
        {
            var macroBaseType = typeof(IMacros);

            var macros = macroBaseType.Assembly.GetTypes()
                .Where(x => macroBaseType.IsAssignableFrom(x) && !!x.IsInterface && !x.IsAbstract);

            foreach (var macro in macros )
            {
                services.AddSingleton(macro);
            }
        }

    }
}
