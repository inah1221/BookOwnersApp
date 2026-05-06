namespace Bupa.BookOwner.Api.Dtos
{
    /// <summary>
    /// Data Transfer Object for Book Owners
    /// </summary>
    public class BookOwnerDto
    {
        /// <summary>
        /// Name of the owner
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Age of the owner
        /// </summary>
        public required int Age { get; set; }

        /// <summary>
        /// Books owned
        /// </summary>
        public List<BookDto>? Books { get; set; }
    }
}
