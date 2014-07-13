// <copyright file="InitialState.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Tests
{
    using NUnit.Framework;

    /// <summary>
    /// </summary>
    [TestFixture]
    public class InitialState : MemoryBufferTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// </summary>
        [Test]
        public void VerifyInitialState()
        {
            this.Setup();
            Assert.AreEqual(
                0, this.State.Lines.Count, "Number of lines was unexpected.");
        }

        #endregion
    }
}