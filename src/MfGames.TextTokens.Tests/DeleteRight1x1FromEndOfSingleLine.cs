// <copyright file="DeleteRight1x1FromEndOfSingleLine.cs" company="Moonfire Games">
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
        /// </summary>
        [Test]
        public virtual void Line1HasCorrectTokenCount()
        {
            this.Setup();
            Assert.AreEqual(9, this.State.Lines[0].Tokens.Count);
        }

        /// <summary>
        /// </summary>
        [Test]
        public virtual void Line2HasCorrectText()
        {
            Assert.Pass();
        }

        /// <summary>
        /// </summary>
        [Test]
        public virtual void Line2HasCorrectTokenCount()
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
            this.Buffer.PopulateRowColumn(2, 3);
            var textLocation = new TextLocation(0, 4, 3);
            this.Controller.SetCursor(textLocation);
            this.Controller.DeleteRight(1);
        }

        #endregion

        /// <summary>
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
            /// </summary>
            [Test]
            public override void HasCorrectLineCount()
            {
                this.Setup();
                Assert.AreEqual(2, this.State.Lines.Count);
            }

            /// <summary>
            /// </summary>
            [Test]
            public override void Line1HasCorrectText()
            {
                this.Setup();
                Assert.AreEqual(
                    "zero one two", this.State.Lines[0].Tokens.GetVisibleText());
            }

            /// <summary>
            /// </summary>
            [Test]
            public override void Line1HasCorrectTokenCount()
            {
                this.Setup();
                Assert.AreEqual(5, this.State.Lines[0].Tokens.Count);
            }

            /// <summary>
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
        [TestFixture]
        public class UndoRedo : DeleteRight1x1FromEndOfSingleLine
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
        [TestFixture]
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