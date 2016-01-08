﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Kong.OnlineStoreAPI.Model
{
    public class User
    {
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
        public string TempPassword { get; set; }
        public string Response { get; set; }
        public string Status { get; set; }
        public string Token { get; set; }
        public DateTime UpdatedDate { get; set; }
    }

    public interface IUserMgr
    {
        bool Login(User info);
        bool Add(User info);
        bool Modify(User info);
        bool Modify(string action, User info);
    }

    public interface IUserDacMgr
    {
        bool Insert(User info);
        User Select(string email);
        bool Update(User info);
        bool UpdateStatus(User info);
    }

   

}
