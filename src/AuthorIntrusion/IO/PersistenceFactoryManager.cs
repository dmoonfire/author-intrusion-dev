// <copyright file="PersistenceFactoryManager.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.IO
{
    using System.Diagnostics;

    /// <summary>
    /// Implements the singleton instance for managing all persistence frameworks.
    /// </summary>
    public class PersistenceFactoryManager
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceFactoryManager"/> class.
        /// </summary>
        /// <param name="factories">
        /// The factories.
        /// </param>
        public PersistenceFactoryManager(IPersistenceFactory[] factories)
        {
            Trace.WriteLine(factories);
        }

        #endregion
    }
}