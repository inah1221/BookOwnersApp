using Bupa.BookOwner.Api.Dtos;

namespace Bupa.BookOwners.Api.HttpServices.Interfaces
{
    /// <summary>
    /// Method using HttpClient to fetch Book Owners data from the API
    /// </summary>
    /// <returns>IEnumerable<BookOwnerDto></returns>
    public interface IBookOwnersHttpService
    {
        Task<List<BookOwnerDto>> FetchBookOwnersDataAsync();
    }
}
