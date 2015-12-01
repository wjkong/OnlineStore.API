using Kong.OnlineStoreAPI.DAL;
using Kong.OnlineStoreAPI.Model;
using System;
using System.Configuration;

namespace Kong.OnlineStoreAPI.Logic
{
    public class UserMgr
    {
        private string passPhrase = ConfigurationManager.AppSettings["passPhrase"].ToString();

        protected UserDacMgr dacMgr = new UserDacMgr();

        public bool Login(User info)
        {
            info.Status = "A";
            info.Password = StringCipher.Encrypt(info.Password, passPhrase);

            return dacMgr.Login(info);
        }
        
        public bool Add(User info)
        {
            info.Password = StringCipher.Encrypt(info.Password, passPhrase);
            return dacMgr.Insert(info);
        }

        public bool Modify(User info)
        {
            info.UpdatedDate = DateTime.Now;
            info.Password = StringCipher.Encrypt(info.Password, passPhrase);

            return dacMgr.Update(info);
        }

        public object Modify(string action, User info)
        {
            bool success = false;

            info.UpdatedDate = DateTime.Now;
            info.Password = StringCipher.Encrypt(LogicHelper.RandomString(6), passPhrase);
            info.Status = NUserStatus.ChangePassword.GetStrValue(); ;

            if (dacMgr.UpdateStatus(info))
            {
                var emailMgr = new EmailMgr();

                if (emailMgr.SendPwdRecoveryEmail(info))
                    success = true;
            }
            
            return success;
        }
    }
}
