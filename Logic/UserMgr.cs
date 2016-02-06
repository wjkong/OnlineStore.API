using Kong.OnlineStoreAPI.Logic.Validator;
using Kong.OnlineStoreAPI.Model;
using Microsoft.Practices.ServiceLocation;
using NLog;
using System;
using System.Configuration;

namespace Kong.OnlineStoreAPI.Logic
{
    public class UserMgr : IUserMgr
    {
        private string passPhrase = ConfigurationManager.AppSettings["passPhrase"].ToString();

        private Logger logMgr = LogManager.GetCurrentClassLogger();

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

        public ApiResponse Add(User info)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var validator = new UserRegistrtionValidator();
                var result = validator.Validate(info);

                if (result.IsValid)
                {
                    info.Password = StringCipher.Encrypt(info.Password, passPhrase);
                    info.Status = NUserStatus.Inactive.GetStrValue();
                    info.Token = Guid.NewGuid().ToString();

                    if (dacMgr.Insert(info))
                    {
                        logMgr.Info("Register new an user " + info.Email);
                        response.Success = true;

                        var emailMgr = new EmailMgr();

                        if (!emailMgr.SendRegConfirmEmail(info))
                            logMgr.Info("Fail to send registration email " + info.Email);
                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        response.ErrorList.Add(new Error
                        {
                            PropertyName = error.PropertyName,
                            Message = error.PropertyName + error.ErrorMessage,
                            Code = error.ErrorCode
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorList.Add(new Error
                {
                    PropertyName = "Generic Error",
                    Message = "Internal Server Error Code:500"
                });

                logMgr.Error(ex);
            }

            return response;

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
