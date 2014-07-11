// <copyright file="MemoryBuffer.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Buffers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using MfGames.TextTokens.Events;
    using MfGames.TextTokens.Lines;
    using MfGames.TextTokens.Tokens;

    /// <summary>
    /// Implements an IBuffer that is based by an in-memory array of line objects.
    /// </summary>
    public class MemoryBuffer : IBuffer
    {
        #region Fields

        /// <summary>
        /// The lines contained with the buffer.
        /// </summary>
        private readonly List<Line> lines;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryBuffer"/> class.
        /// </summary>
        public MemoryBuffer()
        {
            this.lines = new List<Line>();
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Occurs when lines are deleted from the buffer.
        /// </summary>
        public event EventHandler<LineIndexLinesDeletedEventArgs> LinesDeleted;

        /// <summary>
        /// Occurs when lines are inserted into the buffer.
        /// </summary>
        public event EventHandler<LineIndexLinesInsertedEventArgs> LinesInserted;

        /// <summary>
        /// Occurs when a token is replaced by zero or more tokens.
        /// </summary>
        public event EventHandler<LineIndexTokenIndexTokenReplacedEventArgs> TokenReplaced;

        /// <summary>
        /// Occurs when tokens are inserted into a buffer line.
        /// </summary>
        public event EventHandler<LineIndexTokenIndexTokensInsertedEventArgs> TokensInserted;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the lines contained within the buffer.
        /// </summary>
        /// <value>
        /// The lines.
        /// </value>
        public IReadOnlyList<ILine> Lines
        {
            get
            {
                return this.lines.AsReadOnly();
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Gets the index of the line.
        /// </summary>
        /// <param name="line">
        /// The line.
        /// </param>
        /// <returns>
        /// A line index of the given line.
        /// </returns>
        public LineIndex GetLineIndex(ILine line)
        {
            // Establish our contracts.
            Contract.Requires(line != null);

            // Find the line's index, wrap it, and return it.
            int index = this.lines.FindIndex(l => l.LineKey == line.LineKey);
            var lineIndex = new LineIndex(index);
            return lineIndex;
        }

        /// <summary>
        /// Inserts the lines.
        /// </summary>
        /// <param name="afterLineIndex">
        /// Index of the after line.
        /// </param>
        /// <param name="count">
        /// The count.
        /// </param>
        /// <returns>
        /// An enumerable of the created lines.
        /// </returns>
        public IEnumerable<ILine> InsertLines(
            LineIndex afterLineIndex, int count)
        {
            // Establish our contracts.
            Contract.Requires(afterLineIndex.Index >= 0);
            Contract.Requires(count > 0);

            // First populate a list of line keys for the new lines.
            var insertedLines = new Line[count];

            for (int index = 0; index < count; index++)
            {
                LineKey lineKey = KeyGenerator.Instance.GetNextLineKey();
                insertedLines[index] = new Line(lineKey);
            }

            // Insert the lines into the buffer.
            this.InsertLines(afterLineIndex, insertedLines);

            // Return the resulting lines.
            return insertedLines;
        }

        /// <summary>
        /// Inserts the lines.
        /// </summary>
        /// <param name="afterLineIndex">
        /// Index of the after line.
        /// </param>
        /// <param name="insertedLines">
        /// The inserted lines.
        /// </param>
        public void InsertLines(
            LineIndex afterLineIndex, params Line[] insertedLines)
        {
            // Establish our contracts.
            Contract.Requires(afterLineIndex.Index >= 0);
            Contract.Requires(insertedLines != null);

            // Insert the lines into the buffer.
            this.InsertLines(afterLineIndex, (IEnumerable<Line>)insertedLines);
        }

        /// <summary>
        /// Inserts the lines into the buffer.
        /// </summary>
        /// <param name="afterLineIndex">
        /// Index of the after line.
        /// </param>
        /// <param name="insertedLines">
        /// The inserted lines.
        /// </param>
        public void InsertLines(
            LineIndex afterLineIndex, IEnumerable<Line> insertedLines)
        {
            // Establish our contracts.
            Contract.Requires(afterLineIndex.Index >= 0);
            Contract.Requires(insertedLines != null);

            // Subscribe to the events of these lines.
            List<Line> lineArray = insertedLines as List<Line>
                                   ?? insertedLines.ToList();

            lineArray.ForEach(l => l.TokensInserted += this.OnTokensInserted);

            // Insert the lines into the buffer at the given position.
            this.lines.InsertRange(afterLineIndex.Index, lineArray);

            // Raise an event for the inserted lines.
            this.RaiseLinesInserted(afterLineIndex, lineArray);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Raises the LinesInserted event.
        /// </summary>
        /// <param name="afterLineIndex">
        /// Index of the after line.
        /// </param>
        /// <param name="insertedLines">
        /// The inserted lines.
        /// </param>
        protected void RaiseLinesInserted(
            LineIndex afterLineIndex, IEnumerable<ILine> insertedLines)
        {
            EventHandler<LineIndexLinesInsertedEventArgs> listener =
                this.LinesInserted;

            if (listener == null)
            {
                return;
            }

            ReadOnlyCollection<ILine> readOnlyLines =
                new List<ILine>(insertedLines).AsReadOnly();
            var args = new LineIndexLinesInsertedEventArgs(
                afterLineIndex, readOnlyLines);

            listener(this, args);
        }

        /// <summary>
        /// Raises the tokens inserted.
        /// </summary>
        /// <param name="lineIndex">
        /// Index of the line.
        /// </param>
        /// <param name="tokenIndex">
        /// Index of the token.
        /// </param>
        /// <param name="insertedTokens">
        /// The inserted tokens.
        /// </param>
        protected void RaiseTokensInserted(
            LineIndex lineIndex, 
            TokenIndex tokenIndex, 
            IEnumerable<IToken> insertedTokens)
        {
            EventHandler<LineIndexTokenIndexTokensInsertedEventArgs> listener =
                this.TokensInserted;

            if (listener == null)
            {
                return;
            }

            ReadOnlyCollection<IToken> readOnlyTokens =
                new List<IToken>(insertedTokens).AsReadOnly();
            var args = new LineIndexTokenIndexTokensInsertedEventArgs(
                lineIndex, tokenIndex, readOnlyTokens);

            listener(this, args);
        }

        /// <summary>
        /// Called when tokens are inserted into line.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="TokenIndexTokensInsertedEventArgs"/> instance containing the event data.
        /// </param>
        private void OnTokensInserted(
            object sender, TokenIndexTokensInsertedEventArgs e)
        {
            // Establish our contracts.
            Contract.Requires(sender is Line);

            // Get our line and its index.
            var line = (Line)sender;
            LineIndex lineIndex = this.GetLineIndex(line);

            // Pass the event to our listeners.
            this.RaiseTokensInserted(lineIndex, e.TokenIndex, e.TokensInserted);
        }

        #endregion
    }
}