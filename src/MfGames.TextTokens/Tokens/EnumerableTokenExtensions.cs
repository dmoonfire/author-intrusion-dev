// <copyright file="EnumerableTokenExtensions.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Tokens
{
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// </summary>
    public static class EnumerableTokenExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// Gets the visible text from the collection of tokens.
        /// </summary>
        /// <param name="tokens">
        /// The tokens.
        /// </param>
        /// <returns>
        /// A string of the visible text.
        /// </returns>
        public static string GetVisibleText(this IEnumerable<IToken> tokens)
        {
            var buffer = new StringBuilder();

            foreach (IToken token in tokens)
            {
                buffer.Append(token.Text);
            }

            return buffer.ToString();
        }

        #endregion
    }
}