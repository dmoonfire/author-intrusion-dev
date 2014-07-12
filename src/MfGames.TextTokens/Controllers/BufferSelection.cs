// <copyright file="BufferSelection.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Controllers
{
    using System;

    using MfGames.TextTokens.Buffers;
    using MfGames.TextTokens.Commands;
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
            this.Buffer = buffer;
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
        /// </returns>
        public IToken AddOperations(BufferCommand command)
        {
            // If we don't have a selection, then don't do anything.
            if (!this.HasSelection)
            {
                return null;
            }

            // Pull out the first and last token and their associated texts.
            IToken firstToken = this.Buffer.GetToken(
                this.First.LineIndex, this.First.TokenIndex);
            string firstText = firstToken.Text;
            IToken lastToken = this.Buffer.GetToken(
                this.Last.LineIndex, this.Last.TokenIndex);
            string lastText = lastToken.Text;

            // Create the combined token together.
            string newText = firstText.Substring(0, this.First.TextIndex.Index)
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
                return newToken;
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

        #endregion
    }
}