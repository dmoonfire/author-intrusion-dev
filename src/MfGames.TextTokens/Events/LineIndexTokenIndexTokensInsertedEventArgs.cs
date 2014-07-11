// <copyright file="LineIndexTokenIndexTokensInsertedEventArgs.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Events
{
    using System.Collections.Generic;

    using MfGames.TextTokens.Lines;
    using MfGames.TextTokens.Tokens;

    /// <summary>
    /// Represents an event where a token is inserted into a line. This will
    /// identify the token to insert after (0 for a prepend or Count for an append)
    /// and one or more tokens to insert.
    /// </summary>
    public class LineIndexTokenIndexTokensInsertedEventArgs :
        LineIndexTokenIndexEventArgs
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LineIndexTokenIndexTokensInsertedEventArgs"/> class.
        /// </summary>
        /// <param name="lineIndex">
        /// Index of the line.
        /// </param>
        /// <param name="tokenIndex">
        /// Index of the token to insert after.
        /// </param>
        /// <param name="tokensInserted">
        /// The tokens inserted.
        /// </param>
        public LineIndexTokenIndexTokensInsertedEventArgs(
            LineIndex lineIndex, 
            TokenIndex tokenIndex, 
            IReadOnlyList<IToken> tokensInserted)
            : base(lineIndex, tokenIndex)
        {
            this.TokensInserted = tokensInserted;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the tokens to be inserted.
        /// </summary>
        /// <value>
        /// The tokens inserted.
        /// </value>
        public IReadOnlyList<IToken> TokensInserted { get; private set; }

        #endregion
    }
}