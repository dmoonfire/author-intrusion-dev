// <copyright file="TestLine.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Tests
{
    using System.Collections.Generic;

    using MfGames.TextTokens.Lines;
    using MfGames.TextTokens.Tokens;

    /// <summary>
    /// </summary>
    public class TestLine
    {
        #region Constructors and Destructors

        /// <summary>
        /// </summary>
        /// <param name="lineKey">
        /// </param>
        public TestLine(LineKey lineKey)
        {
            this.Tokens = new TokenList<TestToken>();
            this.LineKey = lineKey;
        }

        /// <summary>
        /// </summary>
        /// <param name="line">
        /// </param>
        public TestLine(ILine line)
            : this(line.LineKey)
        {
            // Add in all the tokens already existing on the line.
            foreach (IToken token in line.Tokens)
            {
                var testToken = new TestToken(token);
                this.Tokens.Add(testToken);
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// </summary>
        public LineKey LineKey { get; private set; }

        /// <summary>
        /// </summary>
        public TokenList<TestToken> Tokens { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// </summary>
        /// <param name="tokenIndex">
        /// </param>
        /// <param name="tokensInserted">
        /// </param>
        public void InsertTokens(
            TokenIndex tokenIndex, IEnumerable<IToken> tokensInserted)
        {
            // First wrap the tokens in a test token layer.
            var tokens = new List<TestToken>();

            foreach (IToken token in tokensInserted)
            {
                var testToken = new TestToken(token);
                tokens.Add(testToken);
            }

            // Insert the tokens into the list.
            this.Tokens.InsertRange(tokenIndex.Index, tokens);
        }

        #endregion
    }
}