using NUnit.Framework;

namespace MfGames.TokenizedText.Tests
{
	[TestFixture]
	public class BufferModelTests
	{
		protected TestBufferModel model;
		protected TestBufferState state;

		protected void Setup()
		{
			model = new TestBufferModel();
			state = new TestBufferState(model);
		}

		public class RaiseLinesInserted : BufferModelTests
		{
			[Test]
			public void InitialState()
			{
				// Arrange
				Setup();

				// Assert
				Assert.AreEqual(0, state.Lines.Count, "Unexpected number of lines.");
			}

			[Test]
			public void AfterTenInsertedLinesWithOneEvent()
			{
				// Arrange
				Setup();

				// Act
				model.InsertLines(0, 10);

				// Assert
				Assert.AreEqual(10, state.Lines.Count, "Unexpected number of lines.");
			}

			[Test]
			public void AfterTenInsertedLinesWithTwoEvents()
			{
				// Arrange
				Setup();

				// Act
				model.InsertLines(0, 5);
				model.InsertLines(0, 5);

				// Assert
				Assert.AreEqual(10, state.Lines.Count, "Unexpected number of lines.");
			}

			[Test]
			public void AfterTenTestLinesInserted()
			{
				// Arrange
				Setup();

				// Act
				model.PopulateNumberGrid(10, 10);

				// Assert
				Assert.AreEqual(10, state.Lines.Count, "Unexpected number of lines.");
			}
		}
	}
}