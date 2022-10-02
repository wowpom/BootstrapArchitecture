using CodeBase.Infrastructure.Factory;

namespace CodeBase.Infrastructure.Services
{
    public class ServicesLocator
    {
        private static ServicesLocator _instance;
        public static ServicesLocator Container => _instance ?? (_instance = new ServicesLocator());

        public void RegisterSingle<TService>(TService implement) where TService : IService
            => Implementation<TService>.ServiceInstance = implement;

        public TService Single<TService>() where TService : IService
            => Implementation<TService>.ServiceInstance;

        private static class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
    }
}