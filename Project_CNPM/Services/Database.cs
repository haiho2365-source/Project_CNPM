using Microsoft.Data.Sqlite;

namespace Project_CNPM.Services
{
    public class Database
    {
        public static void Initialize()
        {
            using var connection = new SqliteConnection("Data Source=library.db");
            connection.Open();

            var cmd = connection.CreateCommand();

            cmd.CommandText = @"
CREATE TABLE IF NOT EXISTS Users(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Username TEXT,
    Password TEXT,
    Role TEXT
IsReturned INTEGER
);

CREATE TABLE IF NOT EXISTS Books(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Title TEXT,
    Author TEXT,
    Category TEXT,
    Quantity INTEGER
IsReturned INTEGER
);

CREATE TABLE IF NOT EXISTS Borrows(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId INTEGER,
    BookId INTEGER,
    BorrowDate TEXT,
    ReturnDate TEXT,
    IsReturned INTEGER
);

-- ADMIN mặc định
INSERT INTO Users (Username, Password, Role)
SELECT 'admin','123','Admin'
WHERE NOT EXISTS (SELECT 1 FROM Users WHERE Username='admin');

-- DATA SÁCH MẪU
INSERT INTO Books (Title, Author, Category, Quantity)
SELECT 'C# Basic','Microsoft','IT',5
WHERE NOT EXISTS (SELECT 1 FROM Books);

INSERT INTO Books (Title, Author, Category, Quantity) VALUES
('Java Core','Oracle','IT',4),
('Python Intro','Guido','IT',6),
('Data Structures','Mark','IT',3),
('AI Basics','Andrew','IT',2),
('Machine Learning','Tom','IT',5),
('Math 1','Nguyen','Math',7),
('Math 2','Tran','Math',6),
('Linear Algebra','Le','Math',4),
('Calculus','Pham','Math',5),
('Physics','Einstein','Science',3),
('Chemistry','Curie','Science',2),
('Biology','Darwin','Science',4),
('English 1','John','Language',6),
('English 2','Anna','Language',5),
('History VN','Bao','History',4),
('World History','Smith','History',3),
('Design Pattern','GoF','IT',2),
('SQL Guide','Oracle','IT',5),
('Networking','Cisco','IT',3);
";

            cmd.ExecuteNonQuery();
        }
    }
}