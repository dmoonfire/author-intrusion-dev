// <copyright file="DeleteRight1x4FromSingleLineMiddleToken.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Tests
{
    using MfGames.TextTokens.Texts;

    using NUnit.Framework;

    /// <summary>
    /// Tests deleting one character four times from the middle of the token. This
    /// verifies deleting two tokens and also merging tokens.
    /// </summary>
    [TestFixture]
    public class DeleteRight1x4FromSingleLineMiddleToken : MemoryBufferTests
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
                new TextLocation(
                    0, 
                    2, 
                    2), 
                this.Controller.SelectionAnchor);
        }

        /// <summary>
        /// Verifies the cursor is in the correct location.
        /// </summary>
        [Test]
        public virtual void CursorPositionIsRight()
        {
            this.Setup();
            Assert.AreEqual(
                new TextLocation(
                    0, 
                    2, 
                    2), 
                this.Controller.SelectionCursor);
        }

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
                "zero ono", 
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
                3, 
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
            var textLocation = new TextLocation(
                0, 
                2, 
                2);
            this.Controller.SetCursor(textLocation);
            this.Controller.DeleteRight(1);
            this.Controller.DeleteRight(1);
            this.Controller.DeleteRight(1);
            this.Controller.DeleteRight(1);
        }

        #endregion

        /// <summary>
        /// performs the contained task and then performs an undo.
        /// </summary>
        [TestFixture]
        public class Undo : DeleteRight1x4FromSingleLineMiddleToken
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
                    new TextLocation(
                        0, 
                        2, 
                        2), 
                    this.Controller.SelectionAnchor);
            }

            /// <summary>
            /// Verifies the cursor is in the correct location.
            /// </summary>
            [Test]
            public override void CursorPositionIsRight()
            {
                this.Setup();
                Assert.AreEqual(
                    new TextLocation(
                        0, 
                        2, 
                        2), 
                    this.Controller.SelectionCursor);
            }

            /// <summary>
            /// Verifies that line 1 has the correct text.
            /// </summary>
            [Test]
            public override void Line1HasCorrectText()
            {
                this.Setup();
                Assert.AreEqual(
                    "zero one two", 
                    this.State.Lines[0].Tokens.GetVisibleText());
            }

            /// <summary>
            /// Verifies that line 1 has the correct number of tokens.
            /// </summary>
            [Test]
            public override void Line1HasCorrectTokenCount()
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
                this.Controller.Undo();
                this.Controller.Undo();
                this.Controller.Undo();
                this.Controller.Undo();
            }

            #endregion
        }

        /// <summary>
        /// Perform the task, then an undo, and then a redo.
        /// </summary>
        [TestFixture]
        public class UndoRedo : DeleteRight1x4FromSingleLineMiddleToken
        {
            #region Methods

            /// <summary>
            /// Sets up the unit test.
            /// </summary>
            protected override void Setup()
            {
                base.Setup();
                this.Controller.Undo();
                this.Controller.Undo();
                this.Controller.Undo();
                this.Controller.Undo();
                this.Controller.Redo();
                this.Controller.Redo();
                this.Controller.Redo();
                this.Controller.Redo();
            }

            #endregion
        }

        /// <summary>
        /// Perform the task, undo, redo, and then an undo.
        /// </summary>
        [TestFixture]
        public class UndoRedoUndo : Undo
        {
            #region Methods

            /// <summary>
            /// Sets up the unit test.
            /// </summary>
            protected override void Setup()
            {
                base.Setup();
                this.Controller.Redo();
                this.Controller.Redo();
                this.Controller.Redo();
                this.Controller.Redo();
                this.Controller.Undo();
                this.Controller.Undo();
                this.Controller.Undo();
                this.Controller.Undo();
            }

            #endregion
        }
    }
}