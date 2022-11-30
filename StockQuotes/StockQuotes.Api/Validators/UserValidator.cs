using FluentValidation;
using StockQuotes.Domain;

namespace StockQuotes.Api.Validators
{
	public class UserValidator : AbstractValidator<User>
	{
		public UserValidator() 
		{ 
			RuleFor(user => user.Password)
				.NotEmpty()
				.WithMessage("Password is Empty.");

			RuleFor(user => user.Password)
				.Must(p => p != null && !p.Any(x => Char.IsWhiteSpace(x)))
				.WithMessage("Password can't contain white spaces.");

			RuleFor(user => user.UserName)
				.NotEmpty()
				.WithMessage("UserName is Empty.");

			RuleFor(user => user.UserName)
				.Must(u => u!= null && !u.Any(x => Char.IsWhiteSpace(x)))
				.WithMessage("UserName can't contain white spaces.");

			RuleFor(user => user.Name)
				.NotEmpty()
				.WithMessage("Name is Empty.");

			RuleFor(user => user.Id)
				.GreaterThan(0)
				.WithMessage("Id must be greather than 0.");
		}
	}
}