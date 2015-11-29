using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kong.OnlineStoreAPI.Model
{
    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Response { get; set; }
        public string Status { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
