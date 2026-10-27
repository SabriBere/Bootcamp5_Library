namespace TheLibrary_DataAccessLayer.Models
{
    public class Book //Esto represnta un ejemplar
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public bool IsAvailable { get; set; }

        //Navegation property
        public int LibrayBranchId { get; set; }
        public LibrayBranch LibrayBranch { get; set; }
    }
}
