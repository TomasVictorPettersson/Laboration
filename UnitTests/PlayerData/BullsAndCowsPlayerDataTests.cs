using Laboration.PlayerData.Implementations;

namespace Laboration.UnitTests.PlayerData
{
	[TestClass]
	public class BullsAndCowsPlayerDataTests
	{
		// Constants for usernames
		private const string UserNameJohnDoe = "JohnDoe";

		private const string UserNameJaneDoe = "JaneDoe";

		[TestMethod]
		public void AddGuess_IncreasesTotalGuessesAndGamesPlayed()
		{
			// Arrange
			var playerData = new BullsAndCowsPlayerData(UserNameJohnDoe, 10);

			// Act
			playerData.AddGuess(5);

			// Assert
			Assert.AreEqual(15, playerData.TotalGuesses, "Total guesses not updated correctly.");
			Assert.AreEqual(2, playerData.TotalGamesPlayed, "Games played not updated correctly.");
		}

		[TestMethod]
		public void CalculateAverageGuesses_ReturnsCorrectAverage()
		{
			// Arrange
			var playerData = new BullsAndCowsPlayerData(UserNameJohnDoe, 10);
			playerData.AddGuess(8);
			playerData.AddGuess(12);

			// Act
			double averageGuesses = playerData.CalculateAverageGuesses();

			// Assert
			Assert.AreEqual(10, averageGuesses, "Average guesses calculation is incorrect.");
		}

		[TestMethod]
		public void CalculateAverageGuesses_ReturnsZeroWhenNoGuesses()
		{
			// Arrange
			var playerData = new BullsAndCowsPlayerData(UserNameJohnDoe, 0);

			// Act
			double averageGuesses = playerData.CalculateAverageGuesses();

			// Assert
			Assert.AreEqual(0, averageGuesses, "Average guesses should be zero when no guesses are made.");
		}

		[TestMethod]
		public void Equals_ReturnsTrueForSameUserName()
		{
			// Arrange
			var playerData1 = new BullsAndCowsPlayerData(UserNameJohnDoe, 10);
			var playerData2 = new BullsAndCowsPlayerData(UserNameJohnDoe, 5);

			// Act
			bool areEqual = playerData1.Equals(playerData2);

			// Assert
			Assert.IsTrue(areEqual, "Equals method should return true for players with the same username.");
		}

		[TestMethod]
		public void Equals_ReturnsFalseForDifferentUserName()
		{
			// Arrange
			var playerData1 = new BullsAndCowsPlayerData(UserNameJohnDoe, 10);
			var playerData2 = new BullsAndCowsPlayerData(UserNameJaneDoe, 5);

			// Act
			bool areEqual = playerData1.Equals(playerData2);

			// Assert
			Assert.IsFalse(areEqual, "Equals method should return false for players with different usernames.");
		}

		[TestMethod]
		public void GetHashCode_ReturnsSameHashCodeForSameUserName()
		{
			// Arrange
			var playerData1 = new BullsAndCowsPlayerData(UserNameJohnDoe, 10);
			var playerData2 = new BullsAndCowsPlayerData(UserNameJohnDoe, 5);

			// Act
			int hashCode1 = playerData1.GetHashCode();
			int hashCode2 = playerData2.GetHashCode();

			// Assert
			Assert.AreEqual(hashCode1, hashCode2, "GetHashCode should return the same hash code for the same username.");
		}

		[TestMethod]
		public void GetHashCode_ReturnsDifferentHashCodeForDifferentUserName()
		{
			// Arrange
			var playerData1 = new BullsAndCowsPlayerData(UserNameJohnDoe, 10);
			var playerData2 = new BullsAndCowsPlayerData(UserNameJaneDoe, 5);

			// Act
			int hashCode1 = playerData1.GetHashCode();
			int hashCode2 = playerData2.GetHashCode();

			// Assert
			Assert.AreNotEqual(hashCode1, hashCode2, "GetHashCode should return different hash codes for different usernames.");
		}
	}
}