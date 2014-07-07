namespace MfGames.TokenizedText
{
	/// <summary>
	/// An immutable, typesafe identifier for a single token.
	///
	/// A given TokenKey is only applicable for a single process and should not
	/// be serialized or used in any matter. It is purely for identifying the
	/// key during processing.
	/// </summary>
	public struct TokenKey
	{
		public readonly int Id;

		public TokenKey(int id)
		{
			Id = id;
		}
	}
}
