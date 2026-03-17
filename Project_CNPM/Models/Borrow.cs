namespace Library.Models
{
    public class Borrow
    {
        public int Id { get; set; }
        public int BookId { get; set; }   // BẮT BUỘC
        public string BookTitle { get; set; }
        public string Status { get; set; }
    }
}