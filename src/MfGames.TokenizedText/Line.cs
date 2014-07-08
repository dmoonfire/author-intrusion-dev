using System.Collections.Generic;

namespace MfGames.TokenizedText
{
	/// <summary>
	/// Represents a single line in the buffer which has zero or more tokens.
	/// </summary>
	public class Line
	{
		public IList<Token> Tokens { get; private set; }

		public Line()
		{
			Tokens = new List<Token>();
		}
	}
}