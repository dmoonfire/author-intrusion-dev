// <copyright file="MetadataDictionary.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.Metadata
{
    using System.Collections.Generic;

    /// <summary>
    /// Implements a dictionary for managing metadata keys and values.
    /// </summary>
    public class MetadataDictionary : Dictionary<MetadataKey, MetadataValue>
    {
        #region Public Methods and Operators

        /// <summary>
        /// Retrieves an existing value from the dictionary or creates a new one.
        /// </summary>
        /// <param name="key">
        /// The key to retrieve.
        /// </param>
        /// <returns>
        /// The value, either a new one or an old one.
        /// </returns>
        public MetadataValue GetOrCreate(MetadataKey key)
        {
            MetadataValue value;

            if (!this.TryGetValue(key, out value))
            {
                value = new MetadataValue();
                this[key] = value;
            }

            return value;
        }

        #endregion
    }
}