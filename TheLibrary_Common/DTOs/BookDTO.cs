namespace TheLibrary.Common.DTOs;

public class BookDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string ISBN { get; set; } = string.Empty;
    public string Year { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }
    public int LibraryId { get; set; }
    public string LibraryName { get; set; } = string.Empty;
    public int LibraryBranchId { get; set; }
    public string LibraryBranchName { get; set; } = string.Empty;
}
