using StockQuotes.Domain;

namespace StockQuotes.Service.Interfaces
{
    public interface IChatService
    {
        public Task AddChat(Chat chat);
        Task<IEnumerable<Chat>> Get(int take = 50);

	}
}