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

    /// <summary>
    /// A testing class that listens to events of the Buffer like a text editor and
    /// keeps track of the current state. This is used to reflect what the user will
    /// see in the editor.
    /// </summary>
    public class TestBufferState
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TestBufferState"/> class.
        /// </summary>
        /// <param name="buffer">
        /// The buffer.
        /// </param>
        public TestBufferState(IBuffer buffer)
        {
            // Establish our contracts.
            Contract.Requires(buffer != null);

            // Keep track of the backing variables.
            this.Buffer = buffer;

            // Hook up the events to the buffer.
            this.Buffer.LinesInserted += this.OnLinesInserted;
            this.Buffer.TokensInserted += this.OnTokensInserted;

            // Initialize the collections.
            this.Lines = new List<TestLine>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the lines.
        /// </summary>
        /// <value>
        /// The lines.
        /// </value>
        public List<TestLine> Lines { get; private set; }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the buffer.
        /// </summary>
        /// <value>
        /// The buffer.
        /// </value>
        protected IBuffer Buffer { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// </summary>
        /// <param name="sender">
        /// </param>
        /// <param name="e">
        /// </param>
        private void OnLinesInserted(
            object sender, LineIndexLinesInsertedEventArgs e)
        {
            // Wrap the lines in a line object.
            var insertedLines = new List<TestLine>(e.LinesInserted.Count);

            insertedLines.AddRange(
                e.LinesInserted.Select(line => new TestLine(line)));

            // Report which lines we've inserted.
            Console.WriteLine(
                "Inserted lines: "
                + string.Join(
                    ", ", 
                    insertedLines.Select(l => l.LineKey.ToString()).ToArray()));

            // Insert our copy of the line into the buffer.
            this.Lines.InsertRange(e.LineIndex.Index, insertedLines);
        }

        /// <summary>
        /// Called when a token is replaced with zero or more other tokens.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="LineIndexTokenIndexTokenReplacedEventArgs"/> instance containing the event data.
        /// </param>
        private void OnTokenReplaced(
            object sender, LineIndexTokenIndexTokenReplacedEventArgs e)
        {
        }

        /// <summary>
        /// Called when tokens are inserted into a line.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="LineIndexTokenIndexTokensInsertedEventArgs"/> instance containing the event data.
        /// </param>
        private void OnTokensInserted(
            object sender, LineIndexTokenIndexTokensInsertedEventArgs e)
        {
            // Report which tokens are being added.
            string tokenList = string.Join(
                ", ", 
                e.TokensInserted.Select(t => t.TokenKey.ToString()).ToArray());

            Console.WriteLine(
                "Inserted lines: @(" + e.LineIndex.Index + ", "
                + e.TokenIndex.Index + ") " + tokenList);

            // Get the referenced line.
            TestLine line = this.Lines[e.LineIndex.Index];

            // Insert the tokens into the line.
            line.InsertTokens(e.TokenIndex, e.TokensInserted);
        }

        #endregion
    }
}