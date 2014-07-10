namespace MfGames.TextTokens
{
	/// <summary>
	/// Represents a single token within a line. Most tokens are text tokens
	/// which allow users to edit them, but tokens can also be used to represent
	/// ruby text (text above and below another text), read-only sections, or
	/// specially formatted codes.
	/// 
	/// To avoid memory pressure, a token may actually be the internal object,
	/// however because tokens are effectively immutable outside of two specific
	/// calls to the Buffer (ExecuteUserCommand and ExecuteBackgroundCommands).
	/// </summary>
	public interface IToken
	{
	}
}
