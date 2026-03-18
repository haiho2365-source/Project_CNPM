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
            cmd.CommandText = @"
CREATE TABLE IF NOT EXISTS Users(Id INTEGER PRIMARY KEY, Username TEXT, Password TEXT, Role TEXT);
CREATE TABLE IF NOT EXISTS Books(Id INTEGER PRIMARY KEY, Title TEXT, Author TEXT, Category TEXT, Quantity INTEGER);
CREATE TABLE IF NOT EXISTS Borrows(Id INTEGER PRIMARY KEY, UserId INTEGER, BookId INTEGER, BorrowDate TEXT, ReturnDate TEXT);

INSERT OR IGNORE INTO Users(Id,Username,Password,Role) VALUES(1,'admin','123','Admin');

INSERT OR IGNORE INTO Books(Id,Title,Author,Category,Quantity) VALUES
(1,'Doraemon','Fujiko F Fujio','Manga',10),
(2,'One Piece','Eiichiro Oda','Manga',8),
(3,'Naruto','Masashi Kishimoto','Manga',7),
(4,'Dragon Ball','Akira Toriyama','Manga',6),
(5,'Attack on Titan','Hajime Isayama','Manga',5),

(6,'Harry Potter','J.K. Rowling','Novel',10),
(7,'The Hobbit','J.R.R. Tolkien','Novel',5),
(8,'Lord of the Rings','J.R.R. Tolkien','Novel',4),
(9,'Sherlock Holmes','Arthur Conan Doyle','Detective',6),
(10,'Da Vinci Code','Dan Brown','Thriller',7),

(11,'The Alchemist','Paulo Coelho','Novel',9),
(12,'To Kill a Mockingbird','Harper Lee','Novel',5),
(13,'1984','George Orwell','Dystopian',6),
(14,'Pride and Prejudice','Jane Austen','Romance',4),
(15,'The Great Gatsby','F. Scott Fitzgerald','Classic',5),

(16,'Mắt Biếc','Nguyễn Nhật Ánh','Vietnam',10),
(17,'Tôi Thấy Hoa Vàng Trên Cỏ Xanh','Nguyễn Nhật Ánh','Vietnam',8),
(18,'Đắc Nhân Tâm','Dale Carnegie','Self-help',12),
(19,'Think and Grow Rich','Napoleon Hill','Self-help',7),
(20,'Rich Dad Poor Dad','Robert Kiyosaki','Finance',9);
";
            cmd.ExecuteNonQuery();
        }
    }
}