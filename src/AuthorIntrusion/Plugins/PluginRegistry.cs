// <copyright file="PluginRegistry.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.Plugins
{
    using AuthorIntrusion.IO;

    using StructureMap.Configuration.DSL;

    /// <summary>
    /// Defines the StructureMap plugin registry for the primary Author Intrusion DLL.
    /// </summary>
    public class PluginRegistry : Registry
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginRegistry"/> class.
        /// </summary>
        public PluginRegistry()
        {
            // Set up the persistence from this assembly.
            this.For<PersistenceFactoryManager>().Singleton();
            this.For<IPersistenceFactory>()
                .Add<FilePersistenceFactory>()
                .Singleton();
        }

        #endregion
    }
}