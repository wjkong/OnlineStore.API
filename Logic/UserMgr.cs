using Kong.OnlineStoreAPI.DAL;
using Kong.OnlineStoreAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
