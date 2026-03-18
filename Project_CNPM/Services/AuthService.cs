using Microsoft.Data.Sqlite;
using Library.Models;

namespace Library.Services
{
    public static class AuthService
    {
        public static User Login(string u, string p)
        {
            using var c = new SqliteConnection("Data Source=library.db");
            c.Open();

            var cmd = c.CreateCommand();
            cmd.CommandText = "SELECT * FROM Users WHERE Username=@u AND Password=@p";
            cmd.Parameters.AddWithValue("@u", u);
            cmd.Parameters.AddWithValue("@p", p);

            using var r = cmd.ExecuteReader();
            if (r.Read())
            {
                return new User
                {
                    Id = r.GetInt32(0),
                    Username = r.GetString(1),
                    Password = r.GetString(2),
                    Role = r.GetString(3)
                };
            }
            return null;
        }

        

        public static void ResetPassword(string username, string newPass)
        {
            using var c = new SqliteConnection("Data Source=library.db");
            c.Open();

            var cmd = c.CreateCommand();
            cmd.CommandText = "UPDATE Users SET Password=@p WHERE Username=@u";
            cmd.Parameters.AddWithValue("@p", newPass);
            cmd.Parameters.AddWithValue("@u", username);

            cmd.ExecuteNonQuery();
        }

        public static bool Register(string u, string p)
        {
            using var c = new SqliteConnection("Data Source=library.db");
            c.Open();

            // check trùng
            var check = c.CreateCommand();
            check.CommandText = "SELECT COUNT(*) FROM Users WHERE Username=@u";
            check.Parameters.AddWithValue("@u", u);

            long count = (long)check.ExecuteScalar();
            if (count > 0) return false;

            var cmd = c.CreateCommand();
            cmd.CommandText = "INSERT INTO Users VALUES(NULL,@u,@p,'User')";
            cmd.Parameters.AddWithValue("@u", u);
            cmd.Parameters.AddWithValue("@p", p);

            cmd.ExecuteNonQuery();
            return true;
        }
    }
}