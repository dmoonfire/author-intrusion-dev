using System.Collections.Generic;

namespace MfGames.TokenizedText
{
	/// <summary>
	/// Implements a simplistic, in-memory buffer model that represents the
	/// lines in a simple generic list.
	/// </summary>
	public class MemoryBufferModel : BufferModel
	{
		private List<Line> lines;

		public override Line GetLine(int lineIndex)
		{
			return lines[lineIndex];
		}

		public void InsertLines(int afterLineIndex, int count)
		{
			// Create an array of blank lines to insert into the collection.
			var array = new Line[count];

			for (int i = 0; i < count; i++)
			{
				//LineKey lineKey = LineKeyGenerator.Instance.NewLineKey();
				LineKey lineKey = KeyGenerator.Instance.GetNextLineKey();
				array[i] = new Line(lineKey);
			}

			// Insert the lines into the collection.
			lines.InsertRange(afterLineIndex, array);

			// Raise an event with the created lines.
			RaiseLinesInserted(afterLineIndex, count);
		}

		public MemoryBufferModel()
		{
			lines = new List<Line>();
		}
	}
}
