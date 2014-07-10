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
			IReadOnlyList<IToken> tokenReplacements,
			bool isIdentityReplacement)
		{
			LineSequence = lineSequence;
			TokenSequence = tokenSequence;
			TokenReplacements = tokenReplacements;
			IsIdentity = isIdentityReplacement;
		}

		/// <summary>
		/// If this is true, then the replacement is considered an identity replacement. Identity
		/// replacements are ones where the replaced token's text is identical to the concatenation
		/// of the replacement tokens. This is used in situations where the editor needs to maintain
		/// a cursor or selection while manipulations are made to the tokens.
		/// </summary>
		public bool IsIdentity { get; private set; }

		public LineSequence LineSequence { get; private set; }
		public TokenSequence TokenSequence { get; set; }
		public IReadOnlyList<IToken> TokenReplacements { get; private set; }
	}
}
