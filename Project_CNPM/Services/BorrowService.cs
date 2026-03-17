using Library.Models;
using Microsoft.Data.Sqlite;
using Library.Models;
using System;
using System.Collections.Generic;

namespace Project_CNPM.Services
{
    public static class BorrowService
    {
        public static void BorrowBook(int userId, int bookId)
        {
            using var c = new SqliteConnection("Data Source=library.db");
            c.Open();

            // giảm số lượng
            var update = c.CreateCommand();
            update.CommandText = "UPDATE Books SET Quantity = Quantity - 1 WHERE Id=@id AND Quantity > 0";
            update.Parameters.AddWithValue("@id", bookId);
            update.ExecuteNonQuery();

            var cmd = c.CreateCommand();
            cmd.CommandText = "INSERT INTO Borrows VALUES(NULL,@u,@b,@d,NULL,0)";
            cmd.Parameters.AddWithValue("@u", userId);
            cmd.Parameters.AddWithValue("@b", bookId);
            cmd.Parameters.AddWithValue("@d", DateTime.Now.ToString());

            cmd.ExecuteNonQuery();
        }

        public static void ReturnBook(int borrowId, int bookId)
        {
            using var c = new SqliteConnection("Data Source=library.db");
            c.Open();

            var cmd = c.CreateCommand();
            cmd.CommandText = "UPDATE Borrows SET IsReturned=1 WHERE Id=@id";
            cmd.Parameters.AddWithValue("@id", borrowId);
            cmd.ExecuteNonQuery();

            // tăng lại số lượng
            var update = c.CreateCommand();
            update.CommandText = "UPDATE Books SET Quantity = Quantity + 1 WHERE Id=@b";
            update.Parameters.AddWithValue("@b", bookId);
            update.ExecuteNonQuery();
        }


        public static List<Borrow> GetByUser(int userId)
        {
            var list = new List<Borrow>();

            using var c = new SqliteConnection("Data Source=library.db");
            c.Open();

            var cmd = c.CreateCommand();
            cmd.CommandText = @"
    SELECT br.Id, br.BookId, b.Title, br.IsReturned
    FROM Borrows br
    JOIN Books b ON br.BookId = b.Id
    WHERE br.UserId=@u";

            cmd.Parameters.AddWithValue("@u", userId);

            using var r = cmd.ExecuteReader();

            while (r.Read())
            {
                list.Add(new Borrow
                {
                    Id = r.GetInt32(0),
                    BookId = r.GetInt32(1),
                    BookTitle = r.GetString(2),
                    Status = r.GetInt32(3) == 1 ? "Đã trả" : "Đang mượn"
                });
            }

            return list;
        }

        public static List<Borrow> GetAllBorrow()
        {
            var list = new List<Borrow>();

            using var c = new SqliteConnection("Data Source=library.db");
            c.Open();

            var cmd = c.CreateCommand();
            cmd.CommandText = @"
    SELECT br.Id, br.BookId, b.Title,
           IFNULL(br.IsReturned,0)
    FROM Borrows br
    JOIN Books b ON br.BookId = b.Id";

            using var r = cmd.ExecuteReader();

            while (r.Read())
            {
                list.Add(new Borrow
                {
                    Id = r.GetInt32(0),
                    BookId = r.GetInt32(1),
                    BookTitle = r.GetString(2),
                    Status = r.GetInt32(3) == 1 ? "Đã trả" : "Đang mượn"
                });
            }

            return list;
        }
    }
}