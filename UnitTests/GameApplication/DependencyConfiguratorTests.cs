using ConsoleUI.Implementations;
using GameApplication.Implementations;
using GameFactory.Implementations;

namespace UnitTests.GameApplication
{
	[TestClass]
	public class DependencyConfiguratorTests
	{
		private readonly DependencyConfigurator _configurator = new();

		// Tests if ConfigureDependencies returns a non-null IGameSelection instance.
		[TestMethod]
		public void ConfigureDependencies_ShouldReturnIGameSelectionInstance()
		{
			// Act
			var (gameSelection, _) = _configurator.ConfigureDependencies();

			// Assert
			Assert.IsNotNull(gameSelection, "IGameSelection instance should not be null.");
			Assert.IsInstanceOfType(gameSelection, typeof(GameSelection), "IGameSelection instance should be of type GameSelection.");
		}

		// Tests if ConfigureDependencies returns a non-null IGameFactoryCreator instance.
		[TestMethod]
		public void ConfigureDependencies_ShouldReturnIGameFactoryCreatorInstance()
		{
			// Act
			var (_, factoryCreator) = _configurator.ConfigureDependencies();

			// Assert
			Assert.IsNotNull(factoryCreator, "IGameFactoryCreator instance should not be null.");
			Assert.IsInstanceOfType(factoryCreator, typeof(GameFactoryCreator), "IGameFactoryCreator instance should be of type GameFactoryCreator.");
		}

		// Tests if ConfigureDependencies returns instances of the correct types.
		[TestMethod]
		public void ConfigureDependencies_ShouldReturnCorrectInstances()
		{
			// Act
			var (gameSelection, factoryCreator) = _configurator.ConfigureDependencies();

			// Assert
			Assert.IsInstanceOfType(gameSelection, typeof(GameSelection), "IGameSelection instance should be of type GameSelection.");
			Assert.IsInstanceOfType(factoryCreator, typeof(GameFactoryCreator), "IGameFactoryCreator instance should be of type GameFactoryCreator.");
		}
	}
}