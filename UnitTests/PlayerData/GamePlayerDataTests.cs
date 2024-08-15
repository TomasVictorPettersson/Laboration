using Laboration.GameResources.Constants;
using Laboration.PlayerData.Implementations;

namespace Laboration.UnitTests.PlayerData
{
	[TestClass]
	public class GamePlayerDataTests
	{
		// Verifies that AddGuess correctly updates the total guesses and games played.
		[TestMethod]
		public void AddGuess_IncreasesTotalGuessesAndGamesPlayed()
		{
			// Arrange
			var playerData = new GamePlayerData(TestConstants.UserNameJohnDoe, 10);

			// Act
			playerData.AddGuess(5);

			// Assert
			Assert.AreEqual(15, playerData.TotalGuesses, "Total guesses not updated correctly.");
			Assert.AreEqual(2, playerData.TotalGamesPlayed, "Games played not updated correctly.");
		}

		// Verifies that CalculateAverageGuesses returns the correct average number of guesses.
		[TestMethod]
		public void CalculateAverageGuesses_ReturnsCorrectAverage()
		{
			// Arrange
			var playerData = new GamePlayerData(TestConstants.UserNameJohnDoe, 10);
			playerData.AddGuess(8);
			playerData.AddGuess(12);

			// Act
			double averageGuesses = playerData.CalculateAverageGuesses();

			// Assert
			Assert.AreEqual(10, averageGuesses, "Average guesses calculation is incorrect.");
		}

		// Verifies that CalculateAverageGuesses returns zero when no guesses have been made.
		[TestMethod]
		public void CalculateAverageGuesses_ReturnsZeroWhenNoGuesses()
		{
			// Arrange
			var playerData = new GamePlayerData(TestConstants.UserNameJohnDoe, 0);

			// Act
			double averageGuesses = playerData.CalculateAverageGuesses();

			// Assert
			Assert.AreEqual(0, averageGuesses, "Average guesses should be zero when no guesses are made.");
		}

		// Verifies that Equals returns true when comparing players with the same username.
		[TestMethod]
		public void Equals_ReturnsTrueForSameUserName()
		{
			// Arrange
			var playerData1 = new GamePlayerData(TestConstants.UserNameJohnDoe, 10);
			var playerData2 = new GamePlayerData(TestConstants.UserNameJohnDoe, 5);

			// Act
			bool areEqual = playerData1.Equals(playerData2);

			// Assert
			Assert.IsTrue(areEqual, "Equals method should return true for players with the same username.");
		}

		// Verifies that Equals returns false when comparing players with different usernames.
		[TestMethod]
		public void Equals_ReturnsFalseForDifferentUserName()
		{
			// Arrange
			var playerData1 = new GamePlayerData(TestConstants.UserNameJohnDoe, 10);
			var playerData2 = new GamePlayerData(TestConstants.UserNameJaneDoe, 5);

			// Act
			bool areEqual = playerData1.Equals(playerData2);

			// Assert
			Assert.IsFalse(areEqual, "Equals method should return false for players with different usernames.");
		}

		// Verifies that GetHashCode returns the same hash code for players with the same username.
		[TestMethod]
		public void GetHashCode_ReturnsSameHashCodeForSameUserName()
		{
			// Arrange
			var playerData1 = new GamePlayerData(TestConstants.UserNameJohnDoe, 10);
			var playerData2 = new GamePlayerData(TestConstants.UserNameJohnDoe, 5);

			// Act
			int hashCode1 = playerData1.GetHashCode();
			int hashCode2 = playerData2.GetHashCode();

			// Assert
			Assert.AreEqual(hashCode1, hashCode2, "GetHashCode should return the same hash code for the same username.");
		}

		// Verifies that GetHashCode returns different hash codes for players with different usernames.
		[TestMethod]
		public void GetHashCode_ReturnsDifferentHashCodeForDifferentUserName()
		{
			// Arrange
			var playerData1 = new GamePlayerData(TestConstants.UserNameJohnDoe, 10);
			var playerData2 = new GamePlayerData(TestConstants.UserNameJaneDoe, 5);

			// Act
			int hashCode1 = playerData1.GetHashCode();
			int hashCode2 = playerData2.GetHashCode();

			// Assert
			Assert.AreNotEqual(hashCode1, hashCode2, "GetHashCode should return different hash codes for different usernames.");
		}
	}
}