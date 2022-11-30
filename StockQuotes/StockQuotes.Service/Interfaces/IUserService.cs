using StockQuotes.Domain;

namespace StockQuotes.Service.Interfaces
{
    public interface IUserService
	{
        public Task<bool> AddUser(User user);
		public Task<IEnumerable<User>> GetAll();
		public Task<bool> Login(string userName, string password);
		public Task<bool> IsLoggedInAsync(int id);
	}
}