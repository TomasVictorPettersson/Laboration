using Laboration.PlayerData.Interfaces;
using Moq;
using System.Globalization;

namespace Laboration.Mocks
{
	// Provides mock data for testing IPlayerData implementations.
	public static class MockPlayerData
	{
		// Constants representing test data for player information.
		public const string UserName = "TestUser";

		public const string SecretNumber = "1234";
		public const string TotalGamesPlayed = "10";
		public const string AverageGuesses = "5,50";

		// Creates a mock instance of IPlayerData with predefined test values.
		public static IPlayerData CreateMock()
		{
			var mockPlayerData = new Mock<IPlayerData>();
			mockPlayerData.Setup(p => p.UserName).Returns(UserName);
			mockPlayerData.Setup(p => p.TotalGamesPlayed).Returns(int.Parse(TotalGamesPlayed));
			mockPlayerData.Setup(p => p.CalculateAverageGuesses()).Returns(double.Parse(AverageGuesses.Replace(',', '.'), CultureInfo.InvariantCulture));
			return mockPlayerData.Object;
		}
	}
}