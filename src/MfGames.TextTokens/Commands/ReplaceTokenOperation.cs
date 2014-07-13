// <copyright file="ReplaceTokenOperation.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Commands
{
    using System.Collections.Generic;
    using System.Linq;

    using MfGames.TextTokens.Buffers;
    using MfGames.TextTokens.Lines;
    using MfGames.TextTokens.Tokens;

    /// <summary>
    /// Indicates an operation that replaces a single token with a set of
    /// zero or more tokens.
    /// </summary>
    public class ReplaceTokenOperation : IBufferOperation
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplaceTokenOperation"/> class.
        /// </summary>
        /// <param name="lineIndex">
        /// Index of the line.
        /// </param>
        /// <param name="tokenIndex">
        /// Index of the token.
        /// </param>
        /// <param name="count">
        /// The count.
        /// </param>
        /// <param name="replacementTokens">
        /// The new tokens.
        /// </param>
        public ReplaceTokenOperation(
            LineIndex lineIndex, 
            TokenIndex tokenIndex, 
            int count, 
            params IToken[] replacementTokens)
        {
            this.LineIndex = lineIndex;
            this.TokenIndex = tokenIndex;
            this.Count = count;
            this.ReplacementTokens = replacementTokens;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplaceTokenOperation"/> class.
        /// </summary>
        /// <param name="lineIndex">
        /// Index of the line.
        /// </param>
        /// <param name="tokenIndex">
        /// Index of the token.
        /// </param>
        /// <param name="count">
        /// The count.
        /// </param>
        /// <param name="replacementTokens">
        /// The replacement tokens.
        /// </param>
        public ReplaceTokenOperation(
            LineIndex lineIndex, 
            TokenIndex tokenIndex, 
            int count, 
            IEnumerable<IToken> replacementTokens)
        {
            this.LineIndex = lineIndex;
            this.TokenIndex = tokenIndex;
            this.Count = count;
            this.ReplacementTokens = replacementTokens.ToArray();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count { get; private set; }

        /// <summary>
        /// Gets or sets the index of the line.
        /// </summary>
        /// <value>
        /// The index of the line.
        /// </value>
        public LineIndex LineIndex { get; set; }

        /// <summary>
        /// Gets the tokens that were replaced or null if the operation has not been
        /// completed.
        /// </summary>
        public IToken[] ReplacedTokens { get; private set; }

        /// <summary>
        /// Gets or sets the new tokens.
        /// </summary>
        /// <value>
        /// The new tokens.
        /// </value>
        public IToken[] ReplacementTokens { get; private set; }

        /// <summary>
        /// Gets the index of the token.
        /// </summary>
        /// <value>
        /// The index of the token.
        /// </value>
        public TokenIndex TokenIndex { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Replaces the OldToken with the NewTokens.
        /// </summary>
        /// <param name="buffer">
        /// The buffer to execute the operations on.
        /// </param>
        public void Do(IBuffer buffer)
        {
            IEnumerable<IToken> replacedTokens =
                buffer.ReplaceTokens(
                    this.LineIndex, 
                    this.TokenIndex, 
                    this.Count, 
                    this.ReplacementTokens);

            this.ReplacedTokens = replacedTokens.ToArray();
        }

        /// <summary>
        /// Reverses the operation on the given buffer.
        /// </summary>
        /// <param name="buffer">
        /// </param>
        public void Undo(IBuffer buffer)
        {
            buffer.ReplaceTokens(
                this.LineIndex, 
                this.TokenIndex, 
                this.ReplacementTokens.Length, 
                this.ReplacedTokens);
        }

        #endregion
    }
}