namespace Bupa.BookOwner.Api.Dtos
{
    public class BookDto
    {
        /// <summary>
        /// Name of the book
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Book type (e.g., Hardcover, Paperback)
        /// </summary>
        public string Type { get; set; }
    }
}
