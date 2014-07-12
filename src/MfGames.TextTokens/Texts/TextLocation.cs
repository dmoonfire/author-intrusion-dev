// <copyright file="TextLocation.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Texts
{
    using MfGames.TextTokens.Lines;
    using MfGames.TextTokens.Tokens;

    /// <summary>
    /// Encapsulates a line, token, and text index into a single structure.
    /// </summary>
    public struct TextLocation
    {
        #region Fields

        /// <summary>
        /// The line index for the location.
        /// </summary>
        public readonly LineIndex LineIndex;

        /// <summary>
        /// The text index for the location.
        /// </summary>
        public readonly TextIndex TextIndex;

        /// <summary>
        /// The token index for the location.
        /// </summary>
        public readonly TokenIndex TokenIndex;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TextLocation"/> struct.
        /// </summary>
        /// <param name="lineIndex">
        /// Index of the line.
        /// </param>
        /// <param name="tokenIndex">
        /// Index of the token.
        /// </param>
        /// <param name="textIndex">
        /// Index of the text.
        /// </param>
        public TextLocation(
            LineIndex lineIndex, TokenIndex tokenIndex, TextIndex textIndex)
        {
            this.LineIndex = lineIndex;
            this.TokenIndex = tokenIndex;
            this.TextIndex = textIndex;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextLocation"/> struct.
        /// </summary>
        /// <param name="lineIndex">
        /// Index of the line.
        /// </param>
        /// <param name="tokenIndex">
        /// Index of the token.
        /// </param>
        /// <param name="textIndex">
        /// Index of the text.
        /// </param>
        public TextLocation(int lineIndex, int tokenIndex, int textIndex)
            : this(
                new LineIndex(lineIndex), 
                new TokenIndex(tokenIndex), 
                new TextIndex(textIndex))
        {
        }

        #endregion
    }
}