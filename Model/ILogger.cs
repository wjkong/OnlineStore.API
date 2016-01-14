using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kong.OnlineStoreAPI.Model
{
    public interface ILogMgr
    {
        void Info(string info);
        void Error(Exception ex);
        void Fatal(Exception ex);
    }
}
