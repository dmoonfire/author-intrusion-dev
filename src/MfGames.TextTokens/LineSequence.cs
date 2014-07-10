using System;

namespace MfGames.TextTokens
{
	/// <summary>
	/// A simple, low-overhead sequence for tokens.
	/// </summary>
	public struct LineSequence : IEquatable<LineSequence>
	{
		public int Id;

		public LineSequence(int id)
		{
			Id = id;
		}

		#region IEquatable<LineSequence> Members

		public bool Equals(LineSequence other)
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
			return obj is LineSequence && Equals((LineSequence) obj);
		}

		public override int GetHashCode()
		{
			return Id;
		}

		public static bool operator ==(LineSequence left, LineSequence right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(LineSequence left, LineSequence right)
		{
			return !left.Equals(right);
		}
	}
}
