namespace Bupa.BookOwner.Api.Dtos
{
    /// <summary>
    /// Data Transfer Object for books
    /// </summary>
    public class BookDto
    {
        /// <summary>
        /// Name of the book
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Book type (e.g., Hardcover, Paperback)
        /// </summary>
        public string Type { get; set; } = string.Empty;
    }
}
