// <copyright file="InsertFiveLines.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Tests
{
    using NUnit.Framework;

    /// <summary>
    /// Verifies the result of inserting five lines.
    /// </summary>
    [TestFixture]
    public class InsertFiveLines : MemoryBufferTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// Verifies that the buffer has the correct number of lines.
        /// </summary>
        [Test]
        public void HasCorrectLineCount()
        {
            this.Setup();
            Assert.AreEqual(
                5, 
                this.State.Lines.Count);
        }

        /// <summary>
        /// Verifies that line 1 has the correct text.
        /// </summary>
        [Test]
        public void Line1HasCorrectText()
        {
            this.Setup();
            Assert.AreEqual(
                "zero one two three four", 
                this.State.Lines[0].Tokens.GetVisibleText());
        }

        /// <summary>
        /// Verifies that line 1 has the correct number of tokens.
        /// </summary>
        [Test]
        public void Line1HasCorrectTokenCount()
        {
            this.Setup();
            Assert.AreEqual(
                9, 
                this.State.Lines[0].Tokens.Count);
        }

        /// <summary>
        /// Verifies that line 2 has the correct text.
        /// </summary>
        [Test]
        public void Line2HasCorrectText()
        {
            this.Setup();
            Assert.AreEqual(
                "five six seven eight nine", 
                this.State.Lines[1].Tokens.GetVisibleText());
        }

        /// <summary>
        /// Verifies that line 3 has the correct text.
        /// </summary>
        [Test]
        public void Line3HasCorrectText()
        {
            this.Setup();
            Assert.AreEqual(
                "ten eleven twelve thirteen fourteen", 
                this.State.Lines[2].Tokens.GetVisibleText());
        }

        /// <summary>
        /// Verifies that line 4 has the correct text.
        /// </summary>
        [Test]
        public void Line4HasCorrectText()
        {
            this.Setup();
            Assert.AreEqual(
                "fifteen sixteen seventeen eighteen nineteen", 
                this.State.Lines[3].Tokens.GetVisibleText());
        }

        /// <summary>
        /// Verifies that line 5 has the correct text.
        /// </summary>
        [Test]
        public void Line5HasCorrectText()
        {
            this.Setup();
            Assert.AreEqual(
                "twenty twenty-one twenty-two twenty-three twenty-four", 
                this.State.Lines[4].Tokens.GetVisibleText());
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
                5, 
                5);
        }

        #endregion
    }
}