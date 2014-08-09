// <copyright file="BlockCollection.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.Buffers
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a sequence of blocks.
    /// </summary>
    public class BlockCollection : List<Block>
    {
        #region Public Properties

        /// <summary>
        /// Contains the number of link blocks in the region.
        /// </summary>
        public int LinkCount
        {
            get
            {
                return this.Count(b => b.BlockType == BlockType.Region);
            }
        }

        #endregion
    }
}