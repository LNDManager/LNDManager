using Grpc.Core;
using Havit.Diagnostics.Contracts;
using LNDManager.Shared.Indicator;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;
using static Lnrpc.Lightning;

namespace LNDManager.Infrastructure
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddProgressIndicator(this IServiceCollection services, Action<IndicatorOptions> options = null)
        {
            var _options = new IndicatorOptions();
            options?.Invoke(_options);
            services.AddSingleton<IIndicatorService, IndicatorService>(_ => new IndicatorService
            {
                Options = _options
            });
            return services;
        }

        public static IServiceCollection AddLightningClient(this IServiceCollection services, LightningClientOptions options)
        {
            Contract.Requires<ArgumentNullException>(options != null);
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(options.Macaroon) ^ !String.IsNullOrWhiteSpace(options.MacaroonFilePath), "Set either Macaroon or MacaronFilePath.");
            Contract.Requires<ArgumentException>(String.IsNullOrWhiteSpace(options.MacaroonFilePath) || File.Exists(options.MacaroonFilePath), $"File {options.MacaroonFilePath} doesn't exist.");
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(options.Host), "Missing host LND host.");
            Contract.Requires<ArgumentException>(options.Port != default, "Missing host LND port.");

            string macaroon = options.Macaroon;
            if (File.Exists(options.MacaroonFilePath))
            {
                byte[] macaroonBytes = File.ReadAllBytes(options.MacaroonFilePath);
                macaroon = BitConverter.ToString(macaroonBytes).Replace("-", "");
            }

            services.AddSingleton(serviceProvider =>
            {
                var sslCreds = new SslCredentials();
                Task AddMacaroon(AuthInterceptorContext context, Metadata metadata)
                {
                    metadata.Add(new Metadata.Entry("macaroon", macaroon));
                    return Task.CompletedTask;
                }
                var macaroonInterceptor = new AsyncAuthInterceptor(AddMacaroon);
                var combinedCreds = ChannelCredentials.Create(sslCreds, CallCredentials.FromInterceptor(macaroonInterceptor));

                var channel = new Channel($"{options.Host}:{options.Port}", combinedCreds);
                return new LightningClient(channel);
            });
            return services;
        }
    }
}
