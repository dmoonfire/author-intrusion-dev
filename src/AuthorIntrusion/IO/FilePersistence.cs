// <copyright file="FilePersistence.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.IO
{
    using System;
    using System.IO;

    using MfGames.HierarchicalPaths;

    /// <summary>
    /// Implements a file-system-based persistence layer centered around a single project
    /// file.
    /// </summary>
    public class FilePersistence : IPersistence
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FilePersistence"/> class.
        /// </summary>
        /// <param name="projectFile">
        /// The project file.
        /// </param>
        /// <param name="projectFormat">
        /// The project format.
        /// </param>
        public FilePersistence(
            FileInfo projectFile, 
            IFileBufferFormat projectFormat)
        {
            this.ProjectFormat = projectFormat;
            this.ProjectFile = projectFile;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the project file, the root file for a given project.
        /// </summary>
        public FileInfo ProjectFile { get; private set; }

        /// <summary>
        /// Gets the format of the project file.
        /// </summary>
        public IFileBufferFormat ProjectFormat { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Gets a read stream for the project file.
        /// </summary>
        /// <returns>
        /// A stream to the project file.
        /// </returns>
        /// <remarks>
        /// It is the responsibility of the calling class to close the stream.
        /// </remarks>
        public Stream GetProjectReadStream()
        {
            return this.ProjectFile.OpenRead();
        }

        /// <summary>
        /// Gets the write stream for the project file.
        /// </summary>
        /// <returns>
        /// A stream to the project file.
        /// </returns>
        public Stream GetProjectWriteStream()
        {
            return this.ProjectFile.Open(
                FileMode.Create, 
                FileAccess.Write);
        }

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
        /// <exception cref="System.NotImplementedException">
        /// </exception>
        /// <remarks>
        /// It is the responsibility of the calling class to close the stream.
        /// </remarks>
        public Stream GetReadStream(HierarchicalPath path)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the write stream for a given path relative to the project. The calling
        /// method is responsible for disposing of the stream.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// A stream to the persistence object.
        /// </returns>
        /// <exception cref="System.NotImplementedException">
        /// </exception>
        public Stream GetWriteStream(HierarchicalPath path)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}