﻿// <copyright file="InsertLinesOperation.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Commands
{
    using MfGames.TextTokens.Buffers;
    using MfGames.TextTokens.Lines;

    /// <summary>
    /// Represents an operation that inserts lines into the buffer.
    /// </summary>
    public class InsertLinesOperation : IBufferOperation
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InsertLinesOperation"/> class.
        /// </summary>
        /// <param name="lineIndex">
        /// Index of the line.
        /// </param>
        /// <param name="count">
        /// The count.
        /// </param>
        public InsertLinesOperation(
            LineIndex lineIndex, 
            int count)
        {
            this.LineIndex = lineIndex;
            this.Count = count;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the number of lines to insert.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count { get; private set; }

        /// <summary>
        /// Gets the index of the line to insert after.
        /// </summary>
        /// <value>
        /// The index of the line.
        /// </value>
        public LineIndex LineIndex { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Performs the operation on the buffer.
        /// </summary>
        /// <param name="buffer">
        /// The buffer to execute the operations on.
        /// </param>
        public void Do(IBuffer buffer)
        {
            buffer.InsertLines(
                this.LineIndex, 
                this.Count);
        }

        /// <summary>
        /// Reverses the operation on the given buffer.
        /// </summary>
        /// <param name="buffer">
        /// The buffer to execute the operations on.
        /// </param>
        public void Undo(IBuffer buffer)
        {
            buffer.DeleteLines(
                this.LineIndex, 
                this.Count);
        }

        #endregion
    }
}