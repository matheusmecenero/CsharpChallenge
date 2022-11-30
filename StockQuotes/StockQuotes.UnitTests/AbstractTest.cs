using Moq.AutoMock;

namespace StockQuotes.UnitTests
{
	public class AbstractTest
	{
		public readonly AutoMocker Mocker;

		public AbstractTest() 
		{
			Mocker = new AutoMocker();
		}		
	}
}
