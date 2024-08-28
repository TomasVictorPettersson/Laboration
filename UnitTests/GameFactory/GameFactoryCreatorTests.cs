using GameFactory.Implementations;
using GameResources.Enums;

namespace UnitTests.GameFactory
{
	[TestClass]
	public class GameFactoryCreatorTests
	{
		private readonly GameFactoryCreator _gameFactoryCreator = new();

		// Test that a BullsAndCowsFactory is returned for GameTypes.BullsAndCows.
		[TestMethod]
		public void CreateGameFactory_ShouldReturnBullsAndCowsFactory_ForBullsAndCowsGameType()
		{
			// Act
			var result = _gameFactoryCreator.CreateGameFactory(GameTypes.BullsAndCows);

			// Assert
			Assert.IsNotNull(result, "Factory for BullsAndCows should not be null.");
			Assert.IsInstanceOfType(result, typeof(BullsAndCowsFactory), "Expected BullsAndCowsFactory.");
		}

		// Test that a MasterMindFactory is returned for GameTypes.MasterMind.
		[TestMethod]
		public void CreateGameFactory_ShouldReturnMasterMindFactory_ForMasterMindGameType()
		{
			// Act
			var result = _gameFactoryCreator.CreateGameFactory(GameTypes.MasterMind);

			// Assert
			Assert.IsNotNull(result, "Factory for MasterMind should not be null.");
			Assert.IsInstanceOfType(result, typeof(MasterMindFactory), "Expected MasterMindFactory.");
		}

		// Test that null is returned for GameTypes.Quit.
		[TestMethod]
		public void CreateGameFactory_ShouldReturnNull_ForQuitGameType()
		{
			// Act
			var result = _gameFactoryCreator.CreateGameFactory(GameTypes.Quit);

			// Assert
			Assert.IsNull(result, "Factory for Quit should be null.");
		}

		// Test that an exception is thrown for an invalid GameType.
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void CreateGameFactory_ShouldThrowException_ForInvalidGameType()
		{
			// Act
			_gameFactoryCreator.CreateGameFactory((GameTypes)999);

			// Assert is handled by ExpectedException.
		}
	}
}