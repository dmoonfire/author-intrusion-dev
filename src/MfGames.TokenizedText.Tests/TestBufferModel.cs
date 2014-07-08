using Humanizer;

using MfGames.TokenizedText;

namespace MfGames.TokenizedText.Tests
{
	/// <summary>
	/// Implements a buffer model that has some convienance methods for creating
	/// unit tests.
	/// </summary>
	public class TestBufferModel : MemoryBufferModel
	{
		#region Methods

		public void PopulateNumberGrid(int rows, int columns)
		{
			// We add numerical words (zero, one, two, three, etc).
			int word = 0;

			for (int row = 0; row < rows; row++)
			{
				// Create the lines one at time.
				InsertLine(row);

				// Add one number for every column.
				for (int column = 0; column < columns; column++)
				{
					// If we aren't adding the first character of the line, create a
					// new token for the space character.
					if (column > 0)
					{
						var spaceToken = new Token(" ");
						AddToken(row, spaceToken);
					}

					// Now, add the word (as a human-readable string) to the end of
					// the line.
					var wordToken = new Token(word++.ToWords());
					AddToken(row, wordToken);
				}
			}
		}

		#endregion Methods
	}
}