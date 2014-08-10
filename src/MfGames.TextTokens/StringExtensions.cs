// <copyright file="StringExtensions.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens
{
    /// <summary>
    /// Contains extension methods for System.String.
    /// </summary>
    public static class StringExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// Converts line ending characters of all types into Unix endings.
        /// </summary>
        /// <param name="input">
        /// The input string to normalize.
        /// </param>
        /// <returns>
        /// A string with all newlines normalized to "\n". If the input
        /// is null, then this returns a blank string.
        /// </returns>
        public static string NormalizeNewlines(this string input)
        {
            // If we have a null, then return a blank string.
            if (input == null)
            {
                return string.Empty;
            }

            // For everything else, normalize the returns.
            return input.Replace(
                "\r\n", 
                "\n")
                .Replace(
                    "\r", 
                    "\n");
        }

        #endregion
    }
}