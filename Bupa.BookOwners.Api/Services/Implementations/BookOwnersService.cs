using Bupa.BookOwners.Api.Enums;
using Bupa.BookOwners.Api.Extensions;
using Bupa.BookOwners.Api.HttpServices.Interfaces;
using Bupa.BookOwners.Api.Services.Interfaces;
using Bupa.BookOwners.Api.ViewModels;

namespace Bupa.BookOwners.Api.Services.Implementations
{
    public class BookOwnersService : IBookOwnersService
    {
        private const int BOUNDARY_AGE = 18;

        private readonly ILogger<BookOwnersService> _logger;
        private readonly IBookOwnersHttpService _bookOwnersHttpService;

        public BookOwnersService(
            ILogger<BookOwnersService> logger,
            IBookOwnersHttpService bookOwnersHttpService
        )
        {
            _logger = logger;
            _bookOwnersHttpService = bookOwnersHttpService;
        }

        /// <summary>
        /// Method that calls Book Owners Http Service to get books and transform to View Model
        /// </summary>
        /// <returns>List of books by category</returns>
        public async Task<List<BookByAgeCategoryViewModel>> GetBooks()
        {
            List<BookByAgeCategoryViewModel> bookByAgeCategories = [];
            var bookOwnersData = await _bookOwnersHttpService.FetchBookOwnersDataAsync();

            if (bookOwnersData != null && bookOwnersData.Count() > 0)
            {
                var childrenBooks = bookOwnersData
                    .Where(x => x.Age < BOUNDARY_AGE && x.Books != null)
                    .SelectMany(x => x.Books);
                var adultBooks = bookOwnersData
                    .Where(x => x.Age >= BOUNDARY_AGE && x.Books != null)
                    .SelectMany(x => x.Books)
                    .Where(x => !childrenBooks.Any(y => y.Name == x.Name));

                bookByAgeCategories.Add(
                    new BookByAgeCategoryViewModel
                    {
                        OwnerAgeCategory = nameof(AgeCategoryType.Adults),
                        BooksByAge =
                        [
                            .. adultBooks.OrderBy(x => x.Name).Select(x => x.ToViewModel()),
                        ],
                    }
                );

                bookByAgeCategories.Add(
                    new BookByAgeCategoryViewModel
                    {
                        OwnerAgeCategory = nameof(AgeCategoryType.Children),
                        BooksByAge =
                        [
                            .. childrenBooks.OrderBy(x => x.Name).Select(x => x.ToViewModel()),
                        ],
                    }
                );
            }
            else
            {
                _logger.LogInformation("Book Owners list is empty.");
            }
            return bookByAgeCategories;
        }
    }
}
