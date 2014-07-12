// <copyright file="BufferCommand.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Commands
{
    using System.Collections.Generic;

    using MfGames.TextTokens.Buffers;

    /// <summary>
    /// A command that can be undone or redone.
    /// </summary>
    public class BufferCommand : List<IBufferOperation>
    {
        #region Public Methods and Operators

        /// <summary>
        /// Performs the operations on the given buffer.
        /// </summary>
        /// <param name="buffer">
        /// The buffer.
        /// </param>
        public void Do(IBuffer buffer)
        {
            foreach (IBufferOperation operation in this)
            {
                operation.Do(buffer);
            }
        }

        /// <summary>
        /// Reverses the operations on the given buffer.
        /// </summary>
        /// <param name="buffer">
        /// The buffer.
        /// </param>
        public void Undo(IBuffer buffer)
        {
            foreach (IBufferOperation operation in this)
            {
                operation.Undo(buffer);
            }
        }

        #endregion
    }
}