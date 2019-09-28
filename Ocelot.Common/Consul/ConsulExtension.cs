using System;
using System.Linq;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ocelot.Common.Consul
{
    public static class ConsulExtension
    {
        /// <summary>
        /// consul 配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IServiceCollection AddConsulConfig(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(_config =>
            {
                var _address = config.GetValue<string>("Consul:Host");
                _config.Address = new Uri(_address);

            }));
            return services;
        }

        public static IApplicationBuilder UseConsul(this IApplicationBuilder app)
        {
            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
            var lifetime = app.ApplicationServices.GetRequiredService<IApplicationLifetime>();

            if (!(app.Properties["server.Features"] is FeatureCollection features)) return app;

            var addresses = features.Get<IServerAddressesFeature>();
            var address = addresses.Addresses.First();
            var uri = new Uri(address);
            var registration = new AgentServiceRegistration()
            {
                ID = $"MyService-{uri.Port}",
                // servie name  
                Name = "MyService",
                Address = $"{uri.Host}",
                Port = uri.Port
            };

            consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
            consulClient.Agent.ServiceRegister(registration).ConfigureAwait(true);

            lifetime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
            });

            return app;
        }
    }
}
