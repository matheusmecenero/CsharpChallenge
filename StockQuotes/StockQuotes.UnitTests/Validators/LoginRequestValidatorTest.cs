using StockQuotes.Api.Models;
using StockQuotes.Api.Validators;
using Xunit;

namespace StockQuotes.UnitTests.Validators
{
	public class LoginRequestValidatorTest : AbstractTest
	{
		[Fact]
		public void Should_ReturnError_WhenUserNameAndPasswordEmpty()
		{
			var validator = new LoginRequestValidator();

			var result = validator.Validate(
				new LoginRequest
				{
					Password = "",
					UserName = ""
				});

			Assert.False(result.IsValid);
			Assert.Equal(2, result.Errors.Count);
			Assert.Contains(result.Errors, x => x.ErrorMessage == "UserName is Empty.");
			Assert.Contains(result.Errors, x => x.ErrorMessage == "Password is Empty.");
		}

		[Fact]
		public void Should_ReturnError_WhenUserNameAndPasswordWhiteSpaces()
		{
			var validator = new LoginRequestValidator();

			var result = validator.Validate(
				new LoginRequest
				{
					Password = "a a",
					UserName = "b b"
				});

			Assert.False(result.IsValid);
			Assert.Equal(2, result.Errors.Count);
			Assert.Contains(result.Errors, x => x.ErrorMessage == "UserName can't contain white spaces.");
			Assert.Contains(result.Errors, x => x.ErrorMessage == "Password can't contain white spaces.");
		}

		[Fact]
		public void Should_ReturnSucess_WhenUserNameAndPasswordValid()
		{
			var validator = new LoginRequestValidator();

			var result = validator.Validate(
				new LoginRequest
				{
					Password = "password",
					UserName = "matheus"
				});

			Assert.True(result.IsValid);
			Assert.Empty(result.Errors);
		}
	}
}