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
		public EventHandler<TokenInsertedEventArgs> TokenInserted;

		public Line this[int lineIndex]
		{
			get { return GetLine(lineIndex); }
		}

		public abstract Line GetLine(int lineIndex);
		
		public abstract void InsertLines(int afterLineIndex, int count);
		
		public void InsertLine(int afterLineIndex)
		{
			InsertLines(afterLineIndex, 1);
		}

		public Token GetToken(
			int lineIndex,
			int tokenIndex)
		{
			Line line = GetLine(lineIndex);
			Token token = line.Tokens[tokenIndex];
			Token cloned = new Token(token);
			return token;
		}

		public void AddToken(
			int lineIndex,
			Token token)
		{
			// Get the line from the buffer.
			Line line = GetLine(lineIndex);

			// Add the token to the end of the line.
			line.Tokens.Add(token);

			// Raise a token inserted event.
			int tokenIndex = line.Tokens.IndexOf(token);
			RaiseTokenInserted(lineIndex, tokenIndex);
		}

		protected void RaiseTokenInserted(
			int lineIndex,
			int tokenIndex)
		{
			var listeners = TokenInserted;

			if (listeners != null)
			{
				var args = new TokenInsertedEventArgs(lineIndex, tokenIndex);
				listeners(this, args);
			}
		}

		protected void RaiseLinesInserted(
			int afterLineIndex,
			LineKey[] lineKeys)
		{
			var listeners = LinesInserted;

			if (listeners != null)
			{
				var args = new LinesInsertedEventArgs(afterLineIndex, lineKeys);
				listeners(this, args);
			}
		}
	}
}