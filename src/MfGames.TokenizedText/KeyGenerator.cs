using System.Threading;

namespace MfGames.TokenizedText
{
	/// <summary>
	/// Generates unique keys for internal processing of tokens and
	/// lines.
	/// </summary>
	public class KeyGenerator
	{
		private int id;

		public static KeyGenerator Instance { get; private set; }

		public TokenKey GetNextTokenKey()
		{
			int keyId = Interlocked.Increment(ref id);
			TokenKey key = new TokenKey(keyId);
			return key;
		}

		public LineKey GetNextLineKey()
		{
			int keyId = Interlocked.Increment(ref id);
			LineKey key = new LineKey(keyId);
			return key;
		}

		static KeyGenerator()
		{
			Instance = new KeyGenerator();
		}
	}
}
