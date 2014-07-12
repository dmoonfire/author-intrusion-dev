// <copyright file="IBufferOperation.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Commands
{
    using MfGames.TextTokens.Buffers;

    /// <summary>
    /// Represents a single undoable operation that can be performed against the
    /// buffer.
    /// </summary>
    public interface IBufferOperation
    {
        #region Public Methods and Operators

        /// <summary>
        /// Performs the operation on the buffer.
        /// </summary>
        /// <param name="buffer">
        /// The buffer to execute the operations on.
        /// </param>
        void Do(IBuffer buffer);

        #endregion
    }
}