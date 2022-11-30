using StockQuotes.Api.Validators;
using StockQuotes.Domain;
using Xunit;

namespace StockQuotes.UnitTests.Validators
{
	public class UserValidatorTest : AbstractTest
	{
		[Fact]
		public void Should_ReturnError_WhenAllInvalidData()
		{
			var validator = new UserValidator();

			var result = validator.Validate(
				new User
				{
					Id = 0,
					IsLogged = true,
					Name = "",
					Password = "",
					UserName = ""
				});

			Assert.False(result.IsValid);
			Assert.Equal(4, result.Errors.Count);
			Assert.Contains(result.Errors, x => x.ErrorMessage == "Name is Empty.");
			Assert.Contains(result.Errors, x => x.ErrorMessage == "Id must be greather than 0.");
			Assert.Contains(result.Errors, x => x.ErrorMessage == "UserName is Empty.");
			Assert.Contains(result.Errors, x => x.ErrorMessage == "Password is Empty.");
		}

		[Fact]
		public void Should_ReturnError_WhenUserNameAndPasswordWhiteSpaces()
		{
			var validator = new UserValidator();

			var result = validator.Validate(
				new User
				{
					Id = 1,
					IsLogged = true,
					Name = "Name",
					Password = "1 2",
					UserName = "user name"
				});

			Assert.False(result.IsValid);
			Assert.Equal(2, result.Errors.Count);
			Assert.Contains(result.Errors, x => x.ErrorMessage == "UserName can't contain white spaces.");
			Assert.Contains(result.Errors, x => x.ErrorMessage == "Password can't contain white spaces.");
		}

		[Fact]
		public void Should_ReturnSucess_WhenUserNameAndPasswordValid()
		{
			var validator = new UserValidator();

			var result = validator.Validate(
				new User
				{
					Id = 1,
					IsLogged = true,
					Name = "User Name",
					Password = "mypassword",
					UserName = "myusername"
				});
			Assert.True(result.IsValid);
			Assert.Empty(result.Errors);
		}
	}
}