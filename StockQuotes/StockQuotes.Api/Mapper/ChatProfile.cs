using AutoMapper;
using StockQuotes.Api.Models;
using StockQuotes.Domain;

namespace StockQuotes.Api.Mapper
{
	public class ChatProfile : Profile
	{
		public ChatProfile() 
		{
			CreateMap<ChatRequest, Chat>()
				.ForPath(cht => cht.User.Id,
						 map => map.MapFrom(src => src.UserId));
			
			CreateMap<Chat, ChatRequest>();

			CreateMap<Chat,ChatResponse>()
				.ForPath(cht => cht.UserName,
						 map => map.MapFrom(src => src.User.UserName))
				.ForPath(cht => cht.Message,
						 map => map.MapFrom(src => src.Message))
				.ForPath(cht => cht.DateTime,
						 map => map.MapFrom(src => src.DateTime));
		}		
	}
}