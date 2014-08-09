// <copyright file="BufferLoadContext.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.IO
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using AuthorIntrusion.Buffers;

    /// <summary>
    /// Contains the context for loading a buffer into a project.
    /// </summary>
    public class BufferLoadContext : ProjectPersistenceContext
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BufferLoadContext"/> class.
        /// </summary>
        /// <param name="project">
        /// The project.
        /// </param>
        /// <param name="persistence">
        /// The persistence.
        /// </param>
        /// <param name="options">
        /// The options.
        /// </param>
        public BufferLoadContext(
            Project project, IPersistence persistence, BufferLoadOptions options)
            : base(project, 
                persistence)
        {
            Contract.Requires(project != null);
            Contract.Requires(persistence != null);

            this.Options = options;
            this.RegionStack = new List<Region> { project };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BufferLoadContext"/> class.
        /// </summary>
        /// <param name="project">
        /// The project.
        /// </param>
        /// <param name="persistence">
        /// The persistence.
        /// </param>
        public BufferLoadContext(Project project, IPersistence persistence)
            : this(project, 
                persistence, 
                BufferLoadOptions.Full)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BufferLoadContext"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public BufferLoadContext(BufferLoadContext context)
            : base(context)
        {
            Contract.Requires(context != null);

            this.Options = context.Options;
            this.RegionStack = context.RegionStack;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the current region being loaded.
        /// </summary>
        /// <value>
        /// The current region.
        /// </value>
        public Region CurrentRegion
        {
            get
            {
                return this.RegionStack[0];
            }
        }

        /// <summary>
        /// Gets the header depth in the current file.
        /// </summary>
        /// <value>
        /// The header depth.
        /// </value>
        public int HeaderDepth { get; private set; }

        /// <summary>
        /// Gets the options for loading the buffer.
        /// </summary>
        /// <value>
        /// The options.
        /// </value>
        public BufferLoadOptions Options { get; private set; }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the processing stack of regions across files.
        /// </summary>
        protected List<Region> RegionStack { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Pops this context for the stack off the list.
        /// </summary>
        public void Pop()
        {
            this.RegionStack.RemoveAt(0);
            this.HeaderDepth--;
        }

        /// <summary>
        /// Pushes the region into the loading stack and returns the result.
        /// </summary>
        /// <param name="newRegion">
        /// The new region.
        /// </param>
        public void Push(Region newRegion)
        {
            this.RegionStack.Insert(
                0, 
                newRegion);
            this.HeaderDepth++;
        }

        #endregion
    }
}