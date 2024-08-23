namespace Laboration.ConsoleUI.Utils
{
	public static class ConsoleUtils
	{
		public static void WaitForUserToContinue(string message)
		{
			Console.WriteLine(message);
			Console.ReadKey(intercept: true); // Reads user input without displaying the key pressed.
			Console.Clear();
		}
	}
}