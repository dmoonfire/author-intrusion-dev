namespace MfGames.TextTokens.Tests
{
	/// <summary>
	/// A testing class that listens to events of the Buffer like a text editor and
	/// keeps track of the current state. This is used to reflect what the user will
	/// see in the editor.
	/// </summary>
	public class TestBufferState
	{
		/// <summary>
		/// Called when the buffer splits a token into two tokens.
		/// </summary>
		private void OnTokenSplit(object sender, TokenSplitEventArgs e)
		{
		}

		private void OnTokenReplaced(object sender, TokenReplacedEventArgs e)
		{
		}
	}
}
