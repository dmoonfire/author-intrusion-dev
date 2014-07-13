// <copyright file="InsertFiveLines.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Tests
{
    using NUnit.Framework;

    /// <summary>
    /// </summary>
    [TestFixture]
    public class InsertFiveLines : MemoryBufferTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// </summary>
        [Test]
        public void FifthLineTextIsCorrect()
        {
            this.Setup();
            Assert.AreEqual(
                "twenty twenty-one twenty-two twenty-three twenty-four", 
                this.State.Lines[4].Tokens.GetVisibleText());
        }

        /// <summary>
        /// </summary>
        [Test]
        public void FirstLineHasNineTokens()
        {
            this.Setup();
            Assert.AreEqual(9, this.State.Lines[0].Tokens.Count);
        }

        /// <summary>
        /// </summary>
        [Test]
        public void FirstLineTextIsCorrect()
        {
            this.Setup();
            Assert.AreEqual(
                "zero one two three four", 
                this.State.Lines[0].Tokens.GetVisibleText());
        }

        /// <summary>
        /// </summary>
        [Test]
        public void ForthLineTextIsCorrect()
        {
            this.Setup();
            Assert.AreEqual(
                "fifteen sixteen seventeen eighteen nineteen", 
                this.State.Lines[3].Tokens.GetVisibleText());
        }

        /// <summary>
        /// </summary>
        [Test]
        public void HasFiveLines()
        {
            this.Setup();
            Assert.AreEqual(5, this.State.Lines.Count);
        }

        /// <summary>
        /// </summary>
        [Test]
        public void SecondLineTextIsCorrect()
        {
            this.Setup();
            Assert.AreEqual(
                "five six seven eight nine", 
                this.State.Lines[1].Tokens.GetVisibleText());
        }

        /// <summary>
        /// </summary>
        [Test]
        public void ThirdLineTextIsCorrect()
        {
            this.Setup();
            Assert.AreEqual(
                "ten eleven twelve thirteen fourteen", 
                this.State.Lines[2].Tokens.GetVisibleText());
        }

        #endregion

        #region Methods

        /// <summary>
        /// </summary>
        protected override void Setup()
        {
            base.Setup();
            this.Buffer.PopulateRowColumn(5, 5);
        }

        #endregion
    }
}