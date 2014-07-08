using System.Threading;

namespace MfGames.TokenizedText
{
	/// <summary>
	/// Generates unique keys for internal processing of tokens and
	/// lines.
	/// </summary>
	public class KeyGenerator
	{
		#region Fields

		private int id;

		#endregion Fields

		#region Constructors

		static KeyGenerator()
		{
			Instance = new KeyGenerator();
		}

		#endregion Constructors

		#region Properties

		public static KeyGenerator Instance
		{
			get; private set;
		}

		#endregion Properties

		#region Methods

		public LineKey GetNextLineKey()
		{
			int keyId = Interlocked.Increment(ref id);
			LineKey key = new LineKey(keyId);
			return key;
		}

		public TokenKey GetNextTokenKey()
		{
			int keyId = Interlocked.Increment(ref id);
			TokenKey key = new TokenKey(keyId);
			return key;
		}

		#endregion Methods
	}
}