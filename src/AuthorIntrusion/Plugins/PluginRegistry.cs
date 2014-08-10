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
        /// <param name="additionalRegistries">
        /// </param>
        public PluginRegistry(params Registry[] additionalRegistries)
        {
            // Set up the persistence from this assembly.
            this.For<PersistenceFactoryManager>()
                .Singleton();
            this.For<IPersistenceFactory>()
                .Add<FilePersistenceFactory>()
                .Singleton();
            this.For<IFileBufferFormatFactory>()
                .Add<MarkdownBufferFormatFactory>();
            this.For<IFileBufferFormatFactory>()
                .Add<DocBookBufferFormatFactory>();

            // Add in the additional registries.
            foreach (Registry registry in additionalRegistries)
            {
                this.IncludeRegistry(registry);
            }
        }

        #endregion
    }
}