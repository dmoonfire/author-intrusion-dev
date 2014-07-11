// <copyright file="Line.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Lines
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using MfGames.TextTokens.Events;
    using MfGames.TextTokens.Tokens;

    /// <summary>
    /// A representative line object suitable for most buffers. This contains the basic
    /// functionality of ILine along with some additional methods for manipulation.
    /// </summary>
    public class Line : ILine
    {
        #region Fields

        /// <summary>
        /// The tokens contained in the line.
        /// </summary>
        private readonly List<IToken> tokens;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Line"/> class.
        /// </summary>
        public Line()
        {
            this.tokens = new List<IToken>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Line"/> class.
        /// </summary>
        /// <param name="lineKey">
        /// The line key.
        /// </param>
        public Line(LineKey lineKey)
            : this()
        {
            this.LineKey = lineKey;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Line"/> class.
        /// </summary>
        /// <param name="line">
        /// The line.
        /// </param>
        public Line(ILine line)
            : this()
        {
            Contract.Requires(line != null);

            this.LineKey = line.LineKey;
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Occurs when tokens are inserted into the line.
        /// </summary>
        public event EventHandler<TokenIndexTokensInsertedEventArgs> TokensInserted;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the line key associated with this line.
        /// </summary>
        /// <value>
        /// The line key.
        /// </value>
        public LineKey LineKey { get; private set; }

        /// <summary>
        /// Gets an ordered list of tokens within the line.
        /// </summary>
        public IReadOnlyList<IToken> Tokens
        {
            get
            {
                return this.tokens.AsReadOnly();
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Appends the token to the end of the list, raising events as
        /// appropriate.
        /// </summary>
        /// <param name="newTokens">
        /// The new tokens.
        /// </param>
        public void AddTokens(params IToken[] newTokens)
        {
            var afterTokenIndex = new TokenIndex(this.tokens.Count);
            this.InsertTokens(afterTokenIndex, newTokens);
        }

        /// <summary>
        /// Adds the tokens.
        /// </summary>
        /// <param name="newTokens">
        /// The new tokens.
        /// </param>
        public void AddTokens(IEnumerable<IToken> newTokens)
        {
            IToken[] tokenArray = newTokens.ToArray();
            this.AddTokens(tokenArray);
        }

        /// <summary>
        /// Inserts a token into the token list after the given index and then
        /// raises a token inserted event.
        /// </summary>
        /// <param name="afterTokenIndex">
        /// Index of the token to insert after.
        /// </param>
        /// <param name="newTokens">
        /// The token to insert.
        /// </param>
        public void InsertTokens(
            TokenIndex afterTokenIndex, IEnumerable<IToken> newTokens)
        {
            // Establish our contracts.
            Contract.Requires(afterTokenIndex.Index >= 0);
            Contract.Requires(newTokens != null);

            // Insert the token into the list.
            IToken[] tokenArray = newTokens.ToArray();

            this.tokens.InsertRange(afterTokenIndex.Index, tokenArray);

            // Raise an event to indicate we've inserted a token.
            this.RaiseTokensInserted(afterTokenIndex, tokenArray);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Raises the TokensInserted event.
        /// </summary>
        /// <param name="tokenIndex">
        /// Index of the token.
        /// </param>
        /// <param name="tokensInserted">
        /// The tokens inserted.
        /// </param>
        protected void RaiseTokensInserted(
            TokenIndex tokenIndex, IEnumerable<IToken> tokensInserted)
        {
            EventHandler<TokenIndexTokensInsertedEventArgs> listeners =
                this.TokensInserted;

            if (listeners == null)
            {
                return;
            }

            var args = new TokenIndexTokensInsertedEventArgs(
                tokenIndex, tokensInserted.ToList().AsReadOnly());

            listeners(this, args);
        }

        #endregion
    }
}