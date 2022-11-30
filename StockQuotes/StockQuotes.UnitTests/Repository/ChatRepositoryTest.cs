using Moq;
using StockQuotes.Domain;
using StockQuotes.Repository;
using StockQuotes.Repository.Interfaces;
using StockQuotes.Service;
using Xunit;

namespace StockQuotes.UnitTests.Repository
{
	public class ChatRepositoryTest : AbstractTest
	{
		[Fact]
		public async void Should_ReturnChats_WhenAddAndGet()
		{
			var chatRepository = Mocker.CreateInstance<ChatRepository>();

			var result = await chatRepository.Add(new Chat() { Message = "Message 1" });
			var resultGet = await chatRepository.Get();

			Assert.True(result);
			Assert.Single(resultGet);
		}

		[Fact]
		public async void Should_ReturnChats_WhenUseTake()
		{
			var chatRepository = Mocker.CreateInstance<ChatRepository>();

			var take = 2;

			await chatRepository.Add(new Chat() { Message = "Message 1" });
			await chatRepository.Add(new Chat() { Message = "Message 2" });
			await chatRepository.Add(new Chat() { Message = "Message 3" });
			var resultGet = await chatRepository.Get(take);

			Assert.Equal(take, resultGet.Count());
		}

		[Fact]
		public async void Should_ReturnChat_WhenGetByStockCode()
		{
			var chatRepository = Mocker.CreateInstance<ChatRepository>();

			var stockCode = "APPL.US";

			await chatRepository.Add(new Chat() { Message = "Message 1", StockCode = stockCode });

			var result = await chatRepository.GetByStockCode(stockCode);

			Assert.NotNull(result);
			Assert.True(result?.StockCode == stockCode);
		}
	}
}