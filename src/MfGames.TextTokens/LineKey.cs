using System;

namespace MfGames.TextTokens
{
	/// <summary>
	/// A simple, low-overhead identifiers for tokens.
	/// </summary>
	public struct LineKey : IEquatable<LineKey>
	{
		public int Id;

		public LineKey(int id)
		{
			Id = id;
		}

		#region IEquatable<LineKey> Members

		public bool Equals(LineKey other)
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
			return obj is LineKey && Equals((LineKey) obj);
		}

		public override int GetHashCode()
		{
			return Id;
		}

		public static bool operator ==(LineKey left, LineKey right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(LineKey left, LineKey right)
		{
			return !left.Equals(right);
		}
	}
}
