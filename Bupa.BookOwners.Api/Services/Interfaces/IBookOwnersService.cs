using Bupa.BookOwner.Api.Dtos;
using Bupa.BookOwners.Api.ViewModels;

namespace Bupa.BookOwners.Api.Services.Interfaces
{
    public interface IBookOwnersService
    {
        Task<List<BookByAgeCategoryViewModel>> GetBooks();
    }
}
