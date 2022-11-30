namespace StockQuotes.Api.Models
{
	public class ChatRequest
	{
		public string? Message { get; set; }
		public int UserId { get; set; }
		public string? StockCode { get; set;}
	}
}