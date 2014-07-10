using System.Threading;

namespace MfGames.TextTokens
{
	public class KeyGenerator : IKeyGenerator
	{
		private int nextId;

		static KeyGenerator()
		{
			Instance = new KeyGenerator();
		}

		public KeyGenerator()
		{
			nextId = 1;
		}

		public static IKeyGenerator Instance { get; set; }

		#region IKeyGenerator Members

		public TokenKey GetNextTokenKey()
		{
			int id = Interlocked.Increment(ref nextId);
			var tokenKey = new TokenKey(id);
			return tokenKey;
		}

		public LineKey GetNextLineKey()
		{
			int id = Interlocked.Increment(ref nextId);
			var lineKey = new LineKey(id);
			return lineKey;
		}

		#endregion
	}
}
