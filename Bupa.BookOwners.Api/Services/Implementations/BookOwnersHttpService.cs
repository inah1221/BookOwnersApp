using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Bupa.BookOwner.Api.Dtos;
using Bupa.BookOwners.Api.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Bupa.BookOwners.Api.Services.Implementations
{
    public class BookOwnersHttpService : IBookOwnersHttpService
    {
        private readonly ILogger _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _bookOwnersUrl;

        public BookOwnersHttpService(
            IConfiguration configuration,
            ILogger<BookOwnersHttpService> logger,
            IHttpClientFactory httpClientFactory
        )
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;

            _bookOwnersUrl = configuration["Apis:BookOwnersUrl"] ?? string.Empty;
        }

        public async Task<IEnumerable<BookOwnerDto>> FetchBookOwnersDataAsync()
        {
            try
            {
                if (_bookOwnersUrl == null)
                {
                    string missingConfigMessage = "Book Owners URL is required";
                    _logger.LogError(missingConfigMessage);
                    // TODO: Update to a more specific configuration exception?
                    throw new InvalidOperationException(missingConfigMessage);
                }

                Uri uri = new Uri(_bookOwnersUrl);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
                // Can add authorization headers here
                var client = _httpClientFactory.CreateClient();
                var response = await client.SendAsync(request);

                response.EnsureSuccessStatusCode();
                var mediaType = response.Content.Headers.ContentType?.MediaType;

                if (mediaType == "application/json")
                {
                    var result = await response.Content.ReadFromJsonAsync<List<BookOwnerDto>>();
                    return result;
                }
                else
                {
                    string errorMessage = $"Response body in an invalid format.";
                    _logger.LogError(errorMessage);
                    throw new InvalidOperationException(errorMessage);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in retrieving Book Owners data");
                throw;
            }
        }
    }
}
