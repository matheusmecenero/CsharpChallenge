using Moq;
using StockQuotes.Domain;
using StockQuotes.Repository.Interfaces;
using StockQuotes.Service;
using Xunit;

namespace StockQuotes.UnitTests.Service
{
	public class ChatServiceTest : AbstractTest
	{
		[Fact]
		public async void Should_ReturnChats_WhenGet()
		{
			var chatService = Mocker.CreateInstance<ChatService>();

			var chatMock = new List<Chat>();
			chatMock.Add(new Chat() { Message = "Message" });

			Mocker.GetMock<IChatRepository>().Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(chatMock);

			var result = await chatService.Get();

			Assert.NotNull(result);
			Assert.True(result is IEnumerable<Chat>);
			Assert.Equal(chatMock.Count, result.Count());
		}

		[Fact]
		public async void Should_AddChar_WhenAddNewChat()
		{
			var chatService = Mocker.CreateInstance<ChatService>();

			var chatMock = new List<Chat>();
			var chatFirst = new Chat() { Message = "Message 1", User = new User() { Id = 1 } };
			var chatSecond = new Chat() { Message = "Message 1", User = new User() { Id = 1 } };

			chatMock.Add(chatFirst);
			chatMock.Add(chatSecond);

			Mocker.GetMock<IChatRepository>().Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(chatMock);

			await chatService.AddChat(chatFirst);
			await chatService.AddChat(chatSecond);

			var result = await chatService.Get();

			Assert.NotNull(result);
			Assert.Equal(2, result.Count());
		}

		[Fact]
		public async void Should_AddCharStockCommand_WhenUserPostChatStock()
		{
			var chatService = Mocker.CreateInstance<ChatService>();

			var chatMock = new List<Chat>();
			var chatFirst = new Chat() { Message = "APPL.US quote is $10 per share", User = new User() { Id = 1 } };
			var chatSecond = new Chat() { Message = "/stock=APPL.US", User = new User() { Id = 1 } };
			var chatThird = new Chat() { Message = "APPL.US quote is $10 per share", User = new User() { Id = 1 } };

			chatMock.Add(chatFirst);
			chatMock.Add(chatSecond);
			chatMock.Add(chatThird);

			Mocker.GetMock<IChatRepository>().Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(chatMock);
			Mocker.GetMock<IChatRepository>().Setup(x => x.GetByStockCode(It.IsAny<string>())).ReturnsAsync(chatFirst);

			await chatService.AddChat(chatFirst);
			await chatService.AddChat(chatSecond);

			Mocker.GetMock<IChatRepository>().Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(chatMock);

			var result = await chatService.Get();

			Assert.NotNull(result);
			Assert.Equal(chatMock.Count, result.Count());
		}
	}
}