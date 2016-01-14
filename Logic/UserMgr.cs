using Kong.OnlineStoreAPI.Model;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Configuration;

namespace Kong.OnlineStoreAPI.Logic
{
    public class UserMgr : IUserMgr
    {
        private string passPhrase = ConfigurationManager.AppSettings["passPhrase"].ToString();

        private IUserDacMgr dacMgr;

        public UserMgr()
        {
            dacMgr = ServiceLocator.Current.GetInstance<IUserDacMgr>();
        }

        public bool Login(User info)
        {
            bool success = false;

            info.Password = StringCipher.Encrypt(info.Password, passPhrase);

            User user = dacMgr.Select(info.Email);

            if (user != null)
            {
                if (user.Status == NUserStatus.Active.GetStrValue())
                {
                    if (info.Password == user.Password)
                    {
                        success = true;
                    }
                }
                else if (user.Status == NUserStatus.ChangePassword.GetStrValue())
                {
                    if (info.Password == user.Password)
                    {
                        user.Password = StringCipher.Decrypt(user.Password, passPhrase);

                        Modify(user);

                        success = true;
                    }
                    else if (info.Password == user.TempPassword)
                    {
                        success = true;
                    }
                }
            }

            return success;
        }
        
        public bool Add(User info)
        {
            bool success = false;

            info.Password = StringCipher.Encrypt(info.Password, passPhrase);
            info.Status = NUserStatus.Inactive.GetStrValue();
            info.Token = Guid.NewGuid().ToString();

            if (dacMgr.Insert(info))
            {
                var emailMgr = new EmailMgr();
                                
                if (emailMgr.SendRegConfirmEmail(info))
                    success = true;
            }

            return success;

        }

        public bool Modify(User info)
        {
            info.UpdatedDate = DateTime.Now;
            info.Password = StringCipher.Encrypt(info.Password, passPhrase);
            info.Status = NUserStatus.Active.GetStrValue();
            info.TempPassword = string.Empty;

            return dacMgr.Update(info);
        }

        public bool Modify(string action, User info)
        {
            bool success = false;

            if (dacMgr.Select(info.Email) != null)
            { 
                info.UpdatedDate = DateTime.Now;
                info.TempPassword = StringCipher.Encrypt(LogicHelper.RandomString(6), passPhrase);
                info.Status = NUserStatus.ChangePassword.GetStrValue();

                if (dacMgr.UpdateStatus(info))
                {
                    var emailMgr = new EmailMgr();

                    info.TempPassword = StringCipher.Decrypt(info.TempPassword, passPhrase);
                    if (emailMgr.SendPwdRecoveryEmail(info))
                        success = true;
                }
            }

            return success;
        }
    }
}
