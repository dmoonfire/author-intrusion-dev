// <copyright file="TestBuffer.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Tests
{
    using System.Linq;

    using MfGames.TextTokens.Buffers;
    using MfGames.TextTokens.Lines;

    /// <summary>
    /// Extends the MemoryBuffer to include some useful methods for populating the data
    /// or firing events.
    /// </summary>
    public class TestBuffer : MemoryBuffer
    {
        #region Public Methods and Operators

        /// <summary>
        /// Populates the memory buffer with the given text for a number of rows
        /// and columns as given.
        /// </summary>
        /// <param name="lineCount">
        /// The number of lines to add.
        /// </param>
        /// <param name="columnCount">
        /// The number of times to put the text in each line.
        /// </param>
        /// <param name="text">
        /// The text to insert.
        /// </param>
        public void PopulateRowColumn(
            int lineCount, int columnCount, string text)
        {
            // Go through the lines and each add one in turn.
            for (int lineIndex = 0; lineIndex < lineCount; lineIndex++)
            {
                // Create a new line and insert it into the buffer. We cheat because we
                // know it's a proper Line object.
                var line =
                    (Line)this.InsertLines(new LineIndex(lineIndex), 1).First();
            }
        }

        #endregion
    }
}