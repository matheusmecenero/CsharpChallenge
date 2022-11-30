using StockQuotes.Domain;
using StockQuotes.Repository.Interfaces;
using StockQuotes.Service.Interfaces;

namespace StockQuotes.Service
{
    public class UserService : IUserService
	{	
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository) 
		{
			_userRepository = userRepository;
		}

		public async Task<bool> AddUser(User user)
		{
			if (user is null || user.UserName is null)
				return false;

			var userByUserName = await _userRepository.GetByUserName(user.UserName);
			if (userByUserName != null)
				return false;

			var userById = await _userRepository.GetById(user.Id);
			if (userById != null)
				return false;

			await _userRepository.Add(user);
			return true;
		}

		public async Task<IEnumerable<User>> GetAll()
		{
			return await _userRepository.GetAll();
		}

		public async Task<bool> Login(string userName, string password)
		{
			var userLogin = await _userRepository.GetByUserName(userName);
			if (userLogin is null)
				return false;

			if (userLogin.Password != password)
				return false;

			await _userRepository.Login(userLogin);
			return true;
		}

		public async Task<bool> IsLoggedInAsync(int id)
		{
			var userLoggedIn = await _userRepository.GetById(id);
			if (userLoggedIn is null)
				return false;

			return userLoggedIn.IsLogged;
		}
	}
}