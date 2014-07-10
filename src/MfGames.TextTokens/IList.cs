using System.Collections.Generic;

namespace MfGames.TextTokens
{
	/// <summary>
	/// Represents a single line inside the buffer. Each line consists of zero or more
	/// IToken objects, ordered from left to right.
	/// </summary>
	public interface IList
	{
		/// <summary>
		/// Contains the ordered list of tokens within the line.
		/// </summary>
		IReadOnlyList<IToken> Tokens { get; }
	}
}
