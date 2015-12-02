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
        protected SqlDataReader dreader;

        public bool Insert(User info)
        {
            bool success = false;
            SqlCommand cmd = null;

            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    cmd = new SqlCommand();

                    cmd.CommandText = @"INSERT INTO [EStoreUser] (Email, Password, Status, Token) VALUES (@Email, @Password, @Status, @token)";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@Email", info.Email);
                    cmd.Parameters.AddWithValue("@Password", info.Password);
                    cmd.Parameters.AddWithValue("@Status", info.Status);
                    cmd.Parameters.AddWithValue("@Token", info.Token);
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

        public User Select(string email)
        {
            User info = null;

            SqlCommand cmd = null;

            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    cmd = new SqlCommand();

                    cmd.CommandText = @"SELECT * FROM [EStoreUser] WHERE Email = @Email";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
            
                    cmd.Parameters.AddWithValue("@Email", email);

                    if (cmd.Connection.State == ConnectionState.Closed)
                    {
                        cmd.Connection.Open();
                    }

                    this.dreader = cmd.ExecuteReader();

                    if (this.dreader.Read())
                    {
                        info = new User();
                                       
                        info.Email = dreader["email"].ToString();
                        info.Password = dreader["Password"].ToString();
                        info.Status = dreader["Status"].ToString();
                        info.TempPassword = dreader["TempPassword"] == System.DBNull.Value ? string.Empty : dreader["TempPassword"].ToString();
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (this.dreader != null)
                {
                    this.dreader.Close();
                }

                cmd.Connection.Close();
            }

            return info;
        }

        public bool Update(User info)
        {
            bool success = false;
            SqlCommand cmd = null;

            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    cmd = new SqlCommand();

                    cmd.CommandText = @"UPDATE [EStoreUser] SET Password = @Password, Status = @Status, UpdatedDate = @UpdatedDate, TempPassword = @TempPassword WHERE Email = @Email";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@Password", info.Password);
                    cmd.Parameters.AddWithValue("@Status", info.Status);
                    cmd.Parameters.AddWithValue("@UpdatedDate", info.UpdatedDate);
                    cmd.Parameters.AddWithValue("@TempPassword", info.TempPassword);
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

        public bool UpdateStatus(User info)
        {
            bool success = false;
            SqlCommand cmd = null;

            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    cmd = new SqlCommand();

                    cmd.CommandText = @"UPDATE [EStoreUser] SET TempPassword = @TempPassword, Status = @Status, UpdatedDate = @UpdatedDate WHERE Email = @Email";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@TempPassword", info.TempPassword);
                    cmd.Parameters.AddWithValue("@Status", info.Status);
                    cmd.Parameters.AddWithValue("@UpdatedDate", info.UpdatedDate);
                    cmd.Parameters.AddWithValue("@Email", info.Email);

                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();

                    success = true;
                }
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
