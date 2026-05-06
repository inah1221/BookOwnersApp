namespace Bupa.BookOwners.Api.ViewModels
{
    /// <summary>
    /// View model to be used by the UI for Books by Age Category
    /// </summary>
    public class BookByAgeCategoryViewModel
    {
        /// <summary>
        /// Owner Age Category (e.g., Adults, Children)
        /// </summary>
        public required string OwnerAgeCategory { get; set; }

        /// <summary>
        /// List of Books by Age Category
        /// </summary>
        public List<BookViewModel>? BooksByAge { get; set; }
    }
}
