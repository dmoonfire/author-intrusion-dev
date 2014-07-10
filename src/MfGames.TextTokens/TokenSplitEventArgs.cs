using System.Collections.Generic;

namespace MfGames.TextTokens
{
	/// <summary>
	/// Indicates a token split command where a singe token is broken into a number of
	/// new tokens. The text of the source token is always identical to the concatenation
	/// of the replacement tokens in the order given. This allows for any editor listening
	/// to move a cursor or selection to the new token.
	/// </summary>
	public class TokenSplitEventArgs : TokenReplacedEventArgs
	{
		public TokenSplitEventArgs(
			LineSequence lineSequence,
			TokenSequence tokenSequence,
			IReadOnlyList<IToken> tokenReplacements)
			: base(lineSequence, tokenSequence, tokenReplacements)
		{
		}
	}
}
