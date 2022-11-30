using StockQuotes.Domain;

namespace StockQuotes.Repository.Interfaces
{
    public interface IChatRepository
    {
        public Task<bool> Add(Chat chat);
        public Task<IEnumerable<Chat>> Get(int take = 50);
        public Task<Chat?> GetByStockCode(string stockCode);
	}
}