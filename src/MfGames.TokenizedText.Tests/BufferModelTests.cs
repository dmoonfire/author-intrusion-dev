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
			public void AfterOneInsetedLine()
			{
				// Arrange
				Setup();
				model.InsertLines(0, 10);

				// Assert
				Assert.AreEqual(10, state.Lines.Count, "Unexpected number of lines.");
			}
		}
	}
}