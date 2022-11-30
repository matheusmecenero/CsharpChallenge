using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StockQuotes.Api.Models;
using StockQuotes.Domain;
using StockQuotes.Service.Interfaces;

namespace StockQuotes.Api
{
    [ApiController]
	[Route("[controller]")]
	public class ChatController : ControllerBase
	{
		private readonly ILogger<ChatController> _logger;
		private readonly IChatService _chatService;
		private readonly IUserService _userService;
		private readonly IMapper _mapper;

		public ChatController(ILogger<ChatController> logger, IChatService chatService, IMapper mapper, IUserService userService)
		{
			_logger = logger;
			_chatService = chatService;
			_mapper = mapper;
			_userService = userService;
		}

		[HttpGet(Name = "GetChat")]
		public async Task<IEnumerable<ChatResponse>> Get()
		{
			return _mapper.Map<IEnumerable<ChatResponse>>(await _chatService.Get());
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] ChatRequest chatRequest)
		{
			try
			{
				if (!await _userService.IsLoggedInAsync(chatRequest.UserId))
					return BadRequest("User must Login.");

				if (string.IsNullOrEmpty(chatRequest.Message))
					return BadRequest("Message is empty.");

				var chat = _mapper.Map<Chat>(chatRequest);

				await _chatService.AddChat(chat);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
			return new OkObjectResult(true);
		}
	}
}