namespace Bupa.BookOwners.Api.ViewModels
{
    /// <summary>
    /// View model to be sent to the UI for Books
    /// </summary>
    public class BookViewModel
    {
        /// <summary>
        /// Book name
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Book type (e.g., Paperback, Hardcover, Ebook)
        /// </summary>
        public string Type { get; set; } = string.Empty;
    }
}
