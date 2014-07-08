namespace MfGames.TokenizedText
{
	/// <summary>
	/// Represents a token within the line. The most common type of token is
	/// visible text with no additional attributes or tags. Other properties of
	/// a token may make it non-editable or non-deletable.
	public class Token
	{
		public TokenKey Key { get; private set; }
		public string Text { get; private set; }

		public Token(TokenKey key, string text)
		{
			Key = key;
			Text = text;
		}
	}
}