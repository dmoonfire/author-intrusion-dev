// <copyright file="BufferCommand.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Commands
{
    using System.Collections.Generic;

    /// <summary>
    /// A command that can be undone or redone.
    /// </summary>
    public class BufferCommand : List<IBufferOperation>
    {
    }
}