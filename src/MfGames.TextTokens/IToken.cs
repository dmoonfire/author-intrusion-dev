// <copyright file="IToken.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens
{
    /// <summary>
    /// <para>Represents a single token within a line. Most tokens are text tokens
    /// which allow users to edit them, but tokens can also be used to represent
    /// ruby text (text above and below another text), read-only sections, or
    /// specially formatted codes.</para>
    /// 
    /// <para>To avoid memory pressure, a token may actually be the internal object,
    /// however because tokens are effectively immutable outside of two specific
    /// calls to the Buffer (ExecuteUserCommand and ExecuteBackgroundCommands).</para>
    /// </summary>
    public interface IToken
    {
    }
}