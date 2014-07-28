// <copyright file="IPersistence.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.IO
{
    using System.IO;

    using MfGames.HierarchicalPaths;

    /// <summary>
    /// Defines the signature for a class that manages persistence. This is effectively
    /// an abstract layer over the file system but can also represent the interface to
    /// a server or pulling contents from a zip archive.
    /// </summary>
    public interface IPersistence
    {
        #region Public Methods and Operators

        /// <summary>
        /// Retrieves a read stream for a given path. The calling method is responsible for
        /// disposing of the stream.
        /// </summary>
        /// <param name="path">
        /// The absolute path into the project root.
        /// </param>
        /// <returns>
        /// A read stream to the path.
        /// </returns>
        Stream OpenRead(HierarchicalPath path);

        #endregion
    }
}