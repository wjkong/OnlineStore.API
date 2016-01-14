using System;

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
