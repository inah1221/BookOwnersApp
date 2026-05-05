using Bupa.BookOwner.Api.Dtos;
using Bupa.BookOwners.Api.Enums;
using Bupa.BookOwners.Api.Extensions;
using Bupa.BookOwners.Api.Services.Interfaces;
using Bupa.BookOwners.Api.ViewModels;

namespace Bupa.BookOwners.Api.Services.Implementations
{
    public class BookOwnersService : IBookOwnersService
    {
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

        public async Task<List<BookByAgeCategoryViewModel>> GetBooks()
        {
            List<BookByAgeCategoryViewModel> bookByAgeCategories =
                new List<BookByAgeCategoryViewModel>();
            var bookOwnersData = await _bookOwnersHttpService.FetchBookOwnersDataAsync();

            if (bookOwnersData != null && bookOwnersData.Count() > 0)
            {
                // TODO: Fix Except
                var childrenBooks = bookOwnersData.Where(x => x.Age <= 17).SelectMany(x => x.Books);
                var adultBooks = bookOwnersData
                    .Where(x => x.Age > 17)
                    .SelectMany(x => x.Books)
                    .Except(childrenBooks);

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
            return bookByAgeCategories;
        }
    }
}
