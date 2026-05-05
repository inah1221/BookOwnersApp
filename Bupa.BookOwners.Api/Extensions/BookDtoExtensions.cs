using Bupa.BookOwner.Api.Dtos;
using Bupa.BookOwners.Api.ViewModels;

namespace Bupa.BookOwners.Api.Extensions
{
    public static class BookDtoExtensions
    {
        public static BookViewModel ToViewModel(this BookDto bookDto)
        {
            return new BookViewModel { Name = bookDto.Name, Type = bookDto.Type };
        }
    }
}
