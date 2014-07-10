// <copyright file="TokenReplacedEventArgs.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Indicates an event where a single token is replaced by zero or more tokens. This is
    /// also used to delete tokens, by providing an empty list of tokens.
    /// </summary>
    public class TokenReplacedEventArgs : EventArgs
    {
        #region Constructors and Destructors

        /// <summary>
        /// </summary>
        /// <param name="lineSequence">
        /// </param>
        /// <param name="tokenSequence">
        /// </param>
        /// <param name="tokenReplacements">
        /// </param>
        /// <param name="isIdentityReplacement">
        /// </param>
        public TokenReplacedEventArgs(
            LineSequence lineSequence, 
            TokenSequence tokenSequence, 
            IReadOnlyList<IToken> tokenReplacements, 
            bool isIdentityReplacement)
        {
            this.LineSequence = lineSequence;
            this.TokenSequence = tokenSequence;
            this.TokenReplacements = tokenReplacements;
            this.IsIdentity = isIdentityReplacement;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// If this is true, then the replacement is considered an identity replacement. Identity
        /// replacements are ones where the replaced token's text is identical to the concatenation
        /// of the replacement tokens. This is used in situations where the editor needs to maintain
        /// a cursor or selection while manipulations are made to the tokens.
        /// </summary>
        public bool IsIdentity { get; private set; }

        /// <summary>
        /// </summary>
        public LineSequence LineSequence { get; private set; }

        /// <summary>
        /// </summary>
        public IReadOnlyList<IToken> TokenReplacements { get; private set; }

        /// <summary>
        /// </summary>
        public TokenSequence TokenSequence { get; set; }

        #endregion
    }
}