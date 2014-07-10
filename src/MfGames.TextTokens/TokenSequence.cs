using System;

namespace MfGames.TextTokens
{
	/// <summary>
	/// A simple, low-overhead identifiers for tokens.
	/// </summary>
	public struct TokenSequence : IEquatable<TokenSequence>
	{
		public int Id;

		public TokenSequence(int id)
		{
			Id = id;
		}

		#region IEquatable<TokenSequence> Members

		public bool Equals(TokenSequence other)
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
			return obj is TokenSequence && Equals((TokenSequence) obj);
		}

		public override int GetHashCode()
		{
			return Id;
		}

		public static bool operator ==(TokenSequence left, TokenSequence right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(TokenSequence left, TokenSequence right)
		{
			return !left.Equals(right);
		}
	}
}
