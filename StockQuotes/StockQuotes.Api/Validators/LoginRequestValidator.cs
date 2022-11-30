using FluentValidation;
using StockQuotes.Api.Models;

namespace StockQuotes.Api.Validators
{
	public class LoginRequestValidator : AbstractValidator<LoginRequest>
	{
		public LoginRequestValidator() 
		{
			RuleFor(user => user.UserName)
				.NotEmpty()
				.WithMessage("UserName is Empty.");

			RuleFor(user => user.UserName)
				.Must(p => p != null && !p.Any(x => Char.IsWhiteSpace(x)))
				.WithMessage("UserName can't contain white spaces.");

			RuleFor(user => user.Password)
				.NotEmpty()
				.WithMessage("Password is Empty.");

			RuleFor(user => user.Password)
				.Must(p => p != null && !p.Any(x => Char.IsWhiteSpace(x)))
				.WithMessage("Password can't contain white spaces.");
		}
	}
}