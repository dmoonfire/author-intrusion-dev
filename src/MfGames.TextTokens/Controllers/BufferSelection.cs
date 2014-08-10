// <copyright file="BufferSelection.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Controllers
{
    using System;
    using System.Collections.Immutable;
    using System.Diagnostics.Contracts;

    using MfGames.TextTokens.Buffers;
    using MfGames.TextTokens.Commands;
    using MfGames.TextTokens.Events;
    using MfGames.TextTokens.Lines;
    using MfGames.TextTokens.Texts;
    using MfGames.TextTokens.Tokens;

    /// <summary>
    /// Encapsulates the functionality for cursors and selections within a buffer view. This
    /// is used by the UserBufferController to determine where the selection is and also for
    /// functionality that involves replacing the contents with input.
    /// </summary>
    public class BufferSelection
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BufferSelection"/> class.
        /// </summary>
        /// <param name="buffer">
        /// The buffer.
        /// </param>
        public BufferSelection(IBuffer buffer)
        {
            // Establish our contracts.
            Contract.Requires(buffer != null);

            // Append all of the visible strings together.
            // Save the member variables.
            this.Buffer = buffer;

            // Attach to the events.
            this.Buffer.ReplaceSelection += this.OnReplaceSelection;
            this.Buffer.RestoreSelection += this.OnRestoreSelection;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the anchor of the selection. An anchor is where the selection starts.
        /// </summary>
        /// <value>
        /// The anchor.
        /// </value>
        public TextLocation Anchor { get; private set; }

        /// <summary>
        /// Gets the buffer associated with the selection.
        /// </summary>
        /// <value>
        /// The buffer.
        /// </value>
        public IBuffer Buffer { get; private set; }

        /// <summary>
        /// Gets the cursor location within the buffer.
        /// </summary>
        /// <value>
        /// The cursor.
        /// </value>
        public TextLocation Cursor { get; private set; }

        /// <summary>
        /// Gets the cursor or anchor that's first in the buffer.
        /// </summary>
        public TextLocation First
        {
            get
            {
                return this.Cursor < this.Anchor ? this.Cursor : this.Anchor;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance represents a selection instead
        /// of a simple cursor.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has selection; otherwise, <c>false</c>.
        /// </value>
        public bool HasSelection
        {
            get
            {
                return this.Cursor != this.Anchor;
            }
        }

        /// <summary>
        /// Gets the anchor or cursor that is later in the buffer.
        /// </summary>
        public TextLocation Last
        {
            get
            {
                return this.Cursor > this.Anchor ? this.Cursor : this.Anchor;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Adds the operations to remove a selection.
        /// </summary>
        /// <param name="command">
        /// The command to add operations to.
        /// </param>
        /// <returns>
        /// A state object that represents the state after the operations.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        /// Cannot handle multi-line selections.
        /// </exception>
        public PostSelectionDeleteState AddDeleteOperations(
            BufferCommand command)
        {
            // If we don't have a selection, then don't do anything.
            ILine firstLine = this.Buffer.Lines[this.First.LineIndex.Index];

            if (!this.HasSelection)
            {
                int tokenOffset = this.Cursor.TokenIndex.Index + 1;
                ImmutableList<IToken> remainingTokens =
                    firstLine.Tokens.GetRange(
                        tokenOffset, 
                        firstLine.Tokens.Count - tokenOffset);
                var noopState = new PostSelectionDeleteState(
                    this.Cursor, 
                    this.Buffer.GetToken(this.Cursor), 
                    remainingTokens);
                return noopState;
            }

            // Regardless of what happens, the selection is going to be collapsed down to
            // the First of the selection.
            command.Add(new ReplaceSelectionOperation(this.ToTextRange()));

            // Pull out the first and last token and their associated texts.
            IToken firstToken = this.Buffer.GetToken(
                this.First.LineIndex, 
                this.First.TokenIndex);
            string firstText = firstToken.Text;
            IToken lastToken = this.Buffer.GetToken(
                this.Last.LineIndex, 
                this.Last.TokenIndex);
            string lastText = lastToken.Text;

            // Create the combined token together.
            string newText = firstText.Substring(
                0, 
                this.First.TextIndex.Index)
                + lastText.Substring(this.Last.TextIndex.Index);
            IToken newToken = this.Buffer.CreateToken(newText);

            // If we are on the same line, then we have a slightly modified version of
            // replacement that is smart enough to handle the additional checks.
            if (this.First.LineIndex == this.Last.LineIndex)
            {
                // Figure out how many tokens we have to remove.
                const int ZeroToOneIndex = 1;
                int count = this.Last.TokenIndex.Index
                    - this.First.TokenIndex.Index + ZeroToOneIndex;

                // Create the replacement operation and we're done.
                command.Add(
                    new ReplaceTokenOperation(
                        this.First.LineIndex, 
                        this.First.TokenIndex, 
                        count, 
                        newToken));

                // Replace the modified token which is the new "first".
                int tokenOffset = this.First.TokenIndex.Index + count;
                ImmutableList<IToken> remainingTokens =
                    firstLine.Tokens.GetRange(
                        tokenOffset, 
                        firstLine.Tokens.Count - tokenOffset);

                var singleLineState = new PostSelectionDeleteState(
                    this.First, 
                    newToken, 
                    remainingTokens);
                return singleLineState;
            }

            // We don't know how to handle this yet.
            throw new InvalidOperationException(
                "Cannot handle multi-line selections.");
        }

        /// <summary>
        /// Extends the selection to a new cursor.
        /// </summary>
        /// <param name="location">
        /// The location to set the new cursor.
        /// </param>
        public void Select(TextLocation location)
        {
            this.Cursor = location;
        }

        /// <summary>
        /// Sets the cursor and clears the anchor.
        /// </summary>
        /// <param name="location">
        /// The text location.
        /// </param>
        public void SetCursor(TextLocation location)
        {
            this.Cursor = location;
            this.Anchor = location;
        }

        /// <summary>
        /// Converts the buffer selection into a text range.
        /// </summary>
        /// <returns>
        /// A text range representing the selection.
        /// </returns>
        public TextRange ToTextRange()
        {
            return new TextRange(
                this.Anchor, 
                this.Cursor);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called when the selection is replaced.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="ReplaceSelectionEventArgs"/> instance containing the event data.
        /// </param>
        private void OnReplaceSelection(
            object sender, 
            ReplaceSelectionEventArgs e)
        {
            e.OldTextRanges[this] = this.ToTextRange();
            this.Anchor = e.TextRange.Anchor;
            this.Cursor = e.TextRange.Cursor;
        }

        /// <summary>
        /// Called when [restore selection].
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="RestoreSelectionEventArgs"/> instance containing the event data.
        /// </param>
        private void OnRestoreSelection(
            object sender, 
            RestoreSelectionEventArgs e)
        {
            TextRange newTextRange;

            if (e.PreviousTextRanges.TryGetValue(
                this, 
                out newTextRange))
            {
                this.Anchor = newTextRange.Anchor;
                this.Cursor = newTextRange.Cursor;
            }
        }

        #endregion
    }
}