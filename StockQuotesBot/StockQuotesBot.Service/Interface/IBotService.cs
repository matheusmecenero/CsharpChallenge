using StockQuotesBot.Service.Event;

namespace StockQuotesBot.Service.Interface
{
	public interface IBotService
	{
		public void SendMessage(StockQuoteMessage stockQuoteMessage);
	}
}
