// <copyright file="TestToken.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Tests
{
    using System.Diagnostics.Contracts;

    using MfGames.TextTokens.Tokens;

    /// <summary>
    /// </summary>
    public class TestToken : IToken
    {
        #region Constructors and Destructors

        /// <summary>
        /// </summary>
        /// <param name="tokenKey">
        /// </param>
        /// <param name="text">
        /// </param>
        public TestToken(TokenKey tokenKey, string text)
        {
            this.TokenKey = tokenKey;
            this.Text = text;
        }

        /// <summary>
        /// </summary>
        /// <param name="token">
        /// </param>
        public TestToken(IToken token)
            : this(token.TokenKey, token.Text)
        {
            // Establish our contracts.
            Contract.Requires(token != null);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// </summary>
        public TokenKey TokenKey { get; private set; }

        #endregion
    }
}