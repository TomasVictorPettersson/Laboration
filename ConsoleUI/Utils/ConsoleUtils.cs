namespace ConsoleUI.Utils
{
	// Provides utility methods for interacting with the console.
	public static class ConsoleUtils
	{
		// Displays a message to the user and waits for a key press before clearing the console.
		public static void WaitForUserToContinue(string message)
		{
			Console.WriteLine(message);
			Console.ReadKey(intercept: true);
			Console.Clear();
		}
	}
}