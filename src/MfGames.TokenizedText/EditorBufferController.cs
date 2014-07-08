namespace MfGames.TokenizedText
{
	/// <summary>
	/// Extends the BufferController to handle editor-specific processing.
	/// </summary>
	/// <remarks>
	/// The methods and fields of this class are intended to be run on the GUI
	/// thread.
	/// </remarks>
	public class EditorBufferController : BufferController
	{
		#region Constructors

		public EditorBufferController(BufferModel model)
		{
			Model = model;
		}

		#endregion Constructors

		#region Properties

		public BufferModel Model
		{
			get; private set;
		}

		#endregion Properties

		#region Methods

		void InsertLine()
		{}

		#endregion Methods
	}
}