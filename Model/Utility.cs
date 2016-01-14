using System;
using System.Data.Common;

namespace Kong.OnlineStoreAPI.Model
{
    public static class Utility
    {
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
