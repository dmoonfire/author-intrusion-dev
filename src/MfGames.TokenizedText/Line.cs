using System.Collections.Generic;

namespace MfGames.TokenizedText
{
	/// <summary>
	/// Represents a single line in the buffer which has zero or more tokens.
	/// </summary>
	public class Line
	{
		#region Constructors

		public Line(LineKey lineKey)
		{
			Key = lineKey;
			Tokens = new List<Token>();
		}

		#endregion Constructors

		#region Properties

		public LineKey Key
		{
			get; private set;
		}

		public IList<Token> Tokens
		{
			get; private set;
		}

		#endregion Properties
	}
}