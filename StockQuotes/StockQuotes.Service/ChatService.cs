using StockQuotes.Domain;
using StockQuotes.Repository.Interfaces;
using StockQuotes.Service.Interfaces;

namespace StockQuotes.Service
{
	public class ChatService : IChatService
	{
		private readonly IChatRepository _chatRepository;
		private readonly IUserRepository _userRepository;

		private readonly string stock = "/stock=";

		public ChatService(IChatRepository chatRepository, IUserRepository userRepository)
		{
			_chatRepository = chatRepository;
			_userRepository = userRepository;
		}

		public async Task AddChat(Chat chat)
		{
			if (chat is null || string.IsNullOrEmpty(chat.Message))
				return;

			await _chatRepository.Add(chat);

			if (chat.Message.Contains(stock))
			{
				var stockCode = chat.Message.Replace(stock, string.Empty);

				if (string.IsNullOrEmpty(stockCode))
					return;

				var chatStockCode = await _chatRepository.GetByStockCode(stockCode);

				if (chatStockCode is null)
					return;

				var chatStockCodePost = new Chat()
				{
					DateTime = DateTime.Now,
					Message = chatStockCode.Message,
					StockCode = chatStockCode.StockCode,
					User = chatStockCode.User
				};
								
				await _chatRepository.Add(chatStockCodePost);
			}
		}

		public async Task<IEnumerable<Chat>> Get(int take = 50)
		{
			var chats = await _chatRepository.Get(take).ConfigureAwait(false);

			var users = await _userRepository.GetAll();

			foreach (Chat chat in chats)
			{
				chat.User.UserName = users?.FirstOrDefault(usr => usr.Id == chat.User.Id)?.UserName; //Just Because I'm not using EF/database map
			}

			return chats;
		}
	}
}