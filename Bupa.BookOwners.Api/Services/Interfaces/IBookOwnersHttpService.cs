using Bupa.BookOwner.Api.Dtos;

namespace Bupa.BookOwners.Api.Services.Interfaces
{
    // Should be contact instead?
    public interface IBookOwnersHttpService
    {
        Task<IEnumerable<BookOwnerDto>> FetchBookOwnersDataAsync();
    }
}
