using System;
using System.ComponentModel.DataAnnotations;

namespace Kong.OnlineStoreAPI.Model
{
    public class User
    {
        /// <summary>
        /// Email is username.
        /// </summary>
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string TempPassword { get; set; }
        public string Response { get; set; }
        public string Status { get; set; }
        public string Token { get; set; }
        public DateTime UpdatedDate { get; set; }
    }

    public interface IUserMgr
    {
        ApiResponse Login(User info);
        ApiResponse Add(User info);
        ApiResponse Activate(User info);
        ApiResponse RecoverPassword(User info);
        ApiResponse ChangePassword(User info);
    }

    public interface IUserDacMgr
    {
        bool Insert(User info);
        User Select(string email);
        bool Update(User info);
        bool UpdateStatus(User info);
        bool Activate(User info);
    }



}
