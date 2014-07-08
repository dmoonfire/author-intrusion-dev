using MfGames.TokenizedText;

using NUnit.Framework;

namespace MfGames.TokenizedText.Tests
{
	[TestFixture]
	public class LineKeyTests
	{
		#region Nested Types

		public class Constructor
		{
			#region Methods

			[Test]
			public void DefaultValues()
			{
				// Act
				LineKey lineKey = new LineKey();

				// Assert
				Assert.AreEqual(0, lineKey.Id, "Id is not expected.");
			}

			[Test]
			public void ProvidedValue()
			{
				// Act
				LineKey lineKey = new LineKey(23);

				// Assert
				Assert.AreEqual(23, lineKey.Id, "Id is not expected.");
			}

			#endregion Methods
		}

		#endregion Nested Types
	}
}