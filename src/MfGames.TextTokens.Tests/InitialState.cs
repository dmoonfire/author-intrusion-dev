// <copyright file="InitialState.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Tests
{
    using NUnit.Framework;

    /// <summary>
    /// Verifies the state of an empty MemoryBuffer.
    /// </summary>
    [TestFixture]
    public class InitialState : MemoryBufferTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// Verifies the number of lines in the buffer.
        /// </summary>
        [Test]
        public void HasCorrectLineCount()
        {
            this.Setup();
            Assert.AreEqual(
                0, 
                this.State.Lines.Count, 
                "Number of lines was unexpected.");
        }

        #endregion
    }
}