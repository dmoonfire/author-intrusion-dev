// <copyright file="ReplaceSelectionEventArgs.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Events
{
    using System;
    using System.Collections.Generic;

    using MfGames.TextTokens.Texts;

    /// <summary>
    /// </summary>
    public class ReplaceSelectionEventArgs : EventArgs
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplaceSelectionEventArgs"/> class.
        /// </summary>
        /// <param name="textRange">
        /// The text range.
        /// </param>
        public ReplaceSelectionEventArgs(TextRange textRange)
        {
            this.TextRange = textRange;
            this.OldTextRanges = new Dictionary<object, TextRange>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the old text ranges for any selection which updated itself from
        /// the event.
        /// </summary>
        /// <value>
        /// The old text ranges.
        /// </value>
        public Dictionary<object, TextRange> OldTextRanges { get; private set; }

        /// <summary>
        /// Gets the new text range.
        /// </summary>
        /// <value>
        /// The text range.
        /// </value>
        public TextRange TextRange { get; private set; }

        #endregion
    }
}