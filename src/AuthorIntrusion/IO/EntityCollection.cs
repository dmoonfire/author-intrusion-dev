// <copyright file="EntityCollection.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.IO
{
    using System.Collections.Generic;

    /// <summary>
    /// A custom sequenced collection that contains zero or more entities.
    /// </summary>
    public class EntityCollection : List<EntityInfo>
    {
        #region Public Properties

        /// <summary>
        /// Gets the first (primary) entry in the entity collection.
        /// </summary>
        public EntityInfo First
        {
            get
            {
                return this.Count == 0 ? null : this[0];
            }
        }

        #endregion
    }
}