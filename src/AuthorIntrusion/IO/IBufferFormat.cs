// <copyright file="IBufferFormat.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.IO
{
    using AuthorIntrusion.Cli.Transform;

    /// <summary>
    /// Describes the common interface for working with a buffer format, which may be specific
    /// to a persistence or be general.
    /// </summary>
    public interface IBufferFormat
    {
        #region Public Properties

        /// <summary>
        /// Gets the current settings associated with the format.
        /// </summary>
        IBufferFormatSettings Settings { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Loads a profile of a specific format into memory. Profiles are either
        /// internally identified by the format and may be stored as part of
        /// a project's settings.
        /// </summary>
        /// <param name="profileName">
        /// The name of the profile to load.
        /// </param>
        void LoadProfile(string profileName);

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
        void StoreProject(Project project, IPersistence persistence);

        #endregion
    }
}