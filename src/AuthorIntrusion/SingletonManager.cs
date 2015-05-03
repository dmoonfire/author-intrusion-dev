// <copyright file="SingletonManager.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using AuthorIntrusion.Css;
    using AuthorIntrusion.Metadata;

    /// <summary>
    /// A basic implementation of a singleton manager which 
    /// </summary>
    public class SingletonManager
    {
        #region Fields

        /// <summary>
        /// Contains the singleton instances of CSS class names for the project.
        /// </summary>
        private readonly Dictionary<string, CssClassKey> cssClassKeys;

        /// <summary>
        /// Contains the singleton instances of metadata keys for the project.
        /// </summary>
        private readonly Dictionary<string, MetadataKey> metadataKeys;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SingletonManager"/> class.
        /// </summary>
        public SingletonManager()
        {
            this.cssClassKeys = new Dictionary<string, CssClassKey>();
            this.metadataKeys = new Dictionary<string, MetadataKey>();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Gets a singleton class identifier.
        /// </summary>
        /// <param name="className">
        /// Name of the class.
        /// </param>
        /// <returns>
        /// A <c>CssClassKey</c> that represents that class.
        /// </returns>
        public CssClassKey GetCssClassKey(string className)
        {
            // Establish our contracts.
            Contract.Requires(className != null);
            Contract.Requires(className.Length > 0);

            // All the class names are singletones.
            className = string.Intern(className);

            // Look for the key.
            CssClassKey results;

            if (this.cssClassKeys.TryGetValue(
                className, 
                out results))
            {
                return results;
            }

            // We haven't registered this key yet, so register and return it.
            results = new CssClassKey(className);

            this.cssClassKeys[className] = results;

            return results;
        }

        /// <summary>
        /// Gets a singleton metadata key for a given name.
        /// </summary>
        /// <param name="keyName">
        /// The name of the metadata key to retrieve.
        /// </param>
        /// <returns>
        /// A singleton MetadataKey that represents the given name.
        /// </returns>
        public MetadataKey GetMetadataKey(string keyName)
        {
            // Establish our contracts.
            Contract.Requires(keyName != null);
            Contract.Requires(keyName.Length > 0);

            // All the class names are singletones.
            keyName = string.Intern(keyName);

            // Look for the key.
            MetadataKey results;

            if (this.metadataKeys.TryGetValue(
                keyName, 
                out results))
            {
                return results;
            }

            // We haven't registered this key yet, so register and return it.
            results = new MetadataKey(keyName);

            this.metadataKeys[keyName] = results;

            return results;
        }

        #endregion
    }
}