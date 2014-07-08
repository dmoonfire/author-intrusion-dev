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
		#region Fields

		public readonly int Id;

		#endregion Fields

		#region Constructors

		public TokenKey(int id)
		{
			Id = id;
		}

		#endregion Constructors
	}
}