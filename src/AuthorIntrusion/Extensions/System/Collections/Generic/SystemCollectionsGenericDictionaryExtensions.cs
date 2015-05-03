// <copyright file="SystemCollectionsGenericDictionaryExtensions.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.Extensions.System.Collections.Generic
{
    using global::System.Collections.Generic;

    /// <summary>
    /// Extension methods for dictionary classes.
    /// </summary>
    public static class SystemCollectionsGenericDictionaryExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// Adds a key if it is non-empty.
        /// </summary>
        /// <param name="dictionary">
        /// The dictionary.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public static void AddIfNotEmpty(
            this IDictionary<string, object> dictionary, 
            string key, 
            string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                dictionary[key] = value;
            }
        }

        #endregion
    }
}