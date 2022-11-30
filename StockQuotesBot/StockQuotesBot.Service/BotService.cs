using Newtonsoft.Json;
using RabbitMQ.Client;
using StockQuotesBot.Service.Event;
using StockQuotesBot.Service.Interface;
using System.Text;

namespace StockQuotesBot.Service
{
	public class BotService : IBotService
	{
		private const string hostName = "localhost";
		private const string botQueue = "BotQueue";

		public void SendMessage(StockQuoteMessage stockQuoteMessage)
		{			
			var factory = new ConnectionFactory() { HostName = hostName };
			using (var connection = factory.CreateConnection())
			{
				using (var channel = connection.CreateModel())
				{
					channel.QueueDeclare(queue: botQueue,
						durable: false,
						exclusive: false,
						autoDelete: false,
						arguments: null);

					String json = JsonConvert.SerializeObject(stockQuoteMessage);
					byte[] customerBuffer = Encoding.UTF8.GetBytes(json);

					channel.BasicPublish(exchange: string.Empty,
						routingKey: botQueue,
						basicProperties: null,
						body: customerBuffer);
				}
			}
		}
	}
}