// <copyright file="RegionDictionary.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.Buffers
{
    using System.Collections.Generic;
    using System.Linq;

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
            region = this.Values.ToList().First(r => r.Name == name);

            return region != null;
        }

        #endregion
    }
}