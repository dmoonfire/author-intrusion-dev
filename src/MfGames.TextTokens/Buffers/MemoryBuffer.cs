// <copyright file="MemoryBuffer.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Buffers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Collections.ObjectModel;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using MfGames.TextTokens.Commands;
    using MfGames.TextTokens.Events;
    using MfGames.TextTokens.Lines;
    using MfGames.TextTokens.Texts;
    using MfGames.TextTokens.Tokens;

    /// <summary>
    /// Implements an IBuffer that is based by an in-memory array of line objects.
    /// </summary>
    public class MemoryBuffer : IBuffer
    {
        #region Fields

        /// <summary>
        /// </summary>
        private readonly HashSet<ILine> changedLines;

        /// <summary>
        /// The lines contained with the buffer.
        /// </summary>
        private readonly List<Line> lines;

        /// <summary>
        /// </summary>
        private DefaultTokenizer tokenizer;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryBuffer"/> class.
        /// </summary>
        public MemoryBuffer()
        {
            this.lines = new List<Line>();
            this.changedLines = new HashSet<ILine>();
            this.Tokenizer = new DefaultTokenizer();
            this.UndoCommands = new Stack<BufferCommand>();
            this.RedoCommands = new Stack<BufferCommand>();
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Occurs when lines are deleted from the buffer.
        /// </summary>
        public event EventHandler<LineIndexLinesDeletedEventArgs> LinesDeleted;

        /// <summary>
        /// Occurs when lines are inserted into the buffer.
        /// </summary>
        public event EventHandler<LineIndexLinesInsertedEventArgs> LinesInserted;

        /// <summary>
        /// Occurs when the selection should be replaced because of a command.
        /// </summary>
        public event EventHandler<ReplaceSelectionEventArgs> ReplaceSelection;

        /// <summary>
        /// Occurs when the selection should be restored, typically after
        /// undoing a command.
        /// </summary>
        public event EventHandler<RestoreSelectionEventArgs> RestoreSelection;

        /// <summary>
        /// Occurs when a token is replaced by zero or more tokens.
        /// </summary>
        public event EventHandler<LineIndexTokenIndexTokensReplacedEventArgs> TokensReplaced;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the lines contained within the buffer.
        /// </summary>
        /// <value>
        /// The lines.
        /// </value>
        public IReadOnlyList<ILine> Lines
        {
            get
            {
                return this.lines.AsReadOnly();
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        protected DefaultTokenizer Tokenizer
        {
            get
            {
                return this.tokenizer;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(
                        "value", "Cannot assign a null tokenizer to the buffer.");
                }

                this.tokenizer = value;
            }
        }

        /// <summary>
        /// Gets or sets the redo commands.
        /// </summary>
        /// <value>
        /// The redo commands.
        /// </value>
        private Stack<BufferCommand> RedoCommands { get; set; }

        /// <summary>
        /// Gets or sets the undo commands currently on the buffer.
        /// </summary>
        /// <value>
        /// The undo commands.
        /// </value>
        private Stack<BufferCommand> UndoCommands { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Creates a new token.
        /// </summary>
        /// <param name="newText">
        /// The new text.
        /// </param>
        /// <returns>
        /// A constructed token.
        /// </returns>
        public IToken CreateToken(string newText)
        {
            TokenKey tokenKey = KeyGenerator.Instance.GetNextTokenKey();
            var token = new Token(tokenKey, newText);
            return token;
        }

        /// <summary>
        /// </summary>
        /// <param name="oldToken">
        /// </param>
        /// <param name="newText">
        /// </param>
        /// <returns>
        /// </returns>
        public IToken CreateToken(IToken oldToken, string newText)
        {
            TokenKey tokenKey = KeyGenerator.Instance.GetNextTokenKey();
            var token = new Token(tokenKey, newText);
            return token;
        }

        /// <summary>
        /// Deletes lines from the buffer, starting with the given index.
        /// </summary>
        /// <param name="lineIndex">
        /// Index of the line to start deleting.
        /// </param>
        /// <param name="count">
        /// The number of lines to delete.
        /// </param>
        /// <returns>
        /// The lines deleted from the buffer.
        /// </returns>
        public IEnumerable<ILine> DeleteLines(LineIndex lineIndex, int count)
        {
            // Retrieve the list of lines deleted.
            List<Line> deletedLines = this.lines.GetRange(
                lineIndex.Index, count);

            // Remove the lines from the buffer.
            this.lines.RemoveRange(lineIndex.Index, count);

            // Raise the event to indicate we deleted the lines.
            this.RaiseLinesDeleted(lineIndex, count);

            // Return the resulting lines.
            return deletedLines;
        }

        /// <summary>
        /// Executes a command on the buffer, running through each operation in turn.
        /// </summary>
        /// <param name="command">
        /// The command.
        /// </param>
        public void Do(BufferCommand command)
        {
            // Performs the operations of the command.
            this.DoPostUpdateAction(command.Do);

            // Add this command to our undo stack.
            this.UndoCommands.Push(command);
        }

        /// <summary>
        /// Gets the index of the line.
        /// </summary>
        /// <param name="line">
        /// The line.
        /// </param>
        /// <returns>
        /// A line index of the given line.
        /// </returns>
        public LineIndex GetLineIndex(ILine line)
        {
            // Establish our contracts.
            Contract.Requires(line != null);

            // Find the line's index, wrap it, and return it.
            int index = this.lines.FindIndex(l => l.LineKey == line.LineKey);
            var lineIndex = new LineIndex(index);
            return lineIndex;
        }

        /// <summary>
        /// </summary>
        /// <param name="lineIndex">
        /// </param>
        /// <param name="tokenIndex">
        /// </param>
        /// <returns>
        /// </returns>
        public IToken GetToken(LineIndex lineIndex, TokenIndex tokenIndex)
        {
            ILine line = this.Lines[lineIndex.Index];
            IToken token = line.Tokens[tokenIndex.Index];
            return token;
        }

        /// <summary>
        /// Inserts the lines.
        /// </summary>
        /// <param name="afterLineIndex">
        /// Index of the after line.
        /// </param>
        /// <param name="count">
        /// The count.
        /// </param>
        /// <returns>
        /// An enumerable of the created lines.
        /// </returns>
        public IEnumerable<ILine> InsertLines(
            LineIndex afterLineIndex, int count)
        {
            // First populate a list of line keys for the new lines.
            var insertedLines = new Line[count];

            for (int index = 0; index < count; index++)
            {
                LineKey lineKey = KeyGenerator.Instance.GetNextLineKey();
                insertedLines[index] = new Line(lineKey);
            }

            // Insert the lines into the buffer.
            this.InsertLines(afterLineIndex, insertedLines);

            // Return the resulting lines.
            return insertedLines;
        }

        /// <summary>
        /// Inserts the lines.
        /// </summary>
        /// <param name="afterLineIndex">
        /// Index of the after line.
        /// </param>
        /// <param name="insertedLines">
        /// The inserted lines.
        /// </param>
        public void InsertLines(
            LineIndex afterLineIndex, params Line[] insertedLines)
        {
            // Establish our contracts.
            Contract.Requires(afterLineIndex.Index >= 0);
            Contract.Requires(insertedLines != null);

            // Insert the lines into the buffer.
            this.InsertLines(afterLineIndex, (IEnumerable<Line>)insertedLines);
        }

        /// <summary>
        /// Inserts the lines into the buffer.
        /// </summary>
        /// <param name="afterLineIndex">
        /// Index of the after line.
        /// </param>
        /// <param name="insertedLines">
        /// The inserted lines.
        /// </param>
        public void InsertLines(
            LineIndex afterLineIndex, IEnumerable<ILine> insertedLines)
        {
            // Subscribe to the events of these lines.
            List<Line> lineArray =
                insertedLines.Select(l => (l as Line) ?? new Line(l)).ToList();

            lineArray.ForEach(
                l => l.TokensReplaced += this.OnLineTokensReplaced);

            // Insert the lines into the buffer at the given position.
            this.lines.InsertRange(afterLineIndex.Index, lineArray);

            // Raise an event for the inserted lines.
            this.RaiseLinesInserted(afterLineIndex, lineArray);
        }

        /// <summary>
        /// Raises the ReplaceSelection event with the given arguments.
        /// </summary>
        /// <param name="newTextRange">
        /// The new text range.
        /// </param>
        /// <returns>
        /// A dictionary of the old selection items.
        /// </returns>
        /// <exception cref="System.NotImplementedException">
        /// </exception>
        public Dictionary<object, TextRange> RaiseReplaceSelection(
            TextRange newTextRange)
        {
            // If we don't have any listeners, then return an empty selection.
            EventHandler<ReplaceSelectionEventArgs> listeners =
                this.ReplaceSelection;

            if (listeners == null)
            {
                return new Dictionary<object, TextRange>();
            }

            // Otherwise, raise the event.
            var args = new ReplaceSelectionEventArgs(newTextRange);

            listeners(this, args);

            return args.OldTextRanges;
        }

        /// <summary>
        /// Raises the restore selection event.
        /// </summary>
        /// <param name="oldTextRanges">
        /// The old text ranges.
        /// </param>
        /// <exception cref="System.NotImplementedException">
        /// </exception>
        public void RaiseRestoreSelection(
            Dictionary<object, TextRange> oldTextRanges)
        {
            // If we have no listeners, then don't do anything.
            EventHandler<RestoreSelectionEventArgs> listeners =
                this.RestoreSelection;

            if (listeners == null)
            {
                return;
            }

            // Otherwise, raise the event.
            var args = new RestoreSelectionEventArgs(oldTextRanges);

            listeners(this, args);
        }

        /// <summary>
        /// Re-executes the last undone command (reverses the undo) or do nothing if
        /// there are no redoable commands.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Redo()
        {
            // If there are no undo commands, then do nothing.
            if (this.RedoCommands.Count == 0)
            {
                return;
            }

            // Grab the last command and reexecute it.
            BufferCommand command = this.RedoCommands.Pop();
            this.UndoCommands.Push(command);

            // Perform the action.
            this.DoPostUpdateAction(command.Do);
        }

        /// <summary>
        /// Replaces the tokens.
        /// </summary>
        /// <param name="lineIndex">
        /// Index of the line.
        /// </param>
        /// <param name="tokenIndex">
        /// Index of the token.
        /// </param>
        /// <param name="count">
        /// The count.
        /// </param>
        /// <param name="newTokens">
        /// The new tokens.
        /// </param>
        /// <returns>
        /// The tokens replaced.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// count;Count cannot be less than zero.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// newTokens;newTokens cannot be null.
        /// </exception>
        public IEnumerable<IToken> ReplaceTokens(
            LineIndex lineIndex, 
            TokenIndex tokenIndex, 
            int count, 
            IEnumerable<IToken> newTokens)
        {
            // Establish our contracts.
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(
                    "count", "Count cannot be less than zero.");
            }

            if (newTokens == null)
            {
                throw new ArgumentNullException(
                    "newTokens", "newTokens cannot be null.");
            }

            // Get the line and tokens for this request.
            Line line = this.lines[lineIndex.Index];
            List<Token> oldTokens = line.Tokens.GetRange(
                tokenIndex.Index, count);

            // Make sure we have the right type.
            Token[] tokenArray =
                newTokens.Select(t => t as Token ?? new Token(t)).ToArray();

            // Figure out if these two tokens are identity.
            string oldText = oldTokens.GetVisibleText();
            string newText = tokenArray.GetVisibleText();
            TokenReplacement replacementType = oldText == newText
                ? TokenReplacement.Identity
                : TokenReplacement.Different;

            // Replace the tokens in our collection.
            line.Tokens.RemoveRange(tokenIndex.Index, count);
            line.Tokens.InsertRange(tokenIndex.Index, tokenArray);

            // Raise an event about the change.
            this.RaiseTokensReplaced(
                lineIndex, tokenIndex, count, tokenArray, replacementType);

            // Return the replaced tokens.
            return oldTokens;
        }

        /// <summary>
        /// Executes the reverse operation of the last command or do nothing if there
        /// are no undoable commands.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Undo()
        {
            // If there are no undo commands, then do nothing.
            if (this.UndoCommands.Count == 0)
            {
                return;
            }

            // Grab the last command and reexecute it.
            BufferCommand command = this.UndoCommands.Pop();
            this.RedoCommands.Push(command);

            // Perform the undo action and update the internal state.
            this.DoPostUpdateAction(command.Undo);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Raises the LinesInserted event.
        /// </summary>
        /// <param name="afterLineIndex">
        /// Index of the after line.
        /// </param>
        /// <param name="insertedLines">
        /// The inserted lines.
        /// </param>
        protected void RaiseLinesInserted(
            LineIndex afterLineIndex, IEnumerable<ILine> insertedLines)
        {
            EventHandler<LineIndexLinesInsertedEventArgs> listener =
                this.LinesInserted;

            if (listener == null)
            {
                return;
            }

            ReadOnlyCollection<ILine> readOnlyLines =
                new List<ILine>(insertedLines).AsReadOnly();
            var args = new LineIndexLinesInsertedEventArgs(
                afterLineIndex, readOnlyLines);

            listener(this, args);
        }

        /// <summary>
        /// Raises the token replaced event.
        /// </summary>
        /// <param name="lineIndex">
        /// Index of the line.
        /// </param>
        /// <param name="tokenIndex">
        /// Index of the token.
        /// </param>
        /// <param name="count">
        /// The number of tokens to replace.
        /// </param>
        /// <param name="replacementTokens">
        /// The replacement tokens.
        /// </param>
        /// <param name="replacementType">
        /// Type of the replacement.
        /// </param>
        protected void RaiseTokensReplaced(
            LineIndex lineIndex, 
            TokenIndex tokenIndex, 
            int count, 
            IEnumerable<IToken> replacementTokens, 
            TokenReplacement replacementType)
        {
            // Make sure we have at least one listener for this event.
            EventHandler<LineIndexTokenIndexTokensReplacedEventArgs> listener =
                this.TokensReplaced;

            if (listener == null)
            {
                return;
            }

            // Ensure that the collection is wrapped properly.
            ImmutableArray<IToken> tokenArray =
                replacementTokens.ToImmutableArray();

            // Call the event with the property event arguments.
            var args = new LineIndexTokenIndexTokensReplacedEventArgs(
                lineIndex, tokenIndex, count, tokenArray, replacementType);

            listener(this, args);
        }

        /// <summary>
        /// Performs an action. Once completed, performs the required update actions
        /// to normalize the state of the buffer.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        private void DoPostUpdateAction(Action<IBuffer> action)
        {
            // Perform the action.
            action(this);

            // We need to retokenize the lines after we change them.
            this.UpdateChangedLines();
        }

        /// <summary>
        /// Called when tokens are inserted into line.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="TokenIndexTokensReplacedEventArgs"/> instance containing the event data.
        /// </param>
        private void OnLineTokensReplaced(
            object sender, TokenIndexTokensReplacedEventArgs e)
        {
            // Establish our contracts.
            Contract.Requires(sender is Line);

            // Get our line and its index.
            var line = (Line)sender;
            LineIndex lineIndex = this.GetLineIndex(line);

            // We need to keep track of the lines that have been updated so we can retokenize
            // them later.
            this.changedLines.Add(line);

            // Pass the event to our listeners.
            this.RaiseTokensReplaced(
                lineIndex, 
                e.TokenIndex, 
                e.Count, 
                e.TokensInserted, 
                e.ReplacementType);
        }

        /// <summary>
        /// Raises the lines deleted event.
        /// </summary>
        /// <param name="lineIndex">
        /// Index of the line.
        /// </param>
        /// <param name="count">
        /// The count.
        /// </param>
        private void RaiseLinesDeleted(LineIndex lineIndex, int count)
        {
            // Make sure we have listeners for this event.
            EventHandler<LineIndexLinesDeletedEventArgs> listeners =
                this.LinesDeleted;

            if (listeners == null)
            {
                return;
            }

            // Construct the event arguments and raise the event.
            var args = new LineIndexLinesDeletedEventArgs(lineIndex, count);

            listeners(this, args);
        }

        /// <summary>
        /// Updates the tokens within the buffer for changed lines, including retokenizing
        /// the lines to reflect the changes.
        /// </summary>
        private void UpdateChangedLines()
        {
            // Go through each line and process them in turn.
            foreach (Line line in this.changedLines)
            {
                // See if we can find the line.
                int lineIndex = this.lines.IndexOf(line);

                if (lineIndex < 0)
                {
                    // The line doesn't exist, so skip it.
                    continue;
                }

                // Retokenize the line.
                string lineText = line.Tokens.GetVisibleText();
                IEnumerable<string> newTokens = this.tokenizer.Tokenize(
                    lineText);
            }
        }

        #endregion
    }
}