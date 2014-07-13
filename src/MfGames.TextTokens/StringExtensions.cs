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
        /// </param>
        /// <returns>
        /// </returns>
        public static string NormalizeNewlines(this string input)
        {
            return input.Replace("\r\n", "\n").Replace("\r", "\n");
        }

        #endregion
    }
}