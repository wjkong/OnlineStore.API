using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kong.OnlineStoreAPI.Model
{
    public abstract class Utility
    {
    }

    public enum NUserStatus
    {
        [StringValue("A")]
        Active,

        [StringValue("I")]
        Inactive,

        [StringValue("C")]
        ChangePassword,

        [StringValue("L")]
        Locked
    }
}
