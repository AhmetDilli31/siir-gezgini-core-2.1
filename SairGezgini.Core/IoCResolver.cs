using System;

namespace SairGezgini.Core
{
    public class IoCResolver
    {
        private static IoCResolver _resolver;

        public static IoCResolver Current
        {
            get
            {
                if (_resolver == null)
                    throw new Exception("AppDependencyResolver not initialized. You should initialize it in Startup class");
                return _resolver;
            }
        }

        public static void Init(IServiceProvider services) => _resolver = new IoCResolver(services);

        private readonly IServiceProvider _serviceProvider;

        public T GetService<T>() => (T)_serviceProvider.GetService(typeof(T));

        private IoCResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
    }
}
