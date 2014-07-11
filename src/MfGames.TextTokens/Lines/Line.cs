// <copyright file="Line.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Lines
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

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

        /// <summary>Initializes a new instance of the <see cref="Line"/> class.</summary>
        /// <param name="lineKey">The line key.</param>
        public Line(LineKey lineKey)
            : this()
        {
            this.LineKey = lineKey;
        }

        /// <summary>Initializes a new instance of the <see cref="Line"/> class.</summary>
        /// <param name="line">The line.</param>
        public Line(ILine line)
            : this()
        {
            Contract.Requires(line != null);

            this.LineKey = line.LineKey;
        }

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

        /// <summary>Appends the token to the end of the list, raising events as
        /// appropriate.</summary>
        /// <param name="token">The token to append.</param>
        public void AddToken(IToken token)
        {
            this.InsertToken(this.tokens.Count, token);
        }

        /// <summary>Inserts a token into the token list after the given index and then
        /// raises a token inserted event.</summary>
        /// <param name="afterTokenIndex">Index of the token to insert after.</param>
        /// <param name="token">The token to insert.</param>
        public void InsertToken(int afterTokenIndex, IToken token)
        {
            // Establish our contracts.
            Contract.Requires(afterTokenIndex >= 0);

            // Insert the token into the list.
            this.tokens.Insert(afterTokenIndex, token);

            // Raise an event to indicate we've inserted a token.
        }

        #endregion
    }
}