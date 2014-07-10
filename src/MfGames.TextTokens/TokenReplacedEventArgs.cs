using System;
using System.Collections.Generic;

namespace MfGames.TextTokens
{
	/// <summary>
	/// Indicates an event where a single token is replaced by zero or more tokens. This is
	/// also used to delete tokens, by providing an empty list of tokens.
	/// </summary>
	public class TokenReplacedEventArgs : EventArgs
	{
		public TokenReplacedEventArgs(
			LineSequence lineSequence,
			TokenSequence tokenSequence,
			IReadOnlyList<IToken> tokenReplacements)
		{
			LineSequence = lineSequence;
			TokenSequence = tokenSequence;
			TokenReplacements = tokenReplacements;
		}

		public LineSequence LineSequence { get; private set; }
		public TokenSequence TokenSequence { get; set; }
		public IReadOnlyList<IToken> TokenReplacements { get; private set; }
	}
}
