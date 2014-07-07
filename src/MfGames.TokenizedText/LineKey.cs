namespace MfGames.TokenizedText
{
	/// <summary>
	/// An immutable, typesafe identifier for a single line.
	///
	/// A given LineKey is only applicable for a single process and should not
	/// be serialized or used in any matter. It is purely for identifying the
	/// key during processing.
	/// </summary>
	public struct LineKey
	{
		public readonly int Id;

		public LineKey(int id)
		{
			Id = id;
		}
	}
}
