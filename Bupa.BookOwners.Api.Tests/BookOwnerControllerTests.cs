using Bupa.BookOwner.Api.Controllers;
using Bupa.BookOwners.Api.HttpServices.Interfaces;
using Bupa.BookOwners.Api.Services.Interfaces;
using Bupa.BookOwners.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Bupa.BookOwners.Api.Tests
{
    public class BookOwnerControllerTests
    {
        private readonly Mock<IBookOwnersService> _mockBookOwnersService;
        private readonly BookOwnerController _bookOwnerController;

        public BookOwnerControllerTests()
        {
            _mockBookOwnersService = new Mock<IBookOwnersService>();
            _bookOwnerController = new BookOwnerController(_mockBookOwnersService.Object);
        }

        [Fact]
        public async Task GetBooks_Successful()
        {
            var adultBooksList = new List<BookViewModel>([
                new BookViewModel { Name = "Gulliver's Travels", Type = "Hardcover" },
                new BookViewModel { Name = "Jane Eyre", Type = "Paperback" },
            ]);

            var childrenBooksList = new List<BookViewModel>([
                new BookViewModel { Name = "Great Expectations", Type = "Hardcover" },
                new BookViewModel { Name = "Hamlet", Type = "Paperback" },
            ]);

            var expectedBooks = new List<BookByAgeCategoryViewModel>([
                new BookByAgeCategoryViewModel
                {
                    OwnerAgeCategory = "Adults",
                    BooksByAge = adultBooksList,
                },
                new BookByAgeCategoryViewModel
                {
                    OwnerAgeCategory = "Children",
                    BooksByAge = childrenBooksList,
                },
            ]);

            _mockBookOwnersService.Setup(s => s.GetBooks()).ReturnsAsync(expectedBooks);

            var result = await _bookOwnerController.GetBooks();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var item = Assert.IsType<List<BookByAgeCategoryViewModel>>(okResult.Value);
            Assert.NotNull(item);
            Assert.Equal("Adults", item[0].OwnerAgeCategory);
            Assert.Equal("Gulliver's Travels", item[0].BooksByAge[0].Name);
            Assert.Equal("Children", item[1].OwnerAgeCategory);
            Assert.Equal("Great Expectations", item[1].BooksByAge[0].Name);
        }

        [Fact]
        public async Task GetBooks_Unsuccessful()
        {
            _mockBookOwnersService.Setup(s => s.GetBooks()).ReturnsAsync([]);

            var result = await _bookOwnerController.GetBooks();
            var badRequestResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }
    }
}
