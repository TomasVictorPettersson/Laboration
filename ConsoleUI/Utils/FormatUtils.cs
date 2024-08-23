namespace Laboration.ConsoleUI.Utils
{
	public static class FormatUtils
	{
		// Creates a separator line to format the high score display.
		// Defaults to a width of 50 if no value is provided.
		public static string CreateSeparatorLine(int totalWidth = 50)
		{
			return new string('-', totalWidth);
		}
	}
}