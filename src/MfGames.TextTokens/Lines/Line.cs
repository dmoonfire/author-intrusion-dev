// <copyright file="Line.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Lines
{
    using System.Collections.Generic;

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
        /// </summary>
        /// <param name="lineKey">
        /// </param>
        public Line(LineKey lineKey)
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
        {
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
    }
}