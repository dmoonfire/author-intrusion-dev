namespace MfGames.TokenizedText
{
	/// <summary>
	/// An abstract model that represents a buffer, a collection of lines, and
	/// the associated internal processes that manage those lines. A buffer
	/// can represent a single text file, a sequence of files, or a pure memory
	/// implementation of lines.
	///
	/// Buffers are changed via BufferCommand objects and emit BufferChange
	/// objects (via events) to reflect those changes. Typically, a view does
	/// not change it's visible state until it receives the changes via the
	/// BufferChanges.
	/// </summary>
	public abstract class BufferModel
	{

	}
}