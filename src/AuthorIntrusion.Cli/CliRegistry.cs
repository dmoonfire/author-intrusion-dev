// <copyright file="CliRegistry.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.Cli
{
    using AuthorIntrusion.Cli.Transform;

    using StructureMap.Configuration.DSL;

    /// <summary>
    /// Implements the StructureMap registry for this assembly.
    /// </summary>
    public class CliRegistry : Registry
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CliRegistry"/> class.
        /// </summary>
        public CliRegistry()
        {
            this.For<TransformCommand>().Singleton();
        }

        #endregion
    }
}