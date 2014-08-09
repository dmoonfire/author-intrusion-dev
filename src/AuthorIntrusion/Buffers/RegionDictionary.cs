// <copyright file="RegionDictionary.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.Buffers
{
    using System.Collections.Generic;
    using System.Linq;

    using MfGames.Text;

    /// <summary>
    /// Encapslates the functionality for managing a collection of regions. The
    /// actual sequencing of the regions is done by the Region/Block pair.
    /// </summary>
    public class RegionDictionary : Dictionary<string, Region>
    {
        #region Public Methods and Operators

        /// <summary>
        /// Adds the specified region into the dictionary.
        /// </summary>
        /// <param name="region">
        /// The region.
        /// </param>
        public void Add(Region region)
        {
            this[region.Slug] = region;
        }

        /// <summary>
        /// Tries to retrieve the region via the name.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="region">
        /// The region.
        /// </param>
        /// <returns>
        /// True if the item is found, false if not.
        /// </returns>
        public bool TryGetName(string name, out Region region)
        {
            region = this.Values.ToList().FirstOrDefault(r => r.Name == name);

            return region != null;
        }

        /// <summary>
        /// Tries to get the region by slug or name.
        /// </summary>
        /// <param name="slug">
        /// The slug.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="region">
        /// The region.
        /// </param>
        /// <returns>
        /// True if the region was found, otherwise false.
        /// </returns>
        public bool TryGetSlugOrName(
            string slug, string name, out Region region)
        {
            // Start by trying to get it via slug.
            if (!string.IsNullOrWhiteSpace(slug))
            {
                bool foundSlug = this.TryGetValue(
                    slug, 
                    out region);

                if (foundSlug)
                {
                    return true;
                }
            }

            // Try to get it via name.
            if (!string.IsNullOrWhiteSpace(name))
            {
                bool foundName = this.TryGetName(
                    name, 
                    out region);

                if (foundName)
                {
                    return true;
                }
            }

            // Otherwise, we didn't find it.
            region = null;
            return false;
        }

        #endregion

        /// <summary>
        /// Creates a region based on the specified layout.
        /// </summary>
        /// <param name="layout">The layout.</param>
        /// <returns>The created region.</returns>
        public Region Create(RegionLayout layout)
        {
            // Figure out the indexes for the region.
            int containerIndex = 1;
            int projectIndex = 1;

            // Format the slugs and names.
            var macros = new MacroExpansion();
            var variables =
                new Dictionary<string, object>
                    {
                        { "ContainerIndex", containerIndex },
                        { "ProjectIndex", projectIndex }
                    };

            // Create a new region.
            var region = new Region
                {
                    Slug = macros.Expand(
                        layout.Slug,
                        variables),
                    Name = macros.Expand(
                        layout.Name,
                        variables),
                };

            // Add the region into the collection.
            this[region.Slug] = region;

            // Return the constructed region.
            return region;
        }
    }
}