// <copyright file="TokenList.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Tokens
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Extends List&lt;IToken&gt; to include token-specific operations.
    /// </summary>
    /// <typeparam name="TToken">
    /// The class that extends IToken.
    /// </typeparam>
    public class TokenList<TToken> : List<TToken>
        where TToken : IToken
    {
        #region Public Indexers

        /// <summary>
        /// Gets the <see cref="TToken"/> with the specified key.
        /// </summary>
        /// <value>
        /// The <see cref="TToken"/>.
        /// </value>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The first token in the list that matches the key.
        /// </returns>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown when the key cannot be found.
        /// </exception>
        public TToken this[TokenKey key]
        {
            get
            {
                return this.First(t => t.TokenKey == key);
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public string GetVisibleText()
        {
            var buffer = new StringBuilder();

            foreach (TToken token in this)
            {
                buffer.Append(token.Text);
            }

            return buffer.ToString();
        }

        #endregion
    }
}