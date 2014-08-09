// <copyright file="MetadataManager.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.Metadata
{
    using System.Collections.Generic;

    /// <summary>
    /// Management class to entire a singleton-style access to metadata keys.
    /// </summary>
    public class MetadataManager
    {
        #region Fields

        /// <summary>
        /// Contains a list of all known metadata keys.
        /// </summary>
        private readonly Dictionary<string, MetadataKey> keys;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataManager"/> class.
        /// </summary>
        public MetadataManager()
        {
            this.keys = new Dictionary<string, MetadataKey>();
        }

        #endregion

        #region Public Indexers

        /// <summary>
        /// Gets the singleton metadata key, creating it if needed.
        /// </summary>
        /// <value>
        /// The <see cref="MetadataKey"/>.
        /// </value>
        /// <param name="keyName">
        /// Name of the key.
        /// </param>
        /// <returns>
        /// A metadata key.
        /// </returns>
        public MetadataKey this[string keyName]
        {
            get
            {
                MetadataKey key;

                if (!this.keys.TryGetValue(
                    keyName, 
                    out key))
                {
                    key = new MetadataKey(keyName);
                    this.keys[keyName] = key;
                }

                return key;
            }
        }

        #endregion
    }
}