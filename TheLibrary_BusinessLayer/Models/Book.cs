namespace TheLibrary_BusinessLayer.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public bool IsAvailable { get; set; }
    }
}
