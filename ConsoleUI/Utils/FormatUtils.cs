namespace Laboration.ConsoleUI.Utils
{
	// Provides utility methods for formatting console output.
	public static class FormatUtils
	{
		// Creates a separator line used for formatting console output.
		// int totalWidth is the width of the separator line.
		// Defaults to 50 if not specified.
		public static string CreateSeparatorLine(int totalWidth = 50)
		{
			return new string('-', totalWidth);
		}
	}
}