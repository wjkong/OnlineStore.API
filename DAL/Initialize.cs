using Kong.OnlineStoreAPI.Model;
using Microsoft.Practices.Unity;

namespace Kong.OnlineStoreAPI.DAL
{
    public class Initialize
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IUserDacMgr, UserDacMgr>();

        }
    }
}
