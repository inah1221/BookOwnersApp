namespace Bupa.BookOwners.Api.ViewModels
{
    public class BookByAgeCategoryViewModel
    {
        public string OwnerAgeCategory { get; set; }
        public List<BookViewModel> BooksByAge { get; set; }
    }
}
