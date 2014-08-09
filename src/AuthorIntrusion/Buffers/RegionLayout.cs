// <copyright file="RegionLayout.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.Buffers
{
    using System.Collections.Generic;

    using AuthorIntrusion.IO;

    /// <summary>
    /// Represents the layout of the project, in a nested structure that identifies
    /// every logical region within the project.
    /// </summary>
    public class RegionLayout : INamedSlugged
    {
        #region Constructors and Destructors

        /// <summary>
        /// </summary>
        public RegionLayout()
        {
            this.InnerLayouts = new List<RegionLayout>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the buffer format for all of the dynamic regions within the
        /// container.
        /// </summary>
        /// <value>
        /// The dynamic buffer format.
        /// </value>
        public IBufferFormat DynamicBufferFormat { get; set; }

        /// <summary>
        /// Gets or sets the path format for the dynamic regions. When resolved, it will
        /// determine the persistence name (the physical filename) for each of the dynamic
        /// regions. This uses the standard formatting for dynamic regions, with the
        /// "$(ProjectIndex)" or "$(ContainerIndex)" variables substitution.
        /// </summary>
        /// <value>
        /// The dynamic path format.
        /// </value>
        public string DynamicPathFormat { get; set; }

        /// <summary>
        /// Gets or sets the slug format for any dynamic regions. This uses the standard
        /// formatting for dynamic regions, with the "$(ProjectIndex)" or "$(ContainerIndex)"
        /// variables substitution.
        /// </summary>
        /// <value>
        /// The dynamic slug format.
        /// </value>
        public string DynamicSlugFormat { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has line content
        /// in addition to potentially child regions. As a rule, child regions are
        /// not floating. They will always be below the content.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has content; otherwise, <c>false</c>.
        /// </value>
        public bool HasContent { get; set; }

        /// <summary>
        /// Gets the ordered sequence of layouts within the container. This represents
        /// child regions but not dynamic ones.
        /// </summary>
        /// <value>
        /// The inner layouts.
        /// </value>
        public IList<RegionLayout> InnerLayouts { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is a dynamic container
        /// that can have zero or more child regions. If this is true, then InnerRegions
        /// must be empty.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is dynamic container; otherwise, <c>false</c>.
        /// </value>
        public bool IsDynamicContainer { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this region is an external one
        /// and stored outside of the containing region.
        /// </summary>
        public bool IsExternal { get; set; }

        /// <summary>
        /// Gets or sets the name of the object.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the slug for the object.
        /// </summary>
        /// <value>
        /// The slug.
        /// </value>
        public string Slug { get; set; }

        #endregion
    }
}