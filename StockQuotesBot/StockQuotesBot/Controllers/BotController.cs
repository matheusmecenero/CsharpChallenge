using Microsoft.AspNetCore.Mvc;
using StockQuotesBot.Service;
using StockQuotesBot.Service.Event;
using StockQuotesBot.Service.Interface;

namespace StockQuotesBot.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class BotController : ControllerBase
	{
		private readonly ILogger<BotController> _logger;
		private readonly IBotService _botService;

		public BotController(ILogger<BotController> logger, IBotService botService)
		{
			_logger = logger;
			_botService = botService;
		}

		[HttpPost]
		public ActionResult Post(string stock, string csvContent)
		{
			try
			{
				_botService.SendMessage(new StockQuoteMessage(stock, csvContent.Split(',').ToList()));
				return new OkObjectResult(true);
			}
			catch (Exception ex) 
			{
				_logger.LogError(ex, ex.Message);
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}
	}
}