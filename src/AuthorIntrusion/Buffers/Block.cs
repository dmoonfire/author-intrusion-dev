// <copyright file="Block.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.Buffers
{
    /// <summary>
    /// Represents a single line or link inside the project.
    /// </summary>
    public class Block
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Block"/> class.
        /// </summary>
        public Block()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Block"/> class.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        public Block(string text)
            : this()
        {
            this.Text = text;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the line text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; set; }

        #endregion
    }
}