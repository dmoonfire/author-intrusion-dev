// <copyright file="ITokenizer.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Tokens
{
    using System.Collections.Generic;

    /// <summary>
    /// </summary>
    public interface ITokenizer
    {
        #region Public Methods and Operators

        /// <summary>
        /// Tokenizes the specified input and returns a list of individual tokens.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// An enumeration of string tokens, starting from left to right.
        /// </returns>
        IEnumerable<string> Tokenize(string input);

        #endregion
    }
}