// <copyright file="TestBufferState.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using MfGames.TextTokens.Buffers;
    using MfGames.TextTokens.Events;
    using MfGames.TextTokens.Lines;

    /// <summary>
    /// A testing class that listens to events of the Buffer like a text editor and
    /// keeps track of the current state. This is used to reflect what the user will
    /// see in the editor.
    /// </summary>
    public class TestBufferState
    {
        #region Constructors and Destructors

        /// <summary></summary>
        /// <param name="buffer"></param>
        public TestBufferState(IBuffer buffer)
        {
            // Establish our contracts.
            Contract.Requires(buffer != null);

            // Keep track of the backing variables.
            this.Buffer = buffer;

            // Hook up the events to the buffer.
            this.Buffer.LinesInserted += this.OnLinesInserted;

            // Initialize the collections.
            this.Lines = new List<Line>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// </summary>
        public List<Line> Lines { get; private set; }

        #endregion

        #region Properties

        /// <summary>
        /// </summary>
        protected IBuffer Buffer { get; private set; }

        #endregion

        #region Methods

        /// <summary></summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnLinesInserted(object sender, LinesInsertedEventArgs e)
        {
            // Wrap the lines in a line object.
            var insertedLines = new List<Line>(e.LinesInserted.Count);

            insertedLines.AddRange(
                e.LinesInserted.Select(line => new Line(line)));

            // Report which lines we've inserted.
            Console.WriteLine(
                "Inserted lines: "
                + string.Join(
                    ", ", 
                    insertedLines.Select(l => l.LineKey.ToString()).ToArray()));

            // Insert our copy of the line into the buffer.
            this.Lines.InsertRange(e.LineIndex.Index, insertedLines);
        }

        /// <summary>Called when a token is replaced with zero or more other tokens.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="TokenReplacedEventArgs"/> instance containing the event data.</param>
        private void OnTokenReplaced(object sender, TokenReplacedEventArgs e)
        {
        }

        #endregion
    }
}