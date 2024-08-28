using GameResources.Constants;
using PlayerData.Interfaces;
using Moq;
using System.Globalization;

namespace Mocks
{
	// Provides mock data for testing IPlayerData implementations.
	public static class MockPlayerData
	{
		// Creates a mock instance of IPlayerData with predefined test values.
		public static IPlayerData CreateMock()
		{
			var mockPlayerData = new Mock<IPlayerData>();
			mockPlayerData.Setup(p => p.UserName).Returns(TestConstants.UserName);
			mockPlayerData.Setup(p => p.TotalGamesPlayed).Returns(int.Parse(TestConstants.TotalGamesPlayed));
			mockPlayerData.Setup(p => p.CalculateAverageGuesses()).Returns(double.Parse(TestConstants.AverageGuesses.Replace(',', '.'), CultureInfo.InvariantCulture));
			return mockPlayerData.Object;
		}
	}
}