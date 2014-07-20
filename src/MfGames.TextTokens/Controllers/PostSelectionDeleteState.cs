// <copyright file="PostSelectionDeleteState.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Controllers
{
    using MfGames.TextTokens.Texts;
    using MfGames.TextTokens.Tokens;

    /// <summary>
    /// A results class that contains the state of the buffer after a selection
    /// was deleted. If there was no selection, this will contain the appropriate
    /// variables to simplify logic.
    /// </summary>
    public class PostSelectionDeleteState
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PostSelectionDeleteState"/> class.
        /// </summary>
        /// <param name="cursor">
        /// The cursor.
        /// </param>
        /// <param name="cursorToken">
        /// The token at the cursor.
        /// </param>
        public PostSelectionDeleteState(TextLocation cursor, IToken cursorToken)
        {
            this.Cursor = cursor;
            this.CursorToken = cursorToken;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the cursor position after the delete.
        /// </summary>
        /// <value>
        /// The cursor.
        /// </value>
        public TextLocation Cursor { get; private set; }

        /// <summary>
        /// Gets the token under the cursor, which may be a modified token.
        /// </summary>
        /// <value>
        /// The cursor token.
        /// </value>
        public IToken CursorToken { get; private set; }

        #endregion
    }
}