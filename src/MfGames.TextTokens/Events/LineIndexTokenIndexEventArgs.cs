﻿// <copyright file="LineIndexTokenIndexEventArgs.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Events
{
    using MfGames.TextTokens.Lines;
    using MfGames.TextTokens.Tokens;

    /// <summary>
    /// Base class for events that perform an action at a specific line and token index.
    /// </summary>
    public abstract class LineIndexTokenIndexEventArgs : LineIndexEventArgs
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LineIndexTokenIndexEventArgs"/> class.
        /// </summary>
        /// <param name="lineIndex">
        /// Index of the line.
        /// </param>
        /// <param name="tokenIndex">
        /// Index of the token.
        /// </param>
        protected LineIndexTokenIndexEventArgs(
            LineIndex lineIndex, 
            TokenIndex tokenIndex)
            : base(lineIndex)
        {
            this.TokenIndex = tokenIndex;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the index of the token.
        /// </summary>
        /// <value>
        /// The index of the token.
        /// </value>
        public TokenIndex TokenIndex { get; private set; }

        #endregion
    }
}