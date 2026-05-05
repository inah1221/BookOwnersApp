using System.Threading.Tasks;
using Bupa.BookOwners.Api.Services.Interfaces;
using Bupa.BookOwners.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bupa.BookOwner.Api.Controllers
{
    //[Authorize]
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

        [HttpGet(Name = "GetBooks")]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookOwnersService.GetBooks();
            if (books == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Book list is empty");
            }
            return Ok(books);
        }
    }
}
