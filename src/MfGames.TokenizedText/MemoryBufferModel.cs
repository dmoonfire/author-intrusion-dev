using System.Collections.Generic;

namespace MfGames.TokenizedText
{
	/// <summary>
	/// Implements a simplistic, in-memory buffer model that represents the
	/// lines in a simple generic list.
	/// </summary>
	public class MemoryBufferModel : BufferModel
	{
		public IList<string> Lines { get; private set; }

		public MemoryBufferModel()
		{
			Lines = new List<string>();
		}
	}
}
