// <copyright file="InsertThreeLinesIntoSingleLineMiddleToken.cs" company="Moonfire Games">
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
    public class InsertThreeLinesTextIntoSingleLineMiddleToken :
        MemoryBufferTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// Verifies the cursor is in the correct location.
        /// </summary>
        [Test]
        public virtual void AnchorPositionIsRight()
        {
            this.Setup();
            Assert.AreEqual(
                new TextLocation(2, 0, 1), this.Controller.SelectionAnchor);
        }

        /// <summary>
        /// Verifies the cursor is in the correct location.
        /// </summary>
        [Test]
        public virtual void CursorPositionIsRight()
        {
            this.Setup();
            Assert.AreEqual(
                new TextLocation(2, 0, 1), this.Controller.SelectionCursor);
        }

        /// <summary>
        /// </summary>
        [Test]
        public virtual void FirstLineHasCorrectTokenCount()
        {
            this.Setup();
            Assert.AreEqual(3, this.State.Lines[0].Tokens.Count);
        }

        /// <summary>
        /// </summary>
        [Test]
        public virtual void FirstLineTextIsCorrect()
        {
            this.Setup();
            Assert.AreEqual(
                "zero on_", this.State.Lines[0].Tokens.GetVisibleText());
        }

        /// <summary>
        /// Verifies that there is only a single line in the buffer.
        /// </summary>
        [Test]
        public virtual void HasProperNumberOfLines()
        {
            this.Setup();
            Assert.AreEqual(3, this.State.Lines.Count);
        }

        /// <summary>
        /// </summary>
        [Test]
        public virtual void SecondLineHasCorrectTokenCount()
        {
            this.Setup();
            Assert.AreEqual(1, this.State.Lines[1].Tokens.Count);
        }

        /// <summary>
        /// </summary>
        [Test]
        public virtual void SecondLineTextIsCorrect()
        {
            this.Setup();
            Assert.AreEqual("|", this.State.Lines[1].Tokens.GetVisibleText());
        }

        /// <summary>
        /// </summary>
        [Test]
        public virtual void ThirdLineHasCorrectTokenCount()
        {
            this.Setup();
            Assert.AreEqual(3, this.State.Lines[2].Tokens.Count);
        }

        /// <summary>
        /// </summary>
        [Test]
        public virtual void ThirdLineTextIsCorrect()
        {
            this.Setup();
            Assert.AreEqual(
                "_e two", this.State.Lines[2].Tokens.GetVisibleText());
        }

        #endregion

        #region Methods

        /// <summary>
        /// </summary>
        protected override void Setup()
        {
            base.Setup();
            this.Buffer.PopulateRowColumn(1, 3);
            var textLocation = new TextLocation(0, 2, 2);
            this.Controller.InsertText(textLocation, "_\n|\n_");
        }

        #endregion

        /// <summary>
        /// </summary>
        public class Undo : InsertThreeLinesTextIntoSingleLineMiddleToken
        {
            #region Public Methods and Operators

            /// <summary>
            /// </summary>
            [Test]
            public override void AnchorPositionIsRight()
            {
                this.Setup();
                Assert.AreEqual(
                    new TextLocation(0, 2, 2), this.Controller.SelectionAnchor);
            }

            /// <summary>
            /// </summary>
            [Test]
            public override void CursorPositionIsRight()
            {
                this.Setup();
                Assert.AreEqual(
                    new TextLocation(0, 2, 2), this.Controller.SelectionAnchor);
            }

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
                base.Setup();
                this.Setup();
                Assert.AreEqual(
                    "zero one two", this.State.Lines[0].Tokens.GetVisibleText());
            }

            /// <summary>
            /// </summary>
            [Test]
            public override void HasProperNumberOfLines()
            {
                this.Setup();
                Assert.AreEqual(1, this.State.Lines.Count);
            }

            /// <summary>
            /// </summary>
            [Test]
            public override void SecondLineHasCorrectTokenCount()
            {
                Assert.Pass();
            }

            /// <summary>
            /// </summary>
            [Test]
            public override void SecondLineTextIsCorrect()
            {
                Assert.Pass();
            }

            /// <summary>
            /// </summary>
            public override void ThirdLineHasCorrectTokenCount()
            {
                Assert.Pass();
            }

            /// <summary>
            /// </summary>
            public override void ThirdLineTextIsCorrect()
            {
                Assert.Pass();
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
        public class UndoRedo : InsertThreeLinesTextIntoSingleLineMiddleToken
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

        /// <summary>
        /// </summary>
        public class UndoRedoUndoRedo :
            InsertThreeLinesTextIntoSingleLineMiddleToken
        {
            #region Methods

            /// <summary>
            /// </summary>
            protected override void Setup()
            {
                base.Setup();
                this.Controller.Undo();
                this.Controller.Redo();
                this.Controller.Undo();
                this.Controller.Redo();
            }

            #endregion
        }
    }
}