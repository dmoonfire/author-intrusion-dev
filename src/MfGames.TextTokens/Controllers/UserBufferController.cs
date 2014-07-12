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

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Inserts text into a given location, chosing the appropriate token to modify
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

            // Get the token at this point in the buffer.
            IToken oldToken = this.Buffer.GetToken(
                textLocation.LineIndex, textLocation.TokenIndex);

            // Figure out the new text of the string and create a new token with the modified
            // version. This will also copy the attributes of the old token.
            string newText = oldToken.Text.Insert(
                textLocation.TextIndex.Index, text);

            IToken newToken = this.Buffer.NewToken(oldToken, newText);

            // Create a buffer command, add the replacement operation, and then
            // submit it to the buffer.
            const int SingleTokenReplacement = 1;
            var command = new BufferCommand
                {
                    new ReplaceTokenOperation(
                        textLocation.LineIndex, 
                        textLocation.TokenIndex, 
                        SingleTokenReplacement, 
                        newToken)
                };

            this.Buffer.Do(command);
        }

        /// <summary>
        /// </summary>
        public void Undo()
        {
        }

        #endregion
    }
}