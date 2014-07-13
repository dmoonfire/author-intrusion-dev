﻿// <copyright file="MemoryBufferTests.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Tests
{
    using System;

    using MfGames.TextTokens.Controllers;
    using MfGames.TextTokens.Texts;
    using MfGames.TextTokens.Tokens;

    using NUnit.Framework;

    /// <summary>
    /// </summary>
    [TestFixture]
    public class MemoryBufferTests
    {
        #region Properties

        /// <summary>
        /// Contains an in-memory buffer model.
        /// </summary>
        protected TestBuffer Buffer { get; private set; }

        /// <summary>
        /// Contains a UI controller for the buffer.
        /// </summary>
        protected UserBufferController Controller { get; private set; }

        /// <summary>
        /// Contains a listener which reflects the user-visible state of
        /// the buffer.
        /// </summary>
        protected TestBufferState State { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            // Report the state of the final buffer.
            Console.WriteLine();
            Console.WriteLine("Buffer State:");

            for (int index = 0; index < this.Buffer.Lines.Count; index++)
            {
                // For each line, give the line index and each token separated by [] brackets.
                Console.Write("{0}: ", index.ToString().PadLeft(4));

                foreach (IToken token in this.Buffer.Lines[index].Tokens)
                {
                    Console.Write("[{0}]", token.Text);
                }

                // Finish up the line.
                Console.WriteLine();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Generic setup for all memory buffer tests.
        /// </summary>
        protected virtual void Setup()
        {
            KeyGenerator.Instance = new KeyGenerator();
            this.Buffer = new TestBuffer();
            this.Controller = new UserBufferController(this.Buffer);
            this.State = new TestBufferState(this.Buffer);
        }

        #endregion

        /// <summary>
        /// </summary>
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

        /// <summary>
        /// </summary>
        [TestFixture]
        public class InsertMultilineTextIntoSingleLineMiddleToken :
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
                    new TextLocation(1, 0, 1), this.Controller.SelectionAnchor);
            }

            /// <summary>
            /// Verifies the cursor is in the correct location.
            /// </summary>
            [Test]
            public virtual void CursorPositionIsRight()
            {
                this.Setup();
                Assert.AreEqual(
                    new TextLocation(1, 0, 1), this.Controller.SelectionCursor);
            }

            /// <summary>
            /// </summary>
            [Test]
            public void FirstLineHasCorrectTokenCount()
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
            public void HasProperNumberOfLines()
            {
                this.Setup();
                Assert.AreEqual(2, this.State.Lines.Count);
            }

            /// <summary>
            /// </summary>
            [Test]
            public void SecondLineHasCorrectTokenCount()
            {
                this.Setup();
                Assert.AreEqual(3, this.State.Lines[1].Tokens.Count);
            }

            /// <summary>
            /// </summary>
            [Test]
            public virtual void SecondLineTextIsCorrect()
            {
                this.Setup();
                Assert.AreEqual(
                    "_e two", this.State.Lines[1].Tokens.GetVisibleText());
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
                this.Controller.InsertText(textLocation, "_\n_");
            }

            #endregion
        }

        /// <summary>
        /// </summary>
        [TestFixture]
        public class InsertTextIntoSingleLineMiddleToken : MemoryBufferTests
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
                    new TextLocation(0, 2, 3), this.Controller.SelectionAnchor);
            }

            /// <summary>
            /// Verifies the cursor is in the correct location.
            /// </summary>
            [Test]
            public virtual void CursorPositionIsRight()
            {
                this.Setup();
                Assert.AreEqual(
                    new TextLocation(0, 2, 3), this.Controller.SelectionCursor);
            }

            /// <summary>
            /// </summary>
            [Test]
            public void FirstLineHasCorrectTokenCount()
            {
                this.Setup();
                Assert.AreEqual(5, this.State.Lines[0].Tokens.Count);
            }

            /// <summary>
            /// </summary>
            [Test]
            public virtual void FirstLineTextIsCorrect()
            {
                this.Setup();
                Assert.AreEqual(
                    "zero onBe two", this.State.Lines[0].Tokens.GetVisibleText());
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
                var textLocation = new TextLocation(0, 2, 2);
                this.Controller.InsertText(textLocation, "B");
            }

            #endregion
        }

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
                Assert.AreEqual(
                    "zeBo", this.State.Lines[0].Tokens.GetVisibleText());
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
        }

        /// <summary>
        /// </summary>
        [TestFixture]
        public class InsertTextIntoSingleTokenSelection : MemoryBufferTests
        {
            #region Public Methods and Operators

            /// <summary>
            /// </summary>
            [Test]
            public virtual void FirstLineHasCorrectTokenCount()
            {
                this.Setup();
                Assert.AreEqual(5, this.State.Lines[0].Tokens.Count);
            }

            /// <summary>
            /// </summary>
            [Test]
            public virtual void FirstLineTextIsCorrect()
            {
                this.Setup();
                Assert.AreEqual(
                    "zero oBe two", this.State.Lines[0].Tokens.GetVisibleText());
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
                var anchor = new TextLocation(0, 2, 1);
                var cursor = new TextLocation(0, 2, 2);
                this.Controller.SetCursor(anchor);
                this.Controller.Select(cursor);
                this.Controller.InsertText("B");
            }

            #endregion
        }

        /// <summary>
        /// </summary>
        [TestFixture]
        public class RedoUndoInsertTextIntoSingleLineMiddleToken :
            UndoInsertTextIntoSingleLineMiddleToken
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
                    new TextLocation(0, 2, 3), this.Controller.SelectionAnchor);
            }

            /// <summary>
            /// Verifies the cursor is in the correct location.
            /// </summary>
            [Test]
            public override void CursorPositionIsRight()
            {
                this.Setup();
                Assert.AreEqual(
                    new TextLocation(0, 2, 3), this.Controller.SelectionCursor);
            }

            /// <summary>
            /// </summary>
            [Test]
            public override void FirstLineTextIsCorrect()
            {
                this.Setup();
                Assert.AreEqual(
                    "zero onBe two", this.State.Lines[0].Tokens.GetVisibleText());
            }

            #endregion

            #region Methods

            /// <summary>
            /// </summary>
            protected override void Setup()
            {
                base.Setup();
                this.Controller.Redo();
            }

            #endregion
        }

        /// <summary>
        /// </summary>
        public class RedoUndoInsertTextIntoSingleLineSelection :
            UndoInsertTextIntoSingleLineSelection
        {
            #region Public Methods and Operators

            /// <summary>
            /// </summary>
            [Test]
            public override void FirstLineHasCorrectTokenCount()
            {
                this.Setup();
                Assert.AreEqual(1, this.State.Lines[0].Tokens.Count);
            }

            /// <summary>
            /// </summary>
            [Test]
            public override void FirstLineTextIsCorrect()
            {
                this.Setup();
                Assert.AreEqual(
                    "zeBo", this.State.Lines[0].Tokens.GetVisibleText());
            }

            #endregion

            #region Methods

            /// <summary>
            /// </summary>
            protected override void Setup()
            {
                base.Setup();
                this.Controller.Redo();
            }

            #endregion
        }

        /// <summary>
        /// </summary>
        [TestFixture]
        public class UndoInsertTextIntoSingleLineMiddleToken :
            InsertTextIntoSingleLineMiddleToken
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
                    new TextLocation(0, 2, 2), this.Controller.SelectionAnchor);
            }

            /// <summary>
            /// Verifies the cursor is in the correct location.
            /// </summary>
            [Test]
            public override void CursorPositionIsRight()
            {
                this.Setup();
                Assert.AreEqual(
                    new TextLocation(0, 2, 2), this.Controller.SelectionCursor);
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
        public class UndoInsertTextIntoSingleLineSelection :
            InsertTextIntoSingleLineSelection
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
        [TestFixture]
        public class UndoRedoUndoInsertTextIntoSingleLineMiddleToken :
            RedoUndoInsertTextIntoSingleLineMiddleToken
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
                    new TextLocation(0, 2, 2), this.Controller.SelectionAnchor);
            }

            /// <summary>
            /// Verifies the cursor is in the correct location.
            /// </summary>
            [Test]
            public override void CursorPositionIsRight()
            {
                this.Setup();
                Assert.AreEqual(
                    new TextLocation(0, 2, 2), this.Controller.SelectionCursor);
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
        public class UndoRedoUndoInsertTextIntoSingleLineSelection :
            RedoUndoInsertTextIntoSingleLineSelection
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
    }
}