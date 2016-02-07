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
        private ApiResponse response;

        public UserMgr()
        {
            dacMgr = ServiceLocator.Current.GetInstance<IUserDacMgr>();
            response = new ApiResponse();
        }

        public ApiResponse Login(User info)
        {
            try
            {
                var validator = new UserLoginValidator();
                var result = validator.Validate(info);

                if (result.IsValid)
                {
                    info.Password = StringCipher.Encrypt(info.Password, passPhrase);

                    User user = dacMgr.Select(info.Email);

                    if (user != null)
                    {
                        if (user.Status == NUserStatus.Active.GetStrValue())
                        {
                            if (info.Password == user.Password)
                            {
                                response.Success = true;
                            }
                            else
                            {
                                response.ErrorList.Add(new Error { Message = "Invalid email and password" });
                            }
                        }
                        else if (user.Status == NUserStatus.ChangePassword.GetStrValue())
                        {
                            if (info.Password == user.Password)
                            {
                                user.Password = StringCipher.Decrypt(user.Password, passPhrase);

                                Modify(user);

                                response.Success = true;
                            }
                            else if (info.Password == user.TempPassword)
                            {
                                response.Success = true;
                            }
                        }
                    }
                    else
                    {
                        response.ErrorList.Add(new Error { Message = "Invalid email and password" });
                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        response.ErrorList.Add(new Error { Message = error.PropertyName + error.ErrorMessage });
                    }
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorList.Add(new Error { Message = "Internal Server Error Code:500" });

                logMgr.Error(ex);
            }

            return response;
        }

        public ApiResponse Add(User info)
        {
            try
            {
                var validator = new UserRegistrtionValidator();
                var result = validator.Validate(info);

                if (result.IsValid)
                {
                    info.Password = StringCipher.Encrypt(info.Password, passPhrase);
                    info.Status = NUserStatus.Active.GetStrValue();
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
                        response.ErrorList.Add(new Error { Message = error.PropertyName + error.ErrorMessage });
                    }
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorList.Add(new Error { Message = "Internal Server Error Code:500" });

                logMgr.Error(ex);
            }

            return response;
        }

        public ApiResponse Activate(User info)
        {
            try
            {
                var validator = new UserActivationValidator();
                var result = validator.Validate(info);

                if (result.IsValid)
                {
                    info.Status = NUserStatus.Active.GetStrValue();
                    info.UpdatedDate = DateTime.UtcNow;

                    if (dacMgr.Activate(info))
                    {
                        logMgr.Info("Activate a new user " + info.Email);
                        response.Success = true;
                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        response.ErrorList.Add(new Error { Message = error.PropertyName + error.ErrorMessage });
                    }
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorList.Add(new Error { Message = "Internal Server Error Code:500" });
                logMgr.Error(ex);
            }

            return response;
        }

        public ApiResponse RecoverPassword(User info)
        {
            try
            {
                var validator = new UserRecoverPasswordValidator();
                var result = validator.Validate(info);

                if (result.IsValid)
                {
                    if (dacMgr.Select(info.Email) != null)
                    {
                        info.UpdatedDate = DateTime.Now;
                        info.TempPassword = StringCipher.Encrypt(LogicHelper.ConstructPassword(), passPhrase);
                        info.Status = NUserStatus.ChangePassword.GetStrValue();

                        if (dacMgr.UpdateStatus(info))
                        {
                            var emailMgr = new EmailMgr();

                            info.TempPassword = StringCipher.Decrypt(info.TempPassword, passPhrase);
                            if (emailMgr.SendPwdRecoveryEmail(info))
                                response.Success = true;
                        }
                    }
                    else
                    {
                        response.ErrorList.Add(new Error { Message = "Email doesn't exist in database" });
                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        response.ErrorList.Add(new Error { Message = error.PropertyName + error.ErrorMessage });
                    }
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorList.Add(new Error { Message = "Internal Server Error Code:500" });
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
    }
}
