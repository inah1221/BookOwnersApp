using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Bupa.BookOwner.Api.Controllers;
using Bupa.BookOwner.Api.Dtos;
using Bupa.BookOwners.Api.HttpServices.Implementations;
using Bupa.BookOwners.Api.HttpServices.Interfaces;
using Bupa.BookOwners.Api.Services.Interfaces;
using Bupa.BookOwners.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;

namespace Bupa.BookOwners.Api.Tests
{
    public class BookOwnersHttpServiceTests
    {
        private readonly Mock<ILogger<BookOwnersHttpService>> _mockLogger;
        private readonly Mock<IHttpClientFactory> _mockClientFactory;
        private readonly Mock<IConfiguration> _mockConfig;
        private readonly BookOwnersHttpService _bookOwnersHttpService;

        public BookOwnersHttpServiceTests()
        {
            _mockLogger = new Mock<ILogger<BookOwnersHttpService>>();
            _mockClientFactory = new Mock<IHttpClientFactory>();
            _mockConfig = new Mock<IConfiguration>();
            _mockConfig
                .Setup(x => x["Apis:BaseUrl"])
                .Returns("https://digitalcodingtest.bupa.com.au/api/v1");
            _bookOwnersHttpService = new BookOwnersHttpService(
                _mockConfig.Object,
                _mockLogger.Object,
                _mockClientFactory.Object
            );
        }

        [Fact]
        public async Task FetchBookOwnersDataAsync_Successful()
        {
            var bookOwnerDto = new List<BookOwnerDto>([
                new BookOwnerDto
                {
                    Name = "Jane",
                    Age = 23,
                    Books = new List<BookDto>([
                        new BookDto { Name = "Hamlet", Type = "Hardcover" },
                    ]),
                },
            ]);

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(
                    new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = JsonContent.Create(bookOwnerDto),
                    }
                );

            var httpClient = new HttpClient(handlerMock.Object);

            _mockClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var result = await _bookOwnersHttpService.FetchBookOwnersDataAsync();
            Assert.NotNull(result);
            Assert.Equal("Jane", result[0].Name);
        }

        [Fact]
        public async Task FetchBookOwnersDataAsync_DifferentContentType_Returns_Error()
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(
                    new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent(""),
                    }
                );

            var httpClient = new HttpClient(handlerMock.Object);

            _mockClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

            await Assert.ThrowsAsync<InvalidOperationException>(
                _bookOwnersHttpService.FetchBookOwnersDataAsync
            );
        }

        [Fact]
        public async Task FetchBookOwnersDataAsync_TooManyRequests_Returns_Error()
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(
                    new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.TooManyRequests,
                        Content = JsonContent.Create("Too many requests"),
                    }
                );

            var httpClient = new HttpClient(handlerMock.Object);

            _mockClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var ex = await Assert.ThrowsAsync<HttpRequestException>(
                _bookOwnersHttpService.FetchBookOwnersDataAsync
            );
            Assert.Equal(HttpStatusCode.TooManyRequests, ex.StatusCode);
        }
    }
}
