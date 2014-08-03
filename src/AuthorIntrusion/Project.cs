// <copyright file="Project.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion
{
    using AuthorIntrusion.Buffers;
    using AuthorIntrusion.Metadata;

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
            // Create the collections.
            this.Singletons = new SingletonManager();
            this.MetadataManager = new MetadataManager();
            this.Metadata = new MetadataDictionary();
            this.Blocks = new BlockCollection();

            // Hardcode the project layout to be a story.
            this.Layout = new RegionLayout
                {
                    Name = "Project", 
                    Slug = "project", 
                    HasContent = true, 
                    Minimum = 1, 
                    Maximum = 1, 
                    IsDynamicContainer = false, 
                };
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the blocks associated directly with the project buffer.
        /// </summary>
        /// <value>
        /// The blocks.
        /// </value>
        public BlockCollection Blocks { get; private set; }

        /// <summary>
        /// Gets or sets the layout for the project.
        /// </summary>
        /// <value>
        /// The layout.
        /// </value>
        public RegionLayout Layout { get; set; }

        /// <summary>
        /// Gets the metadata associated with the project.
        /// </summary>
        /// <value>
        /// The metadata.
        /// </value>
        public MetadataDictionary Metadata { get; private set; }

        /// <summary>
        /// Gets the metadata manager for the project.
        /// </summary>
        /// <value>
        /// The metadata manager.
        /// </value>
        public MetadataManager MetadataManager { get; private set; }

        /// <summary>
        /// Gets the singleton manager used for managing keys such as
        /// class names and metadata.
        /// </summary>
        public SingletonManager Singletons { get; private set; }

        #endregion
    }
}