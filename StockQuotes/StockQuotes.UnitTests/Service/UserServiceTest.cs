using Moq;
using StockQuotes.Domain;
using StockQuotes.Repository.Interfaces;
using StockQuotes.Service;
using Xunit;

namespace StockQuotes.UnitTests.Service
{
	public class UserServiceTest : AbstractTest
	{
		[Fact]
		public async void Should_ReturnUsers_WhenGetAll()
		{
			var userService = Mocker.CreateInstance<UserService>();

			var userMock = new List<User>();
			userMock.Add(new User() { UserName = "UserName 1", Id = 1 });
			userMock.Add(new User() { UserName = "UserName 2", Id = 2 });

			Mocker.GetMock<IUserRepository>().Setup(x => x.GetAll()).ReturnsAsync(userMock);

			var result = await userService.GetAll();

			Assert.NotNull(result);
			Assert.True(result is IEnumerable<User>);
			Assert.Equal(userMock.Count, result.Count());
		}

		[Fact]
		public async void Should_ReturnTrue_WhenAddNewUser()
		{
			var userService = Mocker.CreateInstance<UserService>();

			var result = await userService.AddUser(new User() { UserName = "UserName 1", Id = 1 });

			Assert.True(result);
		}

		[Fact]
		public async void Should_ReturnTrue_WhenUserNameAndPasswordIsCorrect()
		{
			var userService = Mocker.CreateInstance<UserService>();
			var userMock = new User() { UserName = "UserName 1", Password = "pass", Id = 1 };

			Mocker.GetMock<IUserRepository>().Setup(x => x.GetByUserName(It.IsAny<string>())).ReturnsAsync(userMock);

			var result = await userService.Login(userMock.UserName, userMock.Password);

			Assert.True(result);
		}

		[Fact]
		public async void Should_ReturnFalse_WhenUserNameAndPasswordIsWrong()
		{
			var userService = Mocker.CreateInstance<UserService>();
			var userMock = new User() { UserName = "UserName 1", Password = "pass", Id = 1 };

			Mocker.GetMock<IUserRepository>().Setup(x => x.GetByUserName(It.IsAny<string>())).ReturnsAsync(userMock);

			var result = await userService.Login("WrongUserName", "WrongPassword");

			Assert.False(result);
		}
	}
}