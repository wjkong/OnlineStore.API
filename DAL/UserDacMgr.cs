using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kong.OnlineStoreAPI.Model;
using System.Data.SqlClient;
using Kong.ApiExpert.DAL;
using System.Data;

namespace Kong.OnlineStoreAPI.DAL
{
    public class UserDacMgr : DataAccessBase
    {
        public bool Insert(User info)
        {
            bool success = false;
            SqlCommand cmd = null;

            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    cmd = new SqlCommand();

                    cmd.CommandText = @"INSERT INTO [EStoreUser] (Email, Password) VALUES (@Email, @Password)";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@Email", info.Email);
                    cmd.Parameters.AddWithValue("@Password", info.Password);

                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }

                success = true;
            }
            catch
            {
                throw;
            }
            finally
            {
                cmd.Connection.Close();
            }

            return success;
        }

        public object Update(User info)
        {
            bool success = false;
            SqlCommand cmd = null;

            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    cmd = new SqlCommand();

                    cmd.CommandText = @"UPDATE [EStoreUser] SET Password = @Password, UpdatedDate = @UpdatedDate WHERE Email = @Email";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@Password", info.Password);
                    cmd.Parameters.AddWithValue("@UpdatedDate", info.UpdatedDate);
                    cmd.Parameters.AddWithValue("@Email", info.Email);
                    
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }

                success = true;
            }
            catch
            {
                throw;
            }
            finally
            {
                cmd.Connection.Close();
            }

            return success;
        }
    }
}
