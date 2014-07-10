// <copyright file="IKeyGenerator.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens
{
    /// <summary>
    /// Represents a generator which creates the unique keys for tokens and lines.
    /// </summary>
    public interface IKeyGenerator
    {
        #region Public Methods and Operators

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        LineKey GetNextLineKey();

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        TokenKey GetNextTokenKey();

        #endregion
    }
}