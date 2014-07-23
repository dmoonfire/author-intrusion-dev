// <copyright file="DefaultTokenizer.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Tokens
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Implements a simplistic tokenizer that splits on whitespace and puncutuation
    /// characters.
    /// </summary>
    public class DefaultTokenizer : ITokenizer
    {
        #region Fields

        /// <summary>
        /// Contains the list of separators that identify a token break.
        /// </summary>
        private readonly char[] TokenBreaks = new[]
            {
               ' ', '\t', '.', ';', ':', '\'', '"' 
            };

        #endregion

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
        public IEnumerable<string> Tokenize(string input)
        {
            // If we have a null, then return a blank string.
            if (input == null)
            {
                return new string[0] { };
            }

            // Split on whitespace and puncuation.
            string[] tokens = input.Split(
                this.TokenBreaks, StringSplitOptions.None);
            return tokens;
        }

        #endregion
    }
}