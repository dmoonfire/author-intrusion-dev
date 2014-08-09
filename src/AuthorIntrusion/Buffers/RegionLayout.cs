// <copyright file="RegionLayout.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.Buffers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using AuthorIntrusion.IO;

    using MfGames.Text;

    /// <summary>
    /// Represents the layout of the project, in a nested structure that identifies
    /// every logical region within the project.
    /// </summary>
    public class RegionLayout : INamedSlugged
    {
        #region Fields

        /// <summary>
        /// Contains the slug for this layout.
        /// </summary>
        private string slug;

        /// <summary>
        /// Contains the cached slug regex used to parse slugs to determine sequencing.
        /// </summary>
        private Regex slugRegex;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RegionLayout"/> class.
        /// </summary>
        public RegionLayout()
        {
            this.InnerLayouts = new List<RegionLayout>();
        }

        #endregion

        #region Public Properties

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
        /// Gets or sets a value indicating whether this region is an external one
        /// and stored outside of the containing region.
        /// </summary>
        public bool IsExternal { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is a dynamic region
        /// layout that will expand into multiple regions.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is dynamic container; otherwise, <c>false</c>.
        /// </value>
        public bool IsSequenced { get; set; }

        /// <summary>
        /// Gets or sets the name of the object.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the buffer format factory used for all the sequenced
        /// regions within the region.
        /// </summary>
        /// <value>
        /// The dynamic buffer format factory.
        /// </value>
        public IBufferFormatFactory SequenceBufferFormatFactory { get; set; }

        /// <summary>
        /// Gets or sets the slug for the object.
        /// </summary>
        /// <value>
        /// The slug.
        /// </value>
        public string Slug
        {
            get
            {
                return this.slug;
            }

            set
            {
                this.slug = value;
                this.slugRegex = null;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Attempts to retrieve a region that matches the given pattern.
        /// </summary>
        /// <param name="testSlug">
        /// The slug to match again.
        /// </param>
        /// <returns>
        /// The found region layout or null.
        /// </returns>
        public RegionLayout GetSequencedRegion(string testSlug)
        {
            RegionLayout layout =
                this.GetSequencedRegions()
                    .FirstOrDefault(l => l.IsSequenceSlug(testSlug));

            return layout;
        }

        /// <summary>
        /// Returns an enumeration of all sequenced containers within the layout.
        /// </summary>
        /// <returns>An enumeration of regions that are sequences.</returns>
        public IEnumerable<RegionLayout> GetSequencedRegions()
        {
            // Create a list of regions with sequences.
            var list = new List<RegionLayout>();

            if (this.IsSequenced)
            {
                list.Add(this);
            }

            // Loop through the inner layouts and see if any of them are a
            // sequence container.
            foreach (RegionLayout innerLayout in this.InnerLayouts)
            {
                IEnumerable<RegionLayout> innerContainers =
                    innerLayout.GetSequencedRegions();

                list.AddRange(innerContainers);
            }

            // Return the resulting list.
            return list;
        }

        /// <summary>
        /// Determines whether the given slug represents a sequenced slug underneath
        /// the given region.
        /// </summary>
        /// <param name="testSlug">
        /// The slug to parse.
        /// </param>
        /// <returns>
        /// True if it is a sequenced slug, otherwise false.
        /// </returns>
        public bool IsSequenceSlug(string testSlug)
        {
            // If we aren't sequenced, we don't bother.
            if (!this.IsSequenced)
            {
                return false;
            }

            // If we don't have the regex, build it.
            if (this.slugRegex == null)
            {
                var macros = new MacroExpansion();

                this.slugRegex = macros.GetRegex(this.Slug);
            }

            // Return if this is a match.
            bool results = this.slugRegex.IsMatch(testSlug);

            return results;
        }

        #endregion
    }
}