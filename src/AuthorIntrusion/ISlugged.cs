// <copyright file="ISlugged.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion
{
    /// <summary>
    /// Indicates that the object has a slug, an identifier that followed WordPress' naming
    /// conventions (all lowercase, dashes instead of spaces).
    /// </summary>
    public interface ISlugged
    {
        #region Public Properties

        /// <summary>
        /// Gets the slug for the object.
        /// </summary>
        /// <value>
        /// The slug.
        /// </value>
        string Slug { get; }

        #endregion
    }
}