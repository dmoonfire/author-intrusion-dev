namespace MfGames.TextTokens
{
	/// <summary>
	/// Represents a generator which creates the unique keys for tokens and lines.
	/// </summary>
	public interface IKeyGenerator
	{
		LineKey GetNextLineKey();
		TokenKey GetNextTokenKey();
	}
}
