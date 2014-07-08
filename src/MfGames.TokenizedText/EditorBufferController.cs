namespace MfGames.TokenizedText
{
	/// <summary>
	/// Extends the BufferController to handle editor-specific processing.
	/// </summary>
	/// <remarks>
	/// The methods and fields of this class are intended to be run on the GUI
	/// thread.
	/// </remarks>
	public class EditorBufferController
		: BufferController
	{
		public BufferModel Model { get; private set; }

		void InsertLine() {}

		public EditorBufferController(BufferModel model)
		{
			Model = model;
		}
	}
}