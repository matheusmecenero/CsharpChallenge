namespace StockQuotes.Consumer.Event
{
	public class StockQuoteMessage
	{
		public StockQuoteMessage(string stockCode, List<string> messages)
		{
			StockCode = stockCode;
			Messages = messages;
		}

		public string StockCode { get; set; }
		public List<string> Messages { get; set; }
	}
}
