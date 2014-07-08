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
		#region Fields

		public readonly int Id;

		#endregion Fields

		#region Constructors

		public LineKey(int id)
		{
			Id = id;
		}

		#endregion Constructors
	}
}