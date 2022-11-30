using Microsoft.AspNetCore.Mvc;
using Moq;
using StockQuotes.Api;
using StockQuotes.Api.Models;
using StockQuotes.Domain;
using StockQuotes.Service.Interfaces;
using Xunit;

namespace StockQuotes.UnitTests.Api
{
    public class ChatControllerTest : AbstractTest
	{
        [Fact]
        public async void Should_ReturnChat_WhenGet()
        {
            var controller = Mocker.CreateInstance<ChatController>();

            var chatMock = new List<Chat>();

            Mocker.GetMock<IChatService>().Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(chatMock);

			var result = await controller.Get();

            Assert.NotNull(result);
            Assert.True(result is IEnumerable<ChatResponse>);
		}

		[Fact]
		public async void Should_ReturnBadRequest_WhenPostLoggedOut()
		{
			var controller = Mocker.CreateInstance<ChatController>();

			Mocker.GetMock<IUserService>().Setup(x => x.IsLoggedInAsync(It.IsAny<int>())).ReturnsAsync(false);

			var result = await controller.Post(new ChatRequest() { Message = "Message", UserId = 1});

			Assert.NotNull(result);
			Assert.True(result is BadRequestObjectResult);
			Assert.True(((BadRequestObjectResult)result).Value?.ToString() == "User must Login.");
		}

		[Fact]
		public async void Should_ReturnBadRequest_WhenPostMessageEmpty()
		{
			var controller = Mocker.CreateInstance<ChatController>();

			Mocker.GetMock<IUserService>().Setup(x => x.IsLoggedInAsync(It.IsAny<int>())).ReturnsAsync(true);

			var result = await controller.Post(new ChatRequest() { Message = string.Empty, UserId = 1 });

			Assert.NotNull(result);
			Assert.True(result is BadRequestObjectResult);
			Assert.True(((BadRequestObjectResult)result).Value?.ToString() == "Message is empty.");
		}

		[Fact]
		public async void Should_ReturnOk_WhenPostMessagesNotEmpty()
		{
			var controller = Mocker.CreateInstance<ChatController>();

			Mocker.GetMock<IUserService>().Setup(x => x.IsLoggedInAsync(It.IsAny<int>())).ReturnsAsync(true);

			var result = await controller.Post(new ChatRequest() { Message = "Message", UserId = 1 });

			Assert.NotNull(result);
			Assert.True(result is OkObjectResult);
		}
	}
}