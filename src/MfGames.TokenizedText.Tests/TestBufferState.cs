using MfGames.TokenizedText.Changes;
using System.Collections.Generic;

namespace MfGames.TokenizedText
{
	public class TestBufferState
	{
		public BufferModel Model { get; private set; }
		public List<Line> Lines { get; private set; }

		public TestBufferState(BufferModel model)
		{
			// Save the model and initialize the collections.
			Model = model;
			Lines = new List<Line>();

			// Hoop up the events we listen to.
			Model.LinesInserted += OnLinesInserted;
		}

		private void OnLinesInserted(object sender, LinesInsertedEventArgs e)
		{
			// Create a collection of lines to insert.
			var lines = new Line[e.Count];

			for (int i = 0; i < e.Count; i++)
			{
				lines[i] = new Line(e.LineKeys[i]);
			}

			// Insert the lines into the collection.
			Lines.InsertRange(e.AfterLineIndex, lines);
		}

		private void OnTokenInserted(object sender, TokenInsertedEventArgs e)
		{
			// Find the appropriate line from our collection.
			Line line = Lines[e.LineIndex];

			// Get the token for that line and add it to our line. This is
			// always a copy of the token, so we can manipulate it safely.
			Token token = Model.GetToken(e.LineIndex, e.TokenIndex);

			line.Tokens.Insert(e.TokenIndex - 1, token);
		}
	}
}