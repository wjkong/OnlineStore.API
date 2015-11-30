using Kong.OnlineStoreAPI.DAL;
using Kong.OnlineStoreAPI.Model;
using System;

namespace Kong.OnlineStoreAPI.Logic
{
    public class UserMgr
    {
        protected UserDacMgr dacMgr = new UserDacMgr();

        public bool Login(User info)
        {
            info.Status = "A";

            return dacMgr.Login(info);
        }
        
        public bool Add(User info)
        {
            return dacMgr.Insert(info);
        }

        public bool Modify(User info)
        {
            info.UpdatedDate = DateTime.Now;

            return dacMgr.Update(info);
        }

        public object Modify(string action, User info)
        {
            info.UpdatedDate = DateTime.Now;

            return dacMgr.UpdateStatus(info);
        }
    }
}
