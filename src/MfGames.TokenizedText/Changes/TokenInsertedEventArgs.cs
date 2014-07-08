using System;

namespace MfGames.TokenizedText.Changes
{
	/// <summary>
	/// Represents a new token was added to a given line. It includes both the line
	/// and token index of the new token.
	///
	/// This event does not include the Token itself. With the usage pattern of
	/// getting changes from the buffer, there is frequently a large number of
	/// events that are processed that cover the same area (same line, etc). So,
	/// in most cases, this event is just to trigger a changed flag of some matter
	/// which is then processed after the flurry of events.
	/// </summary>
	public class TokenInsertedEventArgs
		: EventArgs
	{
		/// <summary>
		/// The numerical index of the line within the buffer at the point when
		/// the change was triggered.
		/// </summary>
		public int LineIndex { get; private set; }

		/// <summary>
		/// The numerical index of the token within the line at the point when
		/// the change was triggered.
		/// </summary>
		public int TokenIndex { get; private set; }

		public TokenInsertedEventArgs(int lineIndex, int tokenIndex)
		{
			LineIndex = lineIndex;
			TokenIndex = tokenIndex;
		}
	}
}