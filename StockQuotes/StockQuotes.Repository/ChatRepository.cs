using StockQuotes.Domain;
using StockQuotes.Repository.Interfaces;

namespace StockQuotes.Repository
{
    public class ChatRepository : IChatRepository
	{
		private readonly List<Chat> chats;

		public ChatRepository()
		{
			this.chats = this.chats ?? new List<Chat>();
		}

		public async Task<bool> Add(Chat chat)
		{
			chats.Add(chat);
			return await Task.FromResult(true);
		}

		public async Task<IEnumerable<Chat>> Get(int take = 50)
		{
			return await Task.FromResult(chats.OrderBy(p => p.DateTime).Take(take));
		}

		public async Task<Chat?> GetByStockCode(string stockCode)
		{
			return await Task.FromResult(chats.OrderByDescending(p => p.DateTime).FirstOrDefault(p => p.StockCode == stockCode));
		}
	}
}