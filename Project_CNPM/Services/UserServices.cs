using Microsoft.Data.Sqlite;
using Library.Models;
using System.Collections.Generic;

namespace Project_CNPM.Services
{
    public static class UserService
    {
        public static List<User> GetAll()
        {
            var list = new List<User>();

            using var c = new SqliteConnection("Data Source=library.db");
            c.Open();

            var cmd = c.CreateCommand();
            cmd.CommandText = "SELECT * FROM Users";

            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                list.Add(new User
                {
                    Id = r.GetInt32(0),
                    Username = r.GetString(1),
                    Password = r.GetString(2),
                    Role = r.GetString(3)
                });
            }

            return list;
        }

        public static void Delete(int id)
        {
            using var c = new SqliteConnection("Data Source=library.db");
            c.Open();

            var cmd = c.CreateCommand();
            cmd.CommandText = "DELETE FROM Users WHERE Id=@id";
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }
    }
}