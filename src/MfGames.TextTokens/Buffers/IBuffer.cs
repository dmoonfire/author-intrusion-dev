// <copyright file="IBuffer.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Buffers
{
    using System;
    using System.Collections.Generic;

    using MfGames.TextTokens.Events;
    using MfGames.TextTokens.Lines;

    /// <summary>
    /// Defines the public signature of a buffer which consists of zero or more lines which
    /// in turn have zero or more tokens. The buffer is the equivalent of a document in most
    /// text editors but can represent a virtualized document such as seamlessly combining
    /// multiple text documents into one or pulling data from another source.
    /// </summary>
    public interface IBuffer
    {
        #region Public Events

        /// <summary>
        /// Occurs when lines are deleted from the buffer.
        /// </summary>
        event EventHandler<LineIndexLinesDeletedEventArgs> LinesDeleted;

        /// <summary>
        /// Occurs when lines are inserted into the buffer.
        /// </summary>
        event EventHandler<LineIndexLinesInsertedEventArgs> LinesInserted;

        /// <summary>
        /// Occurs when a token is replaced by zero or more tokens.
        /// </summary>
        event EventHandler<LineIndexTokenIndexTokenReplacedEventArgs> TokenReplaced;

        /// <summary>
        /// Occurs when tokens are inserted into a buffer line.
        /// </summary>
        event EventHandler<LineIndexTokenIndexTokensInsertedEventArgs> TokensInserted;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the lines contained within the buffer.
        /// </summary>
        /// <value>
        /// The lines.
        /// </value>
        IReadOnlyList<ILine> Lines { get; }

        #endregion
    }
}