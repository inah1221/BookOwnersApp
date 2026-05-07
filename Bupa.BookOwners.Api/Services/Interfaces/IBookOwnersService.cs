using Bupa.BookOwner.Api.Dtos;
using Bupa.BookOwners.Api.ViewModels;

namespace Bupa.BookOwners.Api.Services.Interfaces
{
    public interface IBookOwnersService
    {
        /// <summary>
        /// Method that calls HttpClient to get books and transform to View Model
        /// </summary>
        /// <returns>List of books by categroy</returns>
        Task<List<BookByAgeCategoryViewModel>> GetBooks();
    }
}
