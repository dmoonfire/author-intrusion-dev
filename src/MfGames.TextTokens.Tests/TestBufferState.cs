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
            this.Buffer.LinesDeleted += this.OnLinesDeleted;
            this.Buffer.LinesInserted += this.OnLinesInserted;
            this.Buffer.TokensReplaced += this.OnTokensReplaced;

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
        /// Called when the lines are deleted.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="LineIndexLinesDeletedEventArgs"/> instance containing the event data.
        /// </param>
        private void OnLinesDeleted(
            object sender, LineIndexLinesDeletedEventArgs e)
        {
            this.Lines.RemoveRange(
                e.LineIndex.Index, 
                e.Count);
        }

        /// <summary>
        /// Called when lines are inserted into the buffer.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="LineIndexLinesInsertedEventArgs"/> instance containing the event data.
        /// </param>
        private void OnLinesInserted(
            object sender, LineIndexLinesInsertedEventArgs e)
        {
            // Wrap the lines in a line object.
            var insertedLines = new List<TestLine>(e.LinesInserted.Count);

            insertedLines.AddRange(
                e.LinesInserted.Select(line => new TestLine(line)));

            // Report which lines we've inserted.
            string[] lineKeys =
                insertedLines.Select(l => l.LineKey.ToString()).ToArray();
            string text = string.Join(
                ", ", 
                lineKeys);
            Console.WriteLine(
                "Inserted lines: {0}", 
                text);

            // Insert our copy of the line into the buffer.
            this.Lines.InsertRange(
                e.LineIndex.Index, 
                insertedLines);
        }

        /// <summary>
        /// Called when a token is replaced with zero or more other tokens.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="LineIndexTokenIndexTokensReplacedEventArgs"/> instance containing the event data.
        /// </param>
        private void OnTokensReplaced(
            object sender, LineIndexTokenIndexTokensReplacedEventArgs e)
        {
            // Report which tokens are being replaced and added.
            string tokenList = string.Join(
                ", ", 
                e.ReplacementTokens.Select(t => t.TokenKey.ToString()).ToArray());

            Console.WriteLine(
                "Replaced tokens: @({0}, {1}x{2}): {3}", 
                e.LineIndex.Index, 
                e.TokenIndex.Index, 
                e.Count, 
                tokenList);

            // Get the line we are making these changes.
            TestLine line = this.Lines[e.LineIndex.Index];

            // First remove the tokens, if we have at least one.
            if (e.Count > 0)
            {
                line.Tokens.RemoveRange(
                    e.TokenIndex.Index, 
                    e.Count);
            }

            // Insert the newly created lines, if we have any.
            if (e.ReplacementTokens.Length > 0)
            {
                // Insert the tokens into the line.
                line.InsertTokens(
                    e.TokenIndex, 
                    e.ReplacementTokens);
            }
        }

        #endregion
    }
}