using Moq;
using StockQuotes.Domain;
using StockQuotes.Repository;
using StockQuotes.Repository.Interfaces;
using StockQuotes.Service;
using Xunit;

namespace StockQuotes.UnitTests.Repository
{
	public class UserRepositoryTest : AbstractTest
	{
		[Fact]
		public async void Should_ReturnUsers_WhenAddAndGetAll()
		{
			var userRepository = Mocker.CreateInstance<UserRepository>();

			var result = await userRepository.Add(new User() { Id = 1, Name = "Name" });
			var resultGet = await userRepository.GetAll();

			Assert.True(result);
			Assert.Single(resultGet);
		}

		[Fact]
		public async void Should_ReturnUsers_WhenAddAndGetByUserName()
		{
			var userRepository = Mocker.CreateInstance<UserRepository>();
			var userName = "UserName";
			var result = await userRepository.Add(new User() { Id = 1, UserName = userName });
			var resultGet = await userRepository.GetByUserName(userName);

			Assert.True(result);
			Assert.True(resultGet?.UserName == userName);
		}

		[Fact]
		public async void Should_ReturnUsers_WhenAddAndGetById()
		{
			var userRepository = Mocker.CreateInstance<UserRepository>();
			var userName = "UserName";
			var userId = 1;
			var result = await userRepository.Add(new User() { Id = userId, UserName = userName });
			var resultGet = await userRepository.GetById(userId);

			Assert.True(result);
			Assert.True(resultGet?.Id == userId);
		}

		[Fact]
		public async void Should_ReturnTrue_WhenLoginValidUser()
		{
			var userRepository = Mocker.CreateInstance<UserRepository>();
			var userName = "UserName";
			var userId = 1;
			var user = new User() { Id = userId, UserName = userName };

			await userRepository.Add(user);
			var resultLogin = await userRepository.Login(user);

			Assert.True(resultLogin);
		}

		[Fact]
		public async void Should_ReturnFalse_WhenLoginInvalidUser()
		{
			var userRepository = Mocker.CreateInstance<UserRepository>();
			var userName = "UserName";
			var userId = 1;
			var user = new User() { Id = userId, UserName = userName };

			var resultLogin = await userRepository.Login(user);

			Assert.False(resultLogin);
		}
	}
}