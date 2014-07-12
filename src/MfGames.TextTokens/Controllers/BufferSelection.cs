// <copyright file="BufferSelection.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Controllers
{
    using MfGames.TextTokens.Buffers;
    using MfGames.TextTokens.Texts;

    /// <summary>
    /// Encapsulates the functionality for cursors and selections within a buffer view. This
    /// is used by the UserBufferController to determine where the selection is and also for
    /// functionality that involves replacing the contents with input.
    /// </summary>
    public class BufferSelection
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BufferSelection"/> class.
        /// </summary>
        /// <param name="buffer">
        /// The buffer.
        /// </param>
        public BufferSelection(IBuffer buffer)
        {
            this.Buffer = buffer;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the anchor of the selection. An anchor is where the selection starts.
        /// </summary>
        /// <value>
        /// The anchor.
        /// </value>
        public TextLocation? Anchor { get; private set; }

        /// <summary>
        /// Gets the buffer associated with the selection.
        /// </summary>
        /// <value>
        /// The buffer.
        /// </value>
        public IBuffer Buffer { get; private set; }

        /// <summary>
        /// Gets the cursor location within the buffer.
        /// </summary>
        /// <value>
        /// The cursor.
        /// </value>
        public TextLocation Cursor { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Sets the cursor and clears the anchor.
        /// </summary>
        /// <param name="textLocation">The text location.</param>
        public void SetCursor(TextLocation textLocation)
        {
            this.Cursor = textLocation;
            this.Anchor = null;
        }

        #endregion
    }
}