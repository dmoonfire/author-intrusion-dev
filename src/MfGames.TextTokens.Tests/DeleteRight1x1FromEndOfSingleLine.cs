// <copyright file="DeleteRight1x1FromEndOfSingleLine.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Tests
{
    using MfGames.TextTokens.Texts;

    using NUnit.Framework;

    /// <summary>
    /// Tests deleting the last character of a line, which will merge the next
    /// line with the current one.
    /// </summary>
    [TestFixture]
    public class DeleteRight1x1FromEndOfSingleLine : MemoryBufferTests
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
                new TextLocation(0, 4, 3), this.Controller.SelectionAnchor);
        }

        /// <summary>
        /// Verifies the cursor is in the correct location.
        /// </summary>
        [Test]
        public virtual void CursorPositionIsRight()
        {
            this.Setup();
            Assert.AreEqual(
                new TextLocation(0, 4, 3), this.Controller.SelectionCursor);
        }

        /// <summary>
        /// Verifies that there is only a single line in the buffer.
        /// </summary>
        [Test]
        public virtual void HasCorrectLineCount()
        {
            this.Setup();
            Assert.AreEqual(1, this.State.Lines.Count);
        }

        /// <summary>
        /// Tests that line 1 has the correct text.
        /// </summary>
        [Test]
        public virtual void Line1HasCorrectText()
        {
            this.Setup();
            Assert.AreEqual(
                "zero one twothree four five", 
                this.State.Lines[0].Tokens.GetVisibleText());
        }

        /// <summary>
        /// Tests that line 1 has the correct number of tokens.
        /// </summary>
        [Test]
        public virtual void Line1HasCorrectTokenCount()
        {
            this.Setup();
            Assert.AreEqual(9, this.State.Lines[0].Tokens.Count);
        }

        /// <summary>
        /// Tests that line 2 has the correct text.
        /// </summary>
        [Test]
        public virtual void Line2HasCorrectText()
        {
            Assert.Pass();
        }

        /// <summary>
        /// Tests that line 2 has the correct number of tokens.
        /// </summary>
        [Test]
        public virtual void Line2HasCorrectTokenCount()
        {
            Assert.Pass();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets up the unit tests.
        /// </summary>
        protected override void Setup()
        {
            base.Setup();
            this.Buffer.PopulateRowColumn(2, 3);
            var textLocation = new TextLocation(0, 4, 3);
            this.Controller.SetCursor(textLocation);
            this.Controller.DeleteRight(1);
        }

        #endregion

        /// <summary>
        /// Perform the parent class and then performs an undo.
        /// </summary>
        [TestFixture]
        public class Undo : DeleteRight1x1FromEndOfSingleLine
        {
            #region Public Methods and Operators

            /// <summary>
            /// Verifies the cursor is in the correct location.
            /// </summary>
            [Test]
            public override void AnchorPositionIsRight()
            {
                this.Setup();
                Assert.AreEqual(
                    new TextLocation(0, 4, 3), this.Controller.SelectionAnchor);
            }

            /// <summary>
            /// Verifies the cursor is in the correct location.
            /// </summary>
            [Test]
            public override void CursorPositionIsRight()
            {
                this.Setup();
                Assert.AreEqual(
                    new TextLocation(0, 4, 3), this.Controller.SelectionCursor);
            }

            /// <summary>
            /// Verifies that there is only a single line in the buffer.
            /// </summary>
            [Test]
            public override void HasCorrectLineCount()
            {
                this.Setup();
                Assert.AreEqual(2, this.State.Lines.Count);
            }

            /// <summary>
            /// Tests that line 1 has the correct text.
            /// </summary>
            [Test]
            public override void Line1HasCorrectText()
            {
                this.Setup();
                Assert.AreEqual(
                    "zero one two", this.State.Lines[0].Tokens.GetVisibleText());
            }

            /// <summary>
            /// Tests that line 1 has the correct number of tokens.
            /// </summary>
            [Test]
            public override void Line1HasCorrectTokenCount()
            {
                this.Setup();
                Assert.AreEqual(5, this.State.Lines[0].Tokens.Count);
            }

            /// <summary>
            /// Tests that line 2 has the correct text.
            /// </summary>
            [Test]
            public override void Line2HasCorrectText()
            {
                this.Setup();
                Assert.AreEqual(
                    "three four five", 
                    this.State.Lines[1].Tokens.GetVisibleText());
            }

            /// <summary>
            /// Tests that line 2 has the correct number of tokens.
            /// </summary>
            [Test]
            public override void Line2HasCorrectTokenCount()
            {
                this.Setup();
                Assert.AreEqual(5, this.State.Lines[1].Tokens.Count);
            }

            #endregion

            #region Methods

            /// <summary>
            /// Sets up the unit tests.
            /// </summary>
            protected override void Setup()
            {
                base.Setup();
                this.Controller.Undo();
            }

            #endregion
        }

        /// <summary>
        /// Performs the parent task, then an undo, and then a redo.
        /// </summary>
        [TestFixture]
        public class UndoRedo : DeleteRight1x1FromEndOfSingleLine
        {
            #region Methods

            /// <summary>
            /// Sets up the unit tests.
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
        /// Performs the parent task, an undo, a redo, and an undo.
        /// </summary>
        [TestFixture]
        public class UndoRedoUndo : Undo
        {
            #region Methods

            /// <summary>
            /// Sets up the unit tests.
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