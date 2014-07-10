// <copyright file="IList.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a single line inside the buffer. Each line consists of zero or more
    /// IToken objects, ordered from left to right.
    /// </summary>
    public interface IList
    {
        #region Public Properties

        /// <summary>
        /// Contains the ordered list of tokens within the line.
        /// </summary>
        IReadOnlyList<IToken> Tokens { get; }

        #endregion
    }
}