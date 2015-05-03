// <copyright file="BufferLoadOptions.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.IO
{
    using System;

    /// <summary>
    /// Describes the various controls and options for loading data from a buffer.
    /// </summary>
    [Flags]
    public enum BufferLoadOptions : byte
    {
        /// <summary>
        /// Indicates no options for loading, which will result in no data.
        /// </summary>
        None = 0, 

        /// <summary>
        /// Load only information about the buffer, including header information
        /// and rough line data such as length of visible text.
        /// </summary>
        Metadata = 1, 

        /// <summary>
        /// Load the full line information from the buffer.
        /// </summary>
        Content = 2, 

        /// <summary>
        /// Indicates that the project has already been loaded and the data needs to
        /// be refreshed.
        /// </summary>
        Reload = 4, 

        /// <summary>
        /// Load both the metadata and content.
        /// </summary>
        Full = Metadata | Content, 
    }
}