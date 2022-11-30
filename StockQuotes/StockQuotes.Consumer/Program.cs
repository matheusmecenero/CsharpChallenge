using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StockQuotes.Api.Models;
using StockQuotes.Consumer.Event;
using System.Net.Http.Json;
using System.Text;

namespace StockQuotes.Consumer
{
	public class Program
	{
		private const int botId = 9999;
		private const string apiChat = "http://localhost:5070/";
		private const string hostName = "localhost";
		private const string botQueue = "BotQueue";

		static void Main(string[] args)
		{
			try
			{
				HttpClient client = new HttpClient();
				client.BaseAddress = new Uri(apiChat);

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

						var consumer = new EventingBasicConsumer(channel);
						consumer.Received += (model, ea) =>
						{
							var body = ea.Body.ToArray();
							var messageEndogin = Encoding.UTF8.GetString(body);

							var stockQuoteMessage = JsonConvert.DeserializeObject<StockQuoteMessage>(messageEndogin);

							if (stockQuoteMessage == null)
								return;

							if (stockQuoteMessage.Messages.Count <= 0)
								return;

							foreach (string message in stockQuoteMessage.Messages)
							{
								client.PostAsJsonAsync("chat",
									new ChatRequest()
									{
										StockCode = stockQuoteMessage.StockCode,
										Message = message,
										UserId = botId
									});
							}
						};

						channel.BasicConsume(queue: botQueue,
							autoAck: true,
							consumer: consumer);

						Console.ReadLine();
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}
	}
}