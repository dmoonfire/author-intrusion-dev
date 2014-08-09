// <copyright file="BufferStoreContext.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.IO
{
    /// <summary>
    /// </summary>
    public class BufferStoreContext : ProjectPersistenceContext
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BufferStoreContext"/> class.
        /// </summary>
        /// <param name="project">
        /// The project.
        /// </param>
        /// <param name="persistence">
        /// The persistence.
        /// </param>
        public BufferStoreContext(Project project, IPersistence persistence)
            : base(project, 
                persistence)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BufferStoreContext"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public BufferStoreContext(ProjectPersistenceContext context)
            : base(context)
        {
        }

        #endregion
    }
}