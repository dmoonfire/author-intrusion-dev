// <copyright file="TestBuffer.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Tests
{
    using System;
    using System.Linq;

    using MfGames.TextTokens.Buffers;
    using MfGames.TextTokens.Lines;
    using MfGames.TextTokens.Tokens;

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
                // We create lines in two different ways. The first is to create an empty
                // line and then populate it completely. The other is to create a line with
                // initial data and then finish populating it via token operations.
                // We trigger this based on the column count. If lineIndex % columnCount is
                // zero, we create an empty line directly. If it isn't, we populate a line
                // with `mod` initial tokens before inserting it.
                // Regardless of how we populate the line, we finish up the rest of the lines
                // (columnCount - mod) with token operations.
                var afterLineIndex = new LineIndex(lineIndex);
                int initialTokens = lineIndex % columnCount;
                Line line;
                TokenKey tokenKey;
                Token token;

                if (initialTokens == 0)
                {
                    // Create an empty line via Insert lines.
                    line = (Line)this.InsertLines(afterLineIndex, 1).First();
                }
                else
                {
                    // Create the initial line.
                    LineKey lineKey = KeyGenerator.Instance.GetNextLineKey();
                    line = new Line(lineKey);

                    // Create the initial text for the line.
                    tokenKey = KeyGenerator.Instance.GetNextTokenKey();
                    token = new Token(tokenKey, text);
                    line.AddTokens(token);

                    for (int index = 1; index < initialTokens; index++)
                    {
                        // Add in the separating space.
                        tokenKey = KeyGenerator.Instance.GetNextTokenKey();
                        token = new Token(tokenKey, " ");
                        line.AddTokens(token);

                        // Add in the next text token.
                        tokenKey = KeyGenerator.Instance.GetNextTokenKey();
                        token = new Token(tokenKey, text);
                        line.AddTokens(token);
                    }

                    // Add the line into the buffer.
                    this.InsertLines(afterLineIndex, line);
                }

                // If we have a null line, then this is a critical problem.
                if (line == null)
                {
                    throw new InvalidOperationException(
                        "Could not get a line after inserting into bufer.");
                }

                // Add in the remaining lines, this should fire the appropriate events
                // with each AddToken event.
                for (int index = initialTokens; index < columnCount; index++)
                {
                    // Add in the separating space.
                    if (index > 0)
                    {
                        tokenKey = KeyGenerator.Instance.GetNextTokenKey();
                        token = new Token(tokenKey, " ");
                        line.AddTokens(token);
                    }

                    // Add in the next text token.
                    tokenKey = KeyGenerator.Instance.GetNextTokenKey();
                    token = new Token(tokenKey, text);
                    line.AddTokens(token);
                }
            }
        }

        #endregion
    }
}