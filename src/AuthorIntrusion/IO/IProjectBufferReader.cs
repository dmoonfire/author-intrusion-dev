// <copyright file="IProjectBufferReader.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.IO
{
    using System.IO;

    using AuthorIntrusion.Buffers;

    /// <summary>
    /// Defines the signature of a class capable of reading data into a project buffer.
    /// This class is used for both loading summary data (metadata plus line information)
    /// and full line data.
    /// </summary>
    public interface IProjectBufferReader
    {
        #region Public Methods and Operators

        /// <summary>
        /// Performs a read on the given input stream using the settings given. The results
        /// are populated into the provided ProjectBuffer.
        /// </summary>
        /// <param name="buffer">
        /// The buffer to populate.
        /// </param>
        /// <param name="stream">
        /// The stream to read from.
        /// </param>
        /// <param name="options">
        /// The options for reading the stream.
        /// </param>
        void Read(
            ProjectBuffer buffer, 
            Stream stream, 
            ProjectBufferReaderSettings options);

        #endregion
    }
}