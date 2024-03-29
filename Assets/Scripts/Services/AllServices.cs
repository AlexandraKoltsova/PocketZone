namespace Services
{
    public class AllServices
    {
        private static AllServices _instanse;

        public static AllServices Container
        {
            get
            {
                return _instanse ?? (_instanse = new AllServices());
            }
        }

        public void RegisterSingle<TService>(TService implementation) where TService : IService
        {
            Implementation<TService>.ServiceInstance = implementation;
        }

        public TService Single<TService>() where TService : IService
        {
            return Implementation<TService>.ServiceInstance;
        }

        private static class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
    }
}