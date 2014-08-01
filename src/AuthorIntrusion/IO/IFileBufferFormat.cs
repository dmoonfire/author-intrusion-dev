// <copyright file="IFileBufferFormat.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.IO
{
    using System.IO;

    /// <summary>
    /// Indicates an IBufferFormat that can read and write files from a file system.
    /// </summary>
    public interface IFileBufferFormat : IBufferFormat
    {
        #region Public Methods and Operators

        /// <summary>
        /// Determines whether this instance can handle the specified file.
        /// </summary>
        /// <param name="file">
        /// The file.
        /// </param>
        /// <returns>
        /// True if the format can handle it, otherwise false.
        /// </returns>
        bool CanHandle(FileInfo file);

        #endregion
    }
}