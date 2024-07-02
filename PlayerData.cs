internal class PlayerData(string name, int guesses)
{
	public string Name { get; } = name;
	public int NGames { get; private set; } = 1;
	private int totalGuess = guesses;

	public void Update(int guesses)
	{
		totalGuess += guesses;
		NGames++;
	}

	public double Average()
	{
		return (double)totalGuess / NGames;
	}

	public override bool Equals(Object p)
	{
		return Name.Equals(((PlayerData)p).Name);
	}

	public override int GetHashCode()
	{
		return Name.GetHashCode();
	}
}