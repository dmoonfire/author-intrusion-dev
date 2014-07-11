// <copyright file="MemoryBufferTests.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Tests
{
    using NUnit.Framework;

    /// <summary>
    /// </summary>
    public class MemoryBufferTests
    {
        #region Properties

        /// <summary>
        /// </summary>
        protected TestBuffer Buffer { get; private set; }

        /// <summary>
        /// </summary>
        protected TestBufferState State { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// </summary>
        protected void Setup()
        {
            KeyGenerator.Instance = new KeyGenerator();
            this.Buffer = new TestBuffer();
            this.State = new TestBufferState(this.Buffer);
        }

        #endregion

        /// <summary>
        /// </summary>
        [TestFixture]
        public class InsertLines : MemoryBufferTests
        {
            #region Public Methods and Operators

            /// <summary>
            /// </summary>
            [Test]
            public void InitialState()
            {
                this.Setup();
                Assert.AreEqual(
                    0, this.State.Lines.Count, "Number of lines was unexpected.");
            }

            /// <summary>
            /// </summary>
            [Test]
            public void InsertedFiveLines()
            {
                this.Setup();
                this.Buffer.PopulateRowColumn(5, 5, "aaa");
                Assert.AreEqual(
                    5, this.State.Lines.Count, "Number of lines was unexpected.");
            }

            #endregion
        }
    }
}