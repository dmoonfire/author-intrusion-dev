// <copyright file="FilePersistenceFactory.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.IO
{
    using System;
    using System.IO;

    /// <summary>
    /// Defines the persistence factory for the "file" protocol which interacts
    /// with the local file system.
    /// </summary>
    public class FilePersistenceFactory : IPersistenceFactory
    {
        #region Public Properties

        /// <summary>
        /// Gets the name of the URI protocol that this factory handles, without the
        /// trailing "://". For example, "file" or "ai" instead of "file://" or "ai://".
        /// </summary>
        public string Scheme
        {
            get
            {
                return "file";
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Constructs an IPersistence object based on the given path. This will be the
        /// component of the path after the "://". For example, this method will get "a/b.xml"
        /// from "file://a/b.xml".
        /// </summary>
        /// <param name="uri">
        /// </param>
        /// <returns>
        /// An IPersistence object representing the path.
        /// </returns>
        /// <exception cref="System.NotImplementedException">
        /// </exception>
        public IPersistence CreatePersistence(Uri uri)
        {
            // For the filesystem, we need to keep track of the "project" or root file
            // so we can write it out again. If the URI represents a directory, we have
            // to identify the project file.
            string path = uri.LocalPath;

            if (Directory.Exists(path))
            {
                throw new ArgumentException(
                    "The URI must point to a filename, not a directory.", "uri");
            }

            // Save the project file.
            var projectFile = new FileInfo(path);

            if (!projectFile.Exists)
            {
                throw new FileNotFoundException(
                    "Cannot file project file: " + path + ".");
            }

            // The file exists, so create the file persistence for it.
            var persistence = new FilePersistence(projectFile);
            return persistence;
        }

        #endregion
    }
}