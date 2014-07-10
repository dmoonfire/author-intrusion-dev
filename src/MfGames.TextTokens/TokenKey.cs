using System;

namespace MfGames.TextTokens
{
	/// <summary>
	/// A simple, low-overhead identifiers for tokens.
	/// </summary>
	public struct TokenKey : IEquatable<TokenKey>
	{
		public int Id;

		public TokenKey(int id)
		{
			Id = id;
		}

		#region IEquatable<TokenKey> Members

		public bool Equals(TokenKey other)
		{
			return Id == other.Id;
		}

		#endregion

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}
			return obj is TokenKey && Equals((TokenKey) obj);
		}

		public override int GetHashCode()
		{
			return Id;
		}

		public static bool operator ==(TokenKey left, TokenKey right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(TokenKey left, TokenKey right)
		{
			return !left.Equals(right);
		}
	}
}
