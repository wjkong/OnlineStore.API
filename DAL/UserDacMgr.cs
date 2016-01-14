using Kong.ApiExpert.DAL;
using Kong.OnlineStoreAPI.Model;
using System.Data;
using System.Data.SqlClient;

namespace Kong.OnlineStoreAPI.DAL
{
    public class UserDacMgr : DataAccessBase, IUserDacMgr
    {
        protected SqlDataReader dreader;

        public bool Insert(User info)
        {
            bool success = false;

            using (var connection = CreateConnection().OpenIt())
            {
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO [EStoreUser] (Email, Password, Status, Token) VALUES (@Email, @Password, @Status, @token)";
                    cmd.AddParameter("@Email", info.Email);
                    cmd.AddParameter("@Password", info.Password);
                    cmd.AddParameter("@Status", info.Status);
                    cmd.AddParameter("@Token", info.Token);

                    cmd.ExecuteNonQuery();

                    success = true;
                }
            }

            return success;
        }

        public User Select(string email)
        {
            User info = null;

            using (var connection = CreateConnection().OpenIt())
            {
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM [EStoreUser] WHERE Email = @Email";
                    cmd.CommandType = CommandType.Text;

                    cmd.AddParameter("@Email", email);

                    using (var dReader = cmd.ExecuteReader())
                    {
                        if (dReader.Read())
                        {
                            info = new User();

                            info.Email = dReader["email"].ToString();
                            info.Password = dReader["Password"].ToString();
                            info.Status = dReader["Status"].ToString();
                            info.TempPassword = dReader["TempPassword"] == System.DBNull.Value ? string.Empty : dReader["TempPassword"].ToString();
                        }
                    }
                }
            }

            return info;
        }

        public bool Update(User info)
        {
            bool success = false;

            using (var connection = CreateConnection().OpenIt())
            {
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE [EStoreUser] SET Password = @Password, Status = @Status, UpdatedDate = @UpdatedDate, TempPassword = @TempPassword WHERE Email = @Email";
                    cmd.CommandType = CommandType.Text;

                    cmd.AddParameter("@Password", info.Password);
                    cmd.AddParameter("@Status", info.Status);
                    cmd.AddParameter("@UpdatedDate", info.UpdatedDate);
                    cmd.AddParameter("@TempPassword", info.TempPassword);
                    cmd.AddParameter("@Email", info.Email);

                    cmd.ExecuteNonQuery();

                    success = true;
                }

            }

            return success;
        }

        public bool UpdateStatus(User info)
        {
            bool success = false;

            using (var connection = CreateConnection().OpenIt())
            {
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE [EStoreUser] SET TempPassword = @TempPassword, Status = @Status, UpdatedDate = @UpdatedDate WHERE Email = @Email";
                    cmd.CommandType = CommandType.Text;

                    cmd.AddParameter("@TempPassword", info.TempPassword);
                    cmd.AddParameter("@Status", info.Status);
                    cmd.AddParameter("@UpdatedDate", info.UpdatedDate);
                    cmd.AddParameter("@Email", info.Email);

                    cmd.ExecuteNonQuery();

                    success = true;
                }
            }

            return success;
        }
    }
}
