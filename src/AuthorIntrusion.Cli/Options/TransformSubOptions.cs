// <copyright file="TransformSubOptions.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.Cli.Options
{
    using CommandLine;

    /// <summary>
    /// Defines the options for the "transform" sub-command.
    /// </summary>
    public class TransformSubOptions
    {
        #region Constants

        /// <summary>
        /// Contains the long name that identifies this option
        /// </summary>
        public const string LongName = "transform";

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TransformSubOptions"/> class.
        /// </summary>
        public TransformSubOptions()
        {
            this.InputProtocol = "file";
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the protocol for the input. This defaults to 'file' for file
        /// system operations.
        /// </summary>
        [Option("input-protocol")]
        public string InputProtocol { get; set; }

        #endregion
    }
}