namespace StockQuotes.Domain
{
	public class Chat
	{
		public string? Message { get; set; }
		public DateTime DateTime { get; set; } = DateTime.Now;
		public User User { get; set; } = new User();
		public string? StockCode { get; set; }
	}
}