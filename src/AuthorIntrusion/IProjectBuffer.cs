// <copyright file="IProjectBuffer.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion
{
    using AuthorIntrusion.Buffers;
    using AuthorIntrusion.IO;
    using AuthorIntrusion.Metadata;

    /// <summary>
    /// Represents a buffer or region within the project.
    /// </summary>
    public interface IProjectBuffer
    {
        #region Public Properties

        /// <summary>
        /// Gets the authors of the project.
        /// </summary>
        /// <value>
        /// The names.
        /// </value>
        NameDictionary Authors { get; }

        /// <summary>
        /// Gets the blocks associated directly with the project buffer.
        /// </summary>
        /// <value>
        /// The blocks.
        /// </value>
        BlockCollection Blocks { get; }

        /// <summary>
        /// Gets the metadata associated with the project.
        /// </summary>
        /// <value>
        /// The metadata.
        /// </value>
        MetadataDictionary Metadata { get; }

        /// <summary>
        /// Gets the titles of the project.
        /// </summary>
        /// <value>
        /// The titles.
        /// </value>
        TitleInfo Titles { get; }

        #endregion
    }
}