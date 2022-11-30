using Microsoft.AspNetCore.Mvc;
using Moq;
using StockQuotes.Api;
using StockQuotes.Api.Models;
using StockQuotes.Domain;
using StockQuotes.Service.Interfaces;
using Xunit;

namespace StockQuotes.UnitTests.Api
{
    public class UserControllerTest : AbstractTest
	{
        [Fact]
        public async void Should_ReturnUser_WhenGet()
        {
            var controller = Mocker.CreateInstance<UserController>();

            var userMock = new List<User>();
			userMock.Add(new User { Id = 1, Name = "Matheus" });


			Mocker.GetMock<IUserService>().Setup(x => x.GetAll()).ReturnsAsync(userMock);

			var result = await controller.GetAll();

            Assert.NotNull(result);
            Assert.True(result is OkObjectResult);
		}

		[Fact]
		public async void Should_ReturnBadRequest_WhenCantCreateUser()
		{
			var controller = Mocker.CreateInstance<UserController>();

			Mocker.GetMock<IUserService>().Setup(x => x.AddUser(It.IsAny<User>())).ReturnsAsync(false);

			var result = await controller.Create(new User() { Id = 1, Name = "Matheus"});

			Assert.NotNull(result);
			Assert.True(result is BadRequestObjectResult);
			Assert.True(((BadRequestObjectResult)result).Value?.ToString() == "User can't be created.");
		}

		[Fact]
		public async void Should_ReturnOk_WhenCreateUser()
		{
			var controller = Mocker.CreateInstance<UserController>();

			Mocker.GetMock<IUserService>().Setup(x => x.AddUser(It.IsAny<User>())).ReturnsAsync(true);

			var result = await controller.Create(new User() { Id = 1, Name = "Matheus" });

			Assert.NotNull(result);
			Assert.True(result is OkObjectResult);
		}

		[Fact]
		public async void Should_ReturnBadRequest_WhenLoginInvalid()
		{
			var controller = Mocker.CreateInstance<UserController>();

			Mocker.GetMock<IUserService>().Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);

			var result = await controller.Login(new LoginRequest() { Password = "", UserName = "" });

			Assert.NotNull(result);
			Assert.True(result is BadRequestObjectResult);
			Assert.True(((BadRequestObjectResult)result).Value?.ToString() == "UserName or Password is invalid.");
		}

		[Fact]
		public async void Should_ReturnOk_WhenLoginValid()
		{
			var controller = Mocker.CreateInstance<UserController>();

			Mocker.GetMock<IUserService>().Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);

			var result = await controller.Login(new LoginRequest() { Password = "password", UserName = "username" });

			Assert.NotNull(result);
			Assert.True(result is OkObjectResult);
		}
	}
}