// <copyright file="TokenIndexTokensInsertedEventArgs.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Events
{
    using System.Collections.Generic;

    using MfGames.TextTokens.Tokens;

    /// <summary>
    /// Indicates an event happened that inserted tokens into a line at
    /// a specific index, but without the line information. The sender of
    /// this event will always be a ILine.
    /// </summary>
    public class TokenIndexTokensInsertedEventArgs : TokenIndexEventArgs
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenIndexTokensInsertedEventArgs"/> class.
        /// </summary>
        /// <param name="tokenIndex">
        /// Index of the token.
        /// </param>
        /// <param name="tokensInserted">
        /// The tokens inserted.
        /// </param>
        public TokenIndexTokensInsertedEventArgs(
            TokenIndex tokenIndex, IReadOnlyList<IToken> tokensInserted)
            : base(tokenIndex)
        {
            this.TokensInserted = tokensInserted;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the tokens inserted by this event.
        /// </summary>
        /// <value>
        /// The tokens inserted.
        /// </value>
        public IReadOnlyList<IToken> TokensInserted { get; private set; }

        #endregion
    }
}