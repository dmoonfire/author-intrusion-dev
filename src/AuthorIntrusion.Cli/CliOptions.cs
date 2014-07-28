// <copyright file="CliOptions.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.Cli
{
    using AuthorIntrusion.Cli.Transform;

    using CommandLine;

    /// <summary>
    /// Encapsulates the argument options for the CLI tool.
    /// </summary>
    public class CliOptions
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the options for the sub-command form transforming
        /// files from one format to another.
        /// </summary>
        [VerbOption(TransformOptions.LongName, 
            HelpText =
                "Transform an input file or project into a different format.")]
        public TransformOptions TransformOptions { get; set; }

        #endregion
    }
}