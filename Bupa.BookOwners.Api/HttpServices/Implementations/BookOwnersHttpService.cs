using Bupa.BookOwner.Api.Dtos;
using Bupa.BookOwners.Api.HttpServices.Interfaces;

namespace Bupa.BookOwners.Api.HttpServices.Implementations
{
    public class BookOwnersHttpService : IBookOwnersHttpService
    {
        private readonly ILogger _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _baseUrl;

        public BookOwnersHttpService(
            IConfiguration configuration,
            ILogger<BookOwnersHttpService> logger,
            IHttpClientFactory httpClientFactory
        )
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;

            _baseUrl = configuration["Apis:BaseUrl"] ?? string.Empty;
        }

        /// <summary>
        /// Method using HttpClient to fetch Book Owners data from the API
        /// </summary>
        /// <returns>IEnumerable<BookOwnerDto></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<IEnumerable<BookOwnerDto>> FetchBookOwnersDataAsync()
        {
            try
            {
                if (_baseUrl == null)
                {
                    string missingConfigMessage = "Book Owners URL is required";
                    _logger.LogError(missingConfigMessage);
                    throw new InvalidOperationException(missingConfigMessage);
                }

                Uri uri = new Uri($"{_baseUrl}/bookowners");
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
                var client = _httpClientFactory.CreateClient();
                var response = await client.SendAsync(request);

                response.EnsureSuccessStatusCode();
                var mediaType = response.Content.Headers.ContentType?.MediaType;

                if (
                    response.Content == null
                    || response.Content.Headers.ContentLength == 0
                    || mediaType != "application/json"
                )
                {
                    string errorMessage = $"Response body is empty or in an invalid format.";
                    _logger.LogError(errorMessage);
                    throw new InvalidOperationException(errorMessage);
                }

                var result = await response.Content.ReadFromJsonAsync<List<BookOwnerDto>>();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in retrieving Book Owners data");
                throw;
            }
        }
    }
}
