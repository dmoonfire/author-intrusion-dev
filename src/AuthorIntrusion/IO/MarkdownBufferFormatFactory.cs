// <copyright file="MarkdownBufferFormatFactory.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.IO
{
    using System.IO;

    /// <summary>
    /// Implements the factory used to scan Markdown files and create an
    /// appropriate format.
    /// </summary>
    public class MarkdownBufferFormatFactory : IFileBufferFormatFactory
    {
        #region Public Properties

        /// <summary>
        /// Gets the name of the object.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get
            {
                return "Markdown";
            }
        }

        /// <summary>
        /// Gets the slug for the object.
        /// </summary>
        /// <value>
        /// The slug.
        /// </value>
        public string Slug
        {
            get
            {
                return "markdown";
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Determines whether this instance can handle the specified file.
        /// </summary>
        /// <param name="file">
        /// The file.
        /// </param>
        /// <returns>
        /// True if this is a Markdown file, otherwise false.
        /// </returns>
        public bool CanHandle(FileInfo file)
        {
            // Use the extension to figure out the filename.
            switch (file.Extension.ToLower())
            {
                case ".markdown":
                case ".md":
                    return true;

                default:
                    return false;
            }
        }

        /// <summary>
        /// Creates an instance of a file buffer format and returns it.
        /// </summary>
        /// <returns>
        /// A constructed file buffer format.
        /// </returns>
        public IFileBufferFormat Create()
        {
            return new MarkdownBufferFormat();
        }

        #endregion
    }
}