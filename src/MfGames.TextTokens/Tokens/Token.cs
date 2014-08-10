// <copyright file="Token.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Tokens
{
    using System.Diagnostics.Contracts;

    /// <summary>
    /// A concrete implementation of Token which is used by the internal classes.
    /// </summary>
    public class Token : IToken
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Token"/> class.
        /// </summary>
        /// <param name="tokenKey">
        /// The token key.
        /// </param>
        /// <param name="text">
        /// The text.
        /// </param>
        public Token(
            TokenKey tokenKey, 
            string text)
        {
            this.TokenKey = tokenKey;
            this.Text = text;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Token"/> class.
        /// </summary>
        /// <param name="token">
        /// The token.
        /// </param>
        public Token(IToken token)
            : this(token.TokenKey, 
                token.Text)
        {
            // Establish our contracts.
            Contract.Requires(token != null);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the text associated with this token.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; private set; }

        /// <summary>
        /// Gets the token key for this token.
        /// </summary>
        /// <value>
        /// The token key.
        /// </value>
        public TokenKey TokenKey { get; private set; }

        #endregion
    }
}