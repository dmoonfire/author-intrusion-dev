// <copyright file="Project.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion
{
    /// <summary>
    /// Primary organizational unit for a writing project. This manages all of
    /// the internals of the project including access to the buffer for editing.
    /// </summary>
    public class Project
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Project"/> class.
        /// </summary>
        public Project()
        {
            this.Singletons = new SingletonManager();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the singleton manager used for managing keys such as
        /// class names and metadata.
        /// </summary>
        public SingletonManager Singletons { get; private set; }

        #endregion
    }
}