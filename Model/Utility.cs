using System;
using System.Configuration;
using System.Data.Common;

namespace Kong.OnlineStoreAPI.Model
{
    public static class Utility
    {
        public static readonly string REQUIRED_FIELD = " is required;";
        public static readonly string INVALID_FIELD = " is invalid format;";
        public static readonly string INVALID_PASSWORD = " must contains at least a UPPPER and a lower letter with min. 8 characters;";
        public static readonly string VALID_PASSWORD = @"(?=^.{8,16}$)(?=.*\d)(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$";

        public static readonly string BASE_URL = ConfigurationManager.AppSettings["BaseUrl"].ToString();



        public static bool Eq(this string str, string otherStr)
        {
            if (str == null)
            {
                return otherStr == null;
            }
            else
            {
                return str.Equals(otherStr, StringComparison.OrdinalIgnoreCase);
            }
        }

        public static DbConnection OpenIt(this DbConnection conn)
        {
            if (conn != null)
            {
                conn.Open();
            }

            return conn;
        }

        public static DbParameter AddParameter(this DbCommand cmd, string name, object value)
        {
            DbParameter param = cmd.CreateParameter();

            param.ParameterName = name;
            param.Value = value;

            cmd.Parameters.Add(param);

            return param;
        }
    }

    public enum NUserStatus
    {
        [StringValue("A")]
        Active,

        [StringValue("I")]
        Inactive,

        [StringValue("C")]
        ChangePassword,

        [StringValue("L")]
        Locked
    }

    public class Result
    {
        public bool Success { get; set; }
        public string Response { get; set; }
    }

}
