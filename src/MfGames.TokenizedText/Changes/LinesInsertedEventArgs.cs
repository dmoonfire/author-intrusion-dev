using System;

namespace MfGames.TokenizedText.Changes
{
	/// <summary>
	/// Represents the changes made when one or more lines are inserted into the
	/// buffer. When this event is processed, then the buffer will have already
	/// adjusted all following events for the new line numbers.
	/// </summary>
	public class LinesInsertedEventArgs
		: EventArgs
	{
		/// <summary>
		/// Indicates the line index that the lines are inserted after. If this
		/// is zero, then the lines are prepended to the collection. If this
		/// equals the line count in the buffer, then they were appended to the
		/// end.
		/// </summary>
		public int AfterLineIndex { get; private set; }

		/// <summary>
		/// Indicates how many lines were inserted into the buffer.
		/// </summary>
		public int Count { get { return LineKeys.Length; } }

		/// <summary>
		/// Contains an arrow of LineKeys that represent the new lines inserted.
		/// There are Count entries, starting at 0.
		/// </summary>
		public LineKey[] LineKeys { get; private set; }

		public LinesInsertedEventArgs(
			int afterLineIndex,
			LineKey[] lineKeys)
		{
			AfterLineIndex = afterLineIndex;
			LineKeys = lineKeys;
		}
	}
}