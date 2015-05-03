// <copyright file="InsertTextIntoSingleTokenSelection.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Tests
{
    using MfGames.TextTokens.Texts;

    using NUnit.Framework;

    /// <summary>
    /// Inserts text into a selection within a token.
    /// </summary>
    [TestFixture]
    public class InsertTextIntoSingleTokenSelection : MemoryBufferTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// Verifies that there is only a single line in the buffer.
        /// </summary>
        [Test]
        public void HasCorrectLineCount()
        {
            this.Setup();
            Assert.AreEqual(
                1, 
                this.State.Lines.Count);
        }

        /// <summary>
        /// Verifies that line 1 has the correct text.
        /// </summary>
        [Test]
        public virtual void Line1HasCorrectText()
        {
            this.Setup();
            Assert.AreEqual(
                "zero oBe two", 
                this.State.Lines[0].Tokens.GetVisibleText());
        }

        /// <summary>
        /// Verifies that line 1 has the correct number of tokens.
        /// </summary>
        [Test]
        public virtual void Line1HasCorrectTokenCount()
        {
            this.Setup();
            Assert.AreEqual(
                5, 
                this.State.Lines[0].Tokens.Count);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets up the unit test.
        /// </summary>
        protected override void Setup()
        {
            base.Setup();
            this.Buffer.PopulateRowColumn(
                1, 
                3);
            var anchor = new TextLocation(
                0, 
                2, 
                1);
            var cursor = new TextLocation(
                0, 
                2, 
                2);
            this.Controller.SetCursor(anchor);
            this.Controller.Select(cursor);
            this.Controller.InsertText("B");
        }

        #endregion
    }
}