// <copyright file="TokensInsertedEventArgs.cs" company="Moonfire Games">
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
    public class TokensInsertedEventArgs : LineEventArgs
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TokensInsertedEventArgs"/> class.
        /// </summary>
        /// <param name="lineIndex">
        /// Index of the line.
        /// </param>
        /// <param name="insertAfterTokenIndex">
        /// Index of the insert after token.
        /// </param>
        /// <param name="tokensInserted">
        /// The tokens inserted.
        /// </param>
        public TokensInsertedEventArgs(
            LineIndex lineIndex, 
            TokenIndex insertAfterTokenIndex, 
            IReadOnlyList<IToken> tokensInserted)
            : base(lineIndex)
        {
            this.InsertAfterTokenIndex = insertAfterTokenIndex;
            this.TokensInserted = tokensInserted;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the index of the token to insert after. This will be 0 for a prepend
        /// operation and ILine.Count for an append.
        /// </summary>
        /// <value>
        /// The index of the insert after token.
        /// </value>
        public TokenIndex InsertAfterTokenIndex { get; private set; }

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