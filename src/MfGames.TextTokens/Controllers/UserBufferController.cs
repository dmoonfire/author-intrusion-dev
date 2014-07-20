﻿// <copyright file="UserBufferController.cs" company="Moonfire Games">
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
    using MfGames.TextTokens.Lines;
    using MfGames.TextTokens.Texts;
    using MfGames.TextTokens.Tokens;

    /// <summary>
    /// A specialized controller which takes input from user-initiated events, such
    /// as ones from an editor view, and maps them into buffer operations. There is 
    /// exactly one buffer for a given controller, but a buffer may have multiple
    /// user controllers and (potentially) processing controllers.
    /// </summary>
    public class UserBufferController
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserBufferController"/> class.
        /// </summary>
        /// <param name="buffer">
        /// The buffer.
        /// </param>
        public UserBufferController(IBuffer buffer)
        {
            // Establish our contracts.
            Contract.Requires(buffer != null);

            // Copy the values together.
            this.Buffer = buffer;
            this.Selection = new BufferSelection(buffer);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the buffer associated with the controller.
        /// </summary>
        /// <value>
        /// The buffer.
        /// </value>
        public IBuffer Buffer { get; private set; }

        /// <summary>
        /// Gets the anchor position of the selection.
        /// </summary>
        public TextLocation SelectionAnchor
        {
            get
            {
                return this.Selection.Anchor;
            }
        }

        /// <summary>
        /// Gets the cursor position of the selection.
        /// </summary>
        public TextLocation SelectionCursor
        {
            get
            {
                return this.Selection.Cursor;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selection associated with the controller.
        /// </summary>
        /// <value>
        /// The selection.
        /// </value>
        protected BufferSelection Selection { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Deletes a number of characters to the right of the cursor position.
        /// </summary>
        /// <param name="textCount">
        /// The text count.
        /// </param>
        public void DeleteRight(int textCount)
        {
            // Establish our contracts.
            Contract.Requires(textCount > 0);

            // We'll be gathering up lines to perform the insert.
            var command = new BufferCommand();

            // Allow the selection to add any operations to remove the selection.
            PostSelectionDeleteState state =
                this.Selection.AddDeleteOperations(command);

            // Figure out how much text needs to be deleted from the token.
            string beforeCursorText = state.CursorToken.Text.Substring(
                0, state.Cursor.TextIndex.Index);
            string afterCursorText =
                state.CursorToken.Text.Substring(
                    state.Cursor.TextIndex.Index + textCount);
            string newText = beforeCursorText + afterCursorText;
            IToken newToken = this.Buffer.CreateToken(newText);

            // Create the command and add it.
            var replaceOperation = new ReplaceTokenOperation(
                state.Cursor, 1, newToken);
            command.Add(replaceOperation);

            // Submit the command to the buffer.
            this.Buffer.Do(command);
        }

        /// <summary>
        /// Inserts text into a given location, choosing the appropriate token to modify
        /// and passing it into the buffer as an undoable command.
        /// </summary>
        /// <param name="textLocation">
        /// The location to insert into the buffer.
        /// </param>
        /// <param name="text">
        /// The text to insert.
        /// </param>
        public void InsertText(TextLocation textLocation, string text)
        {
            // Establish our contracts.
            Contract.Requires(text != null);

            // Set the cursor and then insert the text.
            this.SetCursor(textLocation);
            this.InsertText(text);
        }

        /// <summary>
        /// Inserts text at the cursor position, choosing the appropriate token to modify
        /// and passing it into the buffer as an undoble command.
        /// </summary>
        /// <param name="text">
        /// The text to insert.
        /// </param>
        public void InsertText(string text)
        {
            // Establish our contracts.
            Contract.Requires(text != null);

            // We'll be gathering up lines to perform the insert.
            var command = new BufferCommand();

            // Allow the selection to add any operations to remove the selection.
            PostSelectionDeleteState state =
                this.Selection.AddDeleteOperations(command);

            // Determine if we are doing a single or multi-line insert.
            text = text.NormalizeNewlines();

            if (text.Contains("\n"))
            {
                // Insert multiple lines of text.
                this.InsertMultipleLines(text, state, command);
            }
            else
            {
                // Insert a single line text.
                this.InsertSingleLine(text, state, command);
            }

            // Submit the command to the buffer.
            this.Buffer.Do(command);
        }

        /// <summary>
        /// Re-executes the last reversed command on the buffer, or do nothing if there are no
        /// commands that can be redone.
        /// </summary>
        public void Redo()
        {
            this.Buffer.Redo();
        }

        /// <summary>
        /// Extends the selection to the new location.
        /// </summary>
        /// <param name="cursor">
        /// The cursor.
        /// </param>
        public void Select(TextLocation cursor)
        {
            this.Selection.Select(cursor);
        }

        /// <summary>
        /// Sets the cursor location in the selection.
        /// </summary>
        /// <param name="textLocation">
        /// The text location.
        /// </param>
        public void SetCursor(TextLocation textLocation)
        {
            this.Selection.SetCursor(textLocation);
        }

        /// <summary>
        /// Reverses the last command on the buffer or nothing if there are no commands
        /// that can be reversed.
        /// </summary>
        public void Undo()
        {
            this.Buffer.Undo();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets up the various operations to insert multiple lines of text into
        /// the buffer.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <param name="state">
        /// The state after the selection.
        /// </param>
        /// <param name="command">
        /// The command.
        /// </param>
        private void InsertMultipleLines(
            string text, PostSelectionDeleteState state, BufferCommand command)
        {
            // Split the text of the old token where we're going to insert the values. We
            // append the first line to the before token and prepend the last line to the
            // after token to get a complete token. We don't have to worry about splitting
            // the tokens since the buffer will handle that.
            string[] lines = text.Split(new[] { '\n' }, StringSplitOptions.None);
            int lastTokenlength = lines[lines.Length - 1].Length;
            string before = state.CursorToken.Text.Substring(
                0, state.Cursor.TextIndex.Index) + lines[0];
            string after = lines[lines.Length - 1]
                + state.CursorToken.Text.Substring(state.Cursor.TextIndex.Index);

            lines[lines.Length - 1] = after;

            // We need to create lines for all but the first one.
            int newLineCount = lines.Length - 1;

            command.Add(
                new InsertLinesOperation(
                    state.Cursor.LineIndex.Add(1), newLineCount));

            // Move all tokens to the right of the first line into the last one.
            ILine beforeLine = this.Buffer.Lines[state.Cursor.LineIndex.Index];
            ImmutableList<IToken> movedTokens =
                beforeLine.Tokens.GetRange(
                    state.Cursor.TextIndex.Index + 1, 
                    beforeLine.Tokens.Count - state.Cursor.TextIndex.Index - 1);

            // Remove the tokens from the first line.
            command.Add(
                new ReplaceTokenOperation(
                    state.Cursor.LineIndex, 
                    state.Cursor.TokenIndex.Add(1), 
                    movedTokens.Count));

            // Add the tokens into the last line.
            command.Add(
                new ReplaceTokenOperation(
                    state.Cursor.LineIndex.Add(newLineCount), 
                    TokenIndex.First, 
                    0, 
                    movedTokens));

            // Append the new tokens into each line after the first one.
            for (int index = 1; index < lines.Length; index++)
            {
                // Create a token for this line's text and add it to that line.
                IToken lineToken = this.Buffer.CreateToken(lines[index]);

                command.Add(
                    new ReplaceTokenOperation(
                        state.Cursor.LineIndex.Add(index), 
                        TokenIndex.First, 
                        0, 
                        lineToken));
            }

            // Figure out the new text of the string and create a new token with the modified
            // version. This will also copy the attributes of the old token.
            IToken newToken = this.Buffer.CreateToken(state.CursorToken, before);

            // Add the text replacement command into the buffer.
            const int SingleTokenReplacement = 1;

            command.Add(
                new ReplaceTokenOperation(
                    state.Cursor.LineIndex, 
                    state.Cursor.TokenIndex, 
                    SingleTokenReplacement, 
                    newToken));

            // Update the cursor location.
            var newCursor =
                new TextLocation(
                    state.Cursor.LineIndex.Add(newLineCount), 
                    TokenIndex.First, 
                    new TextIndex(lastTokenlength));

            command.Add(new ReplaceSelectionOperation(newCursor));
        }

        /// <summary>
        /// Sets up the various operations to insert a single line text into the buffer.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <param name="state">
        /// The state.
        /// </param>
        /// <param name="command">
        /// The command.
        /// </param>
        private void InsertSingleLine(
            string text, PostSelectionDeleteState state, BufferCommand command)
        {
            // Figure out the new text of the string and create a new token with the modified
            // version. This will also copy the attributes of the old token.
            string newText =
                state.CursorToken.Text.Insert(
                    state.Cursor.TextIndex.Index, text);
            IToken newToken = this.Buffer.CreateToken(
                state.CursorToken, newText);

            // Add the text replacement command into the buffer.
            const int SingleTokenReplacement = 1;

            command.Add(
                new ReplaceTokenOperation(
                    state.Cursor.LineIndex, 
                    state.Cursor.TokenIndex, 
                    SingleTokenReplacement, 
                    newToken));

            // Update the cursor location.
            TextLocation newCursor =
                this.Selection.First.AddTextIndex(text.Length);

            command.Add(new ReplaceSelectionOperation(newCursor));
        }

        #endregion
    }
}