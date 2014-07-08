using System;
using MfGames.TokenizedText.Changes;

namespace MfGames.TokenizedText
{
	/// <summary>
	/// An abstract model that represents a buffer, a collection of lines, and
	/// the associated internal processes that manage those lines. A buffer
	/// can represent a single text file, a sequence of files, or a pure memory
	/// implementation of lines.
	///
	/// Buffers are changed via BufferCommand objects and emit BufferChange
	/// objects (via events) to reflect those changes. Typically, a view does
	/// not change it's visible state until it receives the changes via the
	/// BufferChanges.
	/// </summary>
	public abstract class BufferModel
	{
		public EventHandler<LinesInsertedEventArgs> LinesInserted;

		public Line this[int lineIndex]
		{
			get { return GetLine(lineIndex); }
		}

		public abstract Line GetLine(int lineIndex);

		protected void RaiseLinesInserted(int afterLineIndex, int count)
		{
			var listeners = LinesInserted;

			if (listeners != null)
			{
				var args = new LinesInsertedEventArgs(afterLineIndex, count);
				listeners(this, args);
			}
		}
	}
}