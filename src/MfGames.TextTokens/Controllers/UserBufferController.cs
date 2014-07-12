// <copyright file="UserBufferController.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Controllers
{
    using System.Diagnostics.Contracts;

    using MfGames.TextTokens.Buffers;
    using MfGames.TextTokens.Commands;
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
            IToken selectionToken = this.Selection.AddOperations(command);

            // Get the token at the first point of the buffer. If we had a selection, it would
            // have been deleted by the first command.
            TextLocation cursor = this.Selection.First;
            IToken oldToken = selectionToken
                ?? this.Buffer.GetToken(cursor.LineIndex, cursor.TokenIndex);

            // Figure out the new text of the string and create a new token with the modified
            // version. This will also copy the attributes of the old token.
            string newText = oldToken.Text.Insert(cursor.TextIndex.Index, text);
            IToken newToken = this.Buffer.CreateToken(oldToken, newText);

            // Add the text replacement command into the buffer.
            const int SingleTokenReplacement = 1;

            command.Add(
                new ReplaceTokenOperation(
                    cursor.LineIndex, 
                    cursor.TokenIndex, 
                    SingleTokenReplacement, 
                    newToken));

            // Update the cursor location.
            TextLocation newCursor =
                this.Selection.First.AddTextIndex(text.Length);

            command.Add(new ReplaceSelectionOperation(newCursor));

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
    }
}