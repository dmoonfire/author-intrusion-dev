// <copyright file="LineIndexTokenIndexTokenReplacedEventArgs.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Events
{
    using System.Collections.Generic;

    using MfGames.TextTokens.Lines;
    using MfGames.TextTokens.Tokens;

    /// <summary>
    /// Indicates an event where a single token is replaced by zero or more tokens. This is
    /// also used to delete tokens, by providing an empty list of tokens.
    /// </summary>
    public class LineIndexTokenIndexTokenReplacedEventArgs :
        LineIndexTokenIndexEventArgs
    {
        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="LineIndexTokenIndexTokenReplacedEventArgs"/> class.</summary>
        /// <param name="lineIndex">Index of the line.</param>
        /// <param name="tokenIndex">Index of the token.</param>
        /// <param name="tokenReplacements">The token replacements.</param>
        /// <param name="isIdentityReplacement">if set to <c>true</c> [is identity replacement].</param>
        public LineIndexTokenIndexTokenReplacedEventArgs(
            LineIndex lineIndex, 
            TokenIndex tokenIndex, 
            IReadOnlyList<IToken> tokenReplacements, 
            bool isIdentityReplacement)
            : base(lineIndex, tokenIndex)
        {
            this.TokenReplacements = tokenReplacements;
            this.IsIdentity = isIdentityReplacement;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether the replacement is considered an identity replacement.
        /// Identity replacements are ones where the replaced token's text is identical to the
        /// concatenation of the replacement tokens. This is used in situations where the editor
        /// needs to maintain a cursor or selection while manipulations are made to the tokens.
        /// </summary>
        public bool IsIdentity { get; private set; }

        /// <summary>
        /// Gets the tokens that will replace the given token.
        /// </summary>
        /// <value>
        /// The token replacements.
        /// </value>
        public IReadOnlyList<IToken> TokenReplacements { get; private set; }

        #endregion
    }
}