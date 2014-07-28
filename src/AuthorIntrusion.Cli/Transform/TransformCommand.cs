// <copyright file="TransformCommand.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.Cli.Transform
{
    using System;

    using AuthorIntrusion.IO;

    /// <summary>
    /// Implements the transform command for the command line.
    /// </summary>
    public class TransformCommand
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TransformCommand"/> class.
        /// </summary>
        /// <param name="persistenceFactoryManager">
        /// The persistence factory manager.
        /// </param>
        public TransformCommand(
            PersistenceFactoryManager persistenceFactoryManager)
        {
            this.PersistenceFactoryManager = persistenceFactoryManager;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the persistence factory manager.
        /// </summary>
        /// <value>
        /// The persistence factory manager.
        /// </value>
        public PersistenceFactoryManager PersistenceFactoryManager { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="options">
        /// The options.
        /// </param>
        public void Run(TransformOptions options)
        {
            Console.WriteLine("Transform from the inside 2: " + options);
            Uri uri = options.InputUri;
            Console.WriteLine(uri);

            // Load the project into memory.
            // IPersistenceFactory factory =
            // this.PersistenceFactoryManager.GetFactory(options.InputUri);
        }

        #endregion
    }
}