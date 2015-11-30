using Kong.OnlineStoreAPI.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Kong.OnlineStoreAPI.Logic
{
    public static class LogicHelper
    {
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GetStrValue(this Enum value)
        {
            string strValue = string.Empty;

            StringValueAttribute[] customAttributes = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
            if (customAttributes.Length > 0)
            {
                strValue = customAttributes[0].Value;
            }

            return strValue;
        }
    }
}
