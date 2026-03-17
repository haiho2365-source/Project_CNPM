using Library.Models;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace Project_CNPM.Services
{
    public static class BookService
    {
        static string conn = "Data Source=library.db";

        public static List<Book> GetAll()
        {
            var list = new List<Book>();
            using var c = new SqliteConnection(conn);
            c.Open();

            var cmd = c.CreateCommand();
            cmd.CommandText = "SELECT * FROM Books";

            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                list.Add(new Book
                {
                    Id = r.GetInt32(0),
                    Title = r.GetString(1),
                    Author = r.GetString(2),
                    Category = r.GetString(3),
                    Quantity = r.GetInt32(4)
                });
            }
            return list;
        }

        public static void Add(Book b)
        {
            using var c = new SqliteConnection(conn);
            c.Open();

            var cmd = c.CreateCommand();
            cmd.CommandText = "INSERT INTO Books VALUES(NULL,@t,@a,@c,@q)";
            cmd.Parameters.AddWithValue("@t", b.Title);
            cmd.Parameters.AddWithValue("@a", b.Author);
            cmd.Parameters.AddWithValue("@c", b.Category);
            cmd.Parameters.AddWithValue("@q", b.Quantity);

            cmd.ExecuteNonQuery();
        }

        public static void Delete(int id)
        {
            using var c = new SqliteConnection(conn);
            c.Open();

            var cmd = c.CreateCommand();
            cmd.CommandText = "DELETE FROM Books WHERE Id=@id";
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }

        public static List<Book> Search(string keyword)
        {
            var list = new List<Book>();

            using var c = new SqliteConnection(conn);
            c.Open();

            var cmd = c.CreateCommand();
            cmd.CommandText = @"
        SELECT * FROM Books 
        WHERE Title LIKE @k OR Author LIKE @k";

            cmd.Parameters.AddWithValue("@k", "%" + keyword + "%");

            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                list.Add(new Book
                {
                    Id = r.GetInt32(0),
                    Title = r.GetString(1),
                    Author = r.GetString(2),
                    Category = r.GetString(3),
                    Quantity = r.GetInt32(4)
                });
            }

            return list;
        }

        public static List<Book> Filter(string category)
        {
            var list = new List<Book>();

            using var c = new SqliteConnection(conn);
            c.Open();

            var cmd = c.CreateCommand();
            cmd.CommandText = "SELECT * FROM Books WHERE Category=@c";
            cmd.Parameters.AddWithValue("@c", category);

            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                list.Add(new Book
                {
                    Id = r.GetInt32(0),
                    Title = r.GetString(1),
                    Author = r.GetString(2),
                    Category = r.GetString(3),
                    Quantity = r.GetInt32(4)
                });
            }

            return list;
        }
        public static void Update(Book b)
        {
            using var c = new SqliteConnection(conn);
            c.Open();

            var cmd = c.CreateCommand();
            cmd.CommandText = @"UPDATE Books 
                                SET Title=@t, Author=@a, Category=@c, Quantity=@q 
                                WHERE Id=@id";

            cmd.Parameters.AddWithValue("@t", b.Title);
            cmd.Parameters.AddWithValue("@a", b.Author);
            cmd.Parameters.AddWithValue("@c", b.Category);
            cmd.Parameters.AddWithValue("@q", b.Quantity);
            cmd.Parameters.AddWithValue("@id", b.Id);

            cmd.ExecuteNonQuery();
        }
    }
}