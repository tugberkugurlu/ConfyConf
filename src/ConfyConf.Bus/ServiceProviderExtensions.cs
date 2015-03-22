using System;

namespace ConfyConf.Bus
{
    internal static class ServiceProviderExtensions
    {
        public static TService GetService<TService>(this IServiceProvider provider) where TService : class
        {
            return provider.GetService(typeof(TService)) as TService;
        }
    }
}