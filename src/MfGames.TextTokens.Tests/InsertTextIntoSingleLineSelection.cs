// <copyright file="InsertTextIntoSingleLineSelection.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Tests
{
    using MfGames.TextTokens.Texts;

    using NUnit.Framework;

    /// <summary>
    /// </summary>
    [TestFixture]
    public class InsertTextIntoSingleLineSelection : MemoryBufferTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// </summary>
        [Test]
        public virtual void FirstLineHasCorrectTokenCount()
        {
            this.Setup();
            Assert.AreEqual(1, this.State.Lines[0].Tokens.Count);
        }

        /// <summary>
        /// </summary>
        [Test]
        public virtual void FirstLineTextIsCorrect()
        {
            this.Setup();
            Assert.AreEqual("zeBo", this.State.Lines[0].Tokens.GetVisibleText());
        }

        /// <summary>
        /// Verifies that there is only a single line in the buffer.
        /// </summary>
        [Test]
        public void HasOneLine()
        {
            this.Setup();
            Assert.AreEqual(1, this.State.Lines.Count);
        }

        #endregion

        #region Methods

        /// <summary>
        /// </summary>
        protected override void Setup()
        {
            base.Setup();
            this.Buffer.PopulateRowColumn(1, 3);
            var anchor = new TextLocation(0, 0, 2);
            var cursor = new TextLocation(0, 4, 2);
            this.Controller.SetCursor(anchor);
            this.Controller.Select(cursor);
            this.Controller.InsertText("B");
        }

        #endregion

        /// <summary>
        /// </summary>
        public class Undo : InsertTextIntoSingleLineSelection
        {
            #region Public Methods and Operators

            /// <summary>
            /// </summary>
            [Test]
            public override void FirstLineHasCorrectTokenCount()
            {
                this.Setup();
                Assert.AreEqual(5, this.State.Lines[0].Tokens.Count);
            }

            /// <summary>
            /// </summary>
            [Test]
            public override void FirstLineTextIsCorrect()
            {
                this.Setup();
                Assert.AreEqual(
                    "zero one two", this.State.Lines[0].Tokens.GetVisibleText());
            }

            #endregion

            #region Methods

            /// <summary>
            /// </summary>
            protected override void Setup()
            {
                base.Setup();
                this.Controller.Undo();
            }

            #endregion
        }

        /// <summary>
        /// </summary>
        public class UndoRedo : InsertTextIntoSingleLineSelection
        {
            #region Methods

            /// <summary>
            /// </summary>
            protected override void Setup()
            {
                base.Setup();
                this.Controller.Undo();
                this.Controller.Redo();
            }

            #endregion
        }

        /// <summary>
        /// </summary>
        public class UndoRedoUndo : Undo
        {
            #region Methods

            /// <summary>
            /// </summary>
            protected override void Setup()
            {
                base.Setup();
                this.Controller.Redo();
                this.Controller.Undo();
            }

            #endregion
        }
    }
}