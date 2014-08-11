// <copyright file="RegionProcessingContext.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.IO
{
    using System.Collections.Generic;

    using AuthorIntrusion.Buffers;

    /// <summary>
    /// Encapsulates the logic for a context that deals with processing
    /// regions from the top-down.
    /// </summary>
    public class RegionProcessingContext : ProjectPersistenceContext
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RegionProcessingContext"/> class.
        /// </summary>
        /// <param name="project">
        /// The project.
        /// </param>
        /// <param name="persistence">
        /// The persistence.
        /// </param>
        public RegionProcessingContext(
            Project project, 
            IPersistence persistence)
            : base(project, 
                persistence)
        {
            this.RegionStack = new List<Region>
                {
                    project
                };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegionProcessingContext"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public RegionProcessingContext(RegionProcessingContext context)
            : base(context)
        {
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