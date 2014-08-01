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
        #region Fields

        /// <summary>
        /// Contains the format of the project file.
        /// </summary>
        private readonly IFileBufferFormat projectFormat;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FilePersistence"/> class.
        /// </summary>
        /// <param name="projectFile">
        /// The project file.
        /// </param>
        /// <param name="projectFormat">
        /// </param>
        public FilePersistence(
            FileInfo projectFile, IFileBufferFormat projectFormat)
        {
            this.projectFormat = projectFormat;
            this.ProjectFile = projectFile;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the project file, the root file for a given project.
        /// </summary>
        public FileInfo ProjectFile { get; private set; }

        #endregion

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
        public Stream OpenRead(HierarchicalPath path)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}