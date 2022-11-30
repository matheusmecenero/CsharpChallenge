using System.Text.Json.Serialization;

namespace StockQuotes.Domain
{
	public class User
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public bool IsLogged { get; set; }
		public string? UserName { get; set; }
		public string? Password { get; set; }
	}
}
