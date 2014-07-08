using System.Collections.Generic;

namespace MfGames.TokenizedText
{
	/// <summary>
	/// Implements a simplistic, in-memory buffer model that represents the
	/// lines in a simple generic list.
	/// </summary>
	public class MemoryBufferModel : BufferModel
	{
		#region Fields

		private List<Line> lines;

		#endregion Fields

		#region Constructors

		public MemoryBufferModel()
		{
			lines = new List<Line>();
		}

		#endregion Constructors

		#region Methods

		public override Line GetLine(int lineIndex)
		{
			return lines[lineIndex];
		}

		public override void InsertLines(int afterLineIndex, int count)
		{
			// Create an array of blank lines to insert into the collection.
			var newLines = new Line[count];
			var newKeys = new LineKey[count];

			for (int i = 0; i < count; i++)
			{
				//LineKey lineKey = LineKeyGenerator.Instance.NewLineKey();
				LineKey lineKey = KeyGenerator.Instance.GetNextLineKey();
				newKeys[i] = lineKey;
				newLines[i] = new Line(lineKey);
			}

			// Insert the lines into the collection.
			lines.InsertRange(afterLineIndex, newLines);

			// Raise an event with the created lines.
			RaiseLinesInserted(afterLineIndex, newKeys);
		}

		#endregion Methods
	}
}