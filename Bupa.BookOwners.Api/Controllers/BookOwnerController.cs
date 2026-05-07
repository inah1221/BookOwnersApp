using System.Threading.Tasks;
using Bupa.BookOwners.Api.Enums;
using Bupa.BookOwners.Api.Services.Interfaces;
using Bupa.BookOwners.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bupa.BookOwner.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/BookOwner")]
    public class BookOwnerController : ControllerBase
    {
        private readonly ILogger<BookOwnerController> _logger;
        private readonly IBookOwnersService _bookOwnersService;

        public BookOwnerController(
            ILogger<BookOwnerController> logger,
            IBookOwnersService bookOwnerService
        )
        {
            _logger = logger;
            _bookOwnersService = bookOwnerService;
        }

        /// <summary>
        /// Gets list of Books categorize by Owner's Age category, ordered alphabetically
        /// </summary>
        [HttpGet(nameof(GetBooks))]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookOwnersService.GetBooks();
            if (books == null || books.Count() == 0)
            {
                var message = "Book list is empty";
                _logger.LogError(message);
                return StatusCode(StatusCodes.Status400BadRequest, message);
            }
            return Ok(books);
        }

        /// <summary>
        /// Gets Book Types
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetBookTypes))]
        public IActionResult GetBookTypes()
        {
            var bookTypes = Enum.GetNames(typeof(BookType)).ToList();
            return Ok(bookTypes);
        }
    }
}
