using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kong.OnlineStoreAPI.Model
{
    public class Email
    {
        public string Body { get; set; }

        public DateTime DateSent { get; set; }
     
        public long EmailID { get; set; }
     
        public string From { get; set; }
    
        public string NPriority { get; set; }
   
        public string Subject { get; set; }
      
        public string To { get; set; }
    }
}
