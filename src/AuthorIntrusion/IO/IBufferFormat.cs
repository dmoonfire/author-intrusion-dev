// <copyright file="IBufferFormat.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.IO
{
    /// <summary>
    /// Describes the common interface for working with a buffer format, which may be specific
    /// to a persistence or be general.
    /// </summary>
    public interface IBufferFormat
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
        void LoadProject(
            Project project, 
            IPersistence persistence, 
            BufferFormatLoadOptions options);

        #endregion
    }
}