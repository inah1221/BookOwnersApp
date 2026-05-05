namespace Bupa.BookOwner.Api.Dtos
{
    public class BookOwnerDto 
    {
        /// <summary>
        /// Name of the owner
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Age of the owner
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Books owned
        /// </summary>
        public List<BookDto> Books { get; set; }
    }
}
