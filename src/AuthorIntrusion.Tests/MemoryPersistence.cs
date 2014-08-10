// <copyright file="MemoryPersistence.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    using AuthorIntrusion.IO;

    using MfGames.HierarchicalPaths;

    /// <summary>
    /// Implements a persistence class that has all of the data stored in memory.
    /// </summary>
    public class MemoryPersistence : IPersistence
    {
        #region Fields

        /// <summary>
        /// Contains the registered data keyed by the relative file.
        /// </summary>
        private readonly Dictionary<HierarchicalPath, byte[]> data;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryPersistence"/> class.
        /// </summary>
        public MemoryPersistence()
        {
            this.data = new Dictionary<HierarchicalPath, byte[]>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the format of the project file.
        /// </summary>
        public IFileBufferFormat ProjectFormat { get; set; }

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
            return this.GetReadStream(new HierarchicalPath("/"));
        }

        /// <summary>
        /// Gets the write stream for the project file.
        /// </summary>
        /// <returns>
        /// A stream to the project file.
        /// </returns>
        public Stream GetProjectWriteStream()
        {
            throw new NotImplementedException();
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
        /// <remarks>
        /// It is the responsibility of the calling class to close the stream.
        /// </remarks>
        public Stream GetReadStream(HierarchicalPath path)
        {
            byte[] bytes = this.data[path];
            return new MemoryStream(
                bytes, 
                false);
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
        public Stream GetWriteStream(HierarchicalPath path)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the data for a given path.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="lines">
        /// The lines.
        /// </param>
        public void SetData(
            HierarchicalPath path, 
            params string[] lines)
        {
            // Convert the lines to an UTF-8 byte array.
            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(
                stream, 
                Encoding.UTF8))
            {
                // Go through the lines and write them out.
                foreach (string line in lines)
                {
                    writer.WriteLine(line);
                }

                // Set the byte array.
                writer.Flush();
                stream.Flush();
                stream.Position = 0L;

                byte[] buffer = stream.ToArray();

                this.data[path] = buffer;
            }
        }

        /// <summary>
        /// Sets the data for a given path.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="lines">
        /// The lines.
        /// </param>
        public void SetData(
            string path, 
            params string[] lines)
        {
            var newPath = new HierarchicalPath(path);
            this.SetData(
                newPath, 
                lines);
        }

        #endregion
    }
}