using Kong.OnlineStoreAPI.Model;
using Microsoft.Practices.Unity;

namespace Kong.OnlineStoreAPI.Logic
{
    public class Initialize
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ILogMgr, NLogLogger>();
            container.RegisterType<IUserMgr, UserMgr>();

        }
    }
}
