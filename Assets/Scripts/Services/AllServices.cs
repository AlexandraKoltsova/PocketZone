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

        public void RegisterSingle<TService>(TService implementation) where TService : ISystem
        {
            Implementation<TService>.ServiceInstance = implementation;
        }

        public TService Single<TService>() where TService : ISystem
        {
            return Implementation<TService>.ServiceInstance;
        }

        private static class Implementation<TService> where TService : ISystem
        {
            public static TService ServiceInstance;
        }
    }
}