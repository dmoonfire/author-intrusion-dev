// <copyright file="INamed.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion
{
    /// <summary>
    /// Indicates that the given object has a Name property which can be used for filtering
    /// or selection.
    /// </summary>
    public interface INamed
    {
        #region Public Properties

        /// <summary>
        /// Gets the name of the object.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }

        #endregion
    }
}