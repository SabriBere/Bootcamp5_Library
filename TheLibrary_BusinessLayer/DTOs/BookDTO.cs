namespace TheLibrary_BusinessLayer.DTOs
{
    public class BookDTO
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public bool IsAvailable { get; set; }
        public int LibraryId { get; set; }
        public string LibraryName { get; set; }
        public int LibraryBranchId { get; set; }
        public string LibraryBranchName { get; set; }
    }
}
