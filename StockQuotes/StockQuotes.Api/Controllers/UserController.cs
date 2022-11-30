using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using StockQuotes.Api.Models;
using StockQuotes.Domain;
using StockQuotes.Service.Interfaces;

namespace StockQuotes.Api
{
	[ApiController]
	[Route("[controller]")]
	public class UserController : ControllerBase
	{
		private readonly ILogger<UserController> _logger;
		private readonly IUserService _userService;
		private readonly IValidator<User> _userValidator;
		private readonly IValidator<LoginRequest> _loginRequestValidator;

		public UserController(ILogger<UserController> logger, 
							  IUserService userService,
							  IValidator<User> userValidator,
							  IValidator<LoginRequest> loginRequestValidator)
		{
			_logger = logger;
			_userService = userService;
			_userValidator = userValidator;
			_loginRequestValidator = loginRequestValidator;
		}

		[HttpGet("GetAll")]
		public async Task<IActionResult> GetAll()
		{
			var result = await _userService.GetAll();

			if (result.Count() > 0)
				return Ok(result);
			return new NoContentResult();
		}

		[HttpPost("Create")]
		public async Task<IActionResult> Create([FromBody] User user)
		{
			try
			{
				var validations = await _userValidator.ValidateAsync(user);

				if (validations != null && validations.Errors.Any())
					return BadRequest(validations.Errors.Select(x => x.ErrorMessage));

				if (!await _userService.AddUser(user))
					return BadRequest("User can't be created.");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				return StatusCode(500);
			}
			return new OkObjectResult(true);
		}

		[HttpPut("Login")]
		public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
		{
			try
			{
				var validations = await _loginRequestValidator.ValidateAsync(loginRequest);

				if (validations != null && validations.Errors.Any())
					return BadRequest(validations.Errors.Select(x => x.ErrorMessage));

				if (!await _userService.Login(loginRequest.UserName, loginRequest.Password))
					return BadRequest("UserName or Password is invalid.");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				return StatusCode(500);
			}
			return new OkObjectResult(true);
		}
	}
}