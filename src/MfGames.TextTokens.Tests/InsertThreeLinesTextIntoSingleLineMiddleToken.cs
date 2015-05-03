// <copyright file="InsertThreeLinesTextIntoSingleLineMiddleToken.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Tests
{
    using MfGames.TextTokens.Texts;

    using NUnit.Framework;

    /// <summary>
    /// Verifies the result of pasting three lines into a token.
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
                new TextLocation(
                    2, 
                    0, 
                    1), 
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
                    2, 
                    0, 
                    1), 
                this.Controller.SelectionCursor);
        }

        /// <summary>
        /// Verifies that there is only a single line in the buffer.
        /// </summary>
        [Test]
        public virtual void HasProperNumberOfLines()
        {
            this.Setup();
            Assert.AreEqual(
                3, 
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
                "zero on_", 
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
                4, 
                this.State.Lines[0].Tokens.Count);
        }

        /// <summary>
        /// Verifies that line 2 has the correct text.
        /// </summary>
        [Test]
        public virtual void Line2HasCorrectText()
        {
            this.Setup();
            Assert.AreEqual(
                "|", 
                this.State.Lines[1].Tokens.GetVisibleText());
        }

        /// <summary>
        /// Verifies that line 2 has the correct number of tokens.
        /// </summary>
        [Test]
        public virtual void Line2HasCorrectTokenCount()
        {
            this.Setup();
            Assert.AreEqual(
                1, 
                this.State.Lines[1].Tokens.Count);
        }

        /// <summary>
        /// Verifies that line 3 has the correct text.
        /// </summary>
        [Test]
        public virtual void Line3HasCorrectText()
        {
            this.Setup();
            Assert.AreEqual(
                "_e two", 
                this.State.Lines[2].Tokens.GetVisibleText());
        }

        /// <summary>
        /// Verifies line 3 has the correct number of tokens.
        /// </summary>
        [Test]
        public virtual void Line3HasCorrectTokenCount()
        {
            this.Setup();
            Assert.AreEqual(
                4, 
                this.State.Lines[2].Tokens.Count);
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
            this.Controller.InsertText(
                textLocation, 
                "_\n|\n_");
        }

        #endregion

        /// <summary>
        /// Performs the task and then an undo.
        /// </summary>
        public class Undo : InsertThreeLinesTextIntoSingleLineMiddleToken
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
                    this.Controller.SelectionAnchor);
            }

            /// <summary>
            /// Verifies that there is only a single line in the buffer.
            /// </summary>
            [Test]
            public override void HasProperNumberOfLines()
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
            public override void Line1HasCorrectText()
            {
                base.Setup();
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

            /// <summary>
            /// Verifies that line 2 has the correct text.
            /// </summary>
            [Test]
            public override void Line2HasCorrectText()
            {
                Assert.Pass();
            }

            /// <summary>
            /// Verifies that line 2 has the correct number of tokens.
            /// </summary>
            [Test]
            public override void Line2HasCorrectTokenCount()
            {
                Assert.Pass();
            }

            /// <summary>
            /// Verifies that line 3 has the correct text.
            /// </summary>
            public override void Line3HasCorrectText()
            {
                Assert.Pass();
            }

            /// <summary>
            /// Verifies line 3 has the correct number of tokens.
            /// </summary>
            public override void Line3HasCorrectTokenCount()
            {
                Assert.Pass();
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
            }

            #endregion
        }

        /// <summary>
        /// Performs the task, an undo, and then a redo.
        /// </summary>
        public class UndoRedo : InsertThreeLinesTextIntoSingleLineMiddleToken
        {
            #region Methods

            /// <summary>
            /// Sets up the unit test.
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
        /// Performs the task, an undo, a redo, and then an undo.
        /// </summary>
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
                this.Controller.Undo();
            }

            #endregion
        }

        /// <summary>
        /// Performs the task, an undo, a redo, an undo, and then a redo.
        /// </summary>
        public class UndoRedoUndoRedo :
            InsertThreeLinesTextIntoSingleLineMiddleToken
        {
            #region Methods

            /// <summary>
            /// Sets up the unit test.
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