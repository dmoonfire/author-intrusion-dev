// <copyright file="DocBookBufferFormat.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.IO
{
    using System;

    /// <summary>
    /// Encapsulates the functionality for a buffer format that handles a DocBook 5
    /// file.
    /// </summary>
    public class DocBookBufferFormat : IFileBufferFormat
    {
        #region Public Methods and Operators

        /// <summary>
        /// Loads project data from the persistence layer and populates the project.
        /// </summary>
        /// <param name="project">
        /// The project.
        /// </param>
        /// <param name="persistence">
        /// The persistence.
        /// </param>
        /// <param name="options">
        /// The options.
        /// </param>
        /// <exception cref="System.InvalidOperationException">
        /// Cannot read DocBook 5 files.
        /// </exception>
        public void LoadProject(
            Project project, 
            IPersistence persistence, 
            BufferFormatLoadOptions options)
        {
            throw new InvalidOperationException("Cannot read DocBook 5 files.");
        }

        /// <summary>
        /// Writes out the project to the given persistence using the
        /// format instance.
        /// </summary>
        /// <param name="project">
        /// The project to write out.
        /// </param>
        /// <param name="persistence">
        /// The persistence layer to use.
        /// </param>
        public void StoreProject(Project project, IPersistence persistence)
        {
        }

        #endregion
    }
}