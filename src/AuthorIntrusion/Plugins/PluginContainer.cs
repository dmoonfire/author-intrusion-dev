// <copyright file="PluginContainer.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.Plugins
{
    using StructureMap;
    using StructureMap.Configuration.DSL;

    /// <summary>
    /// Implements the primary container for the IoC implementation.
    /// </summary>
    public class PluginContainer : Container
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginContainer"/> class.
        /// </summary>
        /// <param name="additionalRegistries">
        /// The additional registries.
        /// </param>
        public PluginContainer(params Registry[] additionalRegistries)
            : base(new PluginRegistry(additionalRegistries))
        {
        }

        #endregion
    }
}