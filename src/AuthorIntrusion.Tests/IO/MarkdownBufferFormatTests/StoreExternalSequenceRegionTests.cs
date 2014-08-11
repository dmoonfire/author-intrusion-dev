// <copyright file="StoreExternalSequenceRegionTests.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.Tests.IO.MarkdownBufferFormatTests
{
    using System.Collections.Generic;

    using AuthorIntrusion.Buffers;
    using AuthorIntrusion.IO;

    using MfGames.HierarchicalPaths;

    using NUnit.Framework;

    /// <summary>
    /// Tests various aspects of storing an external sequence region in Markdown.
    /// </summary>
    [TestFixture]
    public class StoreExternalSequenceRegionTests : MemoryPersistenceTestsBase
    {
        #region Fields

        /// <summary>
        /// Contains the persistence used to read in the file.
        /// </summary>
        private MemoryPersistence inputPersistence;

        /// <summary>
        /// Contains the context from the load process.
        /// </summary>
        private BufferStoreContext outputContext;

        /// <summary>
        /// Contains the persistence used to write out the results.
        /// </summary>
        private MemoryPersistence outputPersistence;

        /// <summary>
        /// Contains the loaded project for verification purposes.
        /// </summary>
        private Project project;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Verifies the resulting output files.
        /// </summary>
        [Test]
        public void VerifyOutputFiles()
        {
            this.Setup();

            Assert.AreEqual(
                3, 
                this.outputPersistence.DataCount, 
                "The number of output files was unexpected.");
        }

        /// <summary>
        /// Verifies the contents of the project file.
        /// </summary>
        [Test]
        public void VerifyProjectContents()
        {
            this.Setup();

            List<string> lines = this.outputPersistence.GetDataLines("/");

            this.AssertLines(
                lines, 
                "---", 
                "title: Testing", 
                "---", 
                string.Empty, 
                "1. [Chapter 1a](regions/region-1)", 
                "2. [Not Cheese](regions/region-2)");
        }

        /// <summary>
        /// Verifies the contents of the region-1 file.
        /// </summary>
        [Test]
        public void VerifyRegion1()
        {
            this.Setup();

            List<string> lines =
                this.outputPersistence.GetDataLines("/regions/region-1");

            this.AssertLines(
                lines, 
                "---", 
                "title: Chapter 1a", 
                "---", 
                string.Empty, 
                "One Two Three.");
        }

        /// <summary>
        /// Verifies the contents of the region-2 file.
        /// </summary>
        [Test]
        public void VerifyRegion2()
        {
            this.Setup();

            List<string> lines =
                this.outputPersistence.GetDataLines("/regions/region-2");

            this.AssertLines(
                lines, 
                "---", 
                "title: Not Cheese", 
                "---", 
                string.Empty, 
                "Four Five Six.");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets up this instance.
        /// </summary>
        private void Setup()
        {
            // Create the test input.
            this.inputPersistence = new MemoryPersistence();
            this.inputPersistence.SetData(
                new HierarchicalPath("/"), 
                "---", 
                "title: Testing", 
                "---", 
                "* [Chapter 1](regions/region-1)", 
                "* [Cheese](regions/region-2)");
            this.inputPersistence.SetData(
                new HierarchicalPath("/regions/region-1"), 
                "---", 
                "title: Chapter 1a", 
                "---", 
                "One Two Three.");
            this.inputPersistence.SetData(
                new HierarchicalPath("/regions/region-2"), 
                "---", 
                "title: Not Cheese", 
                "---", 
                "Four Five Six.");

            // Set up the layout.
            var projectLayout = new RegionLayout
                {
                    Name = "Project", 
                    Slug = "project", 
                    HasContent = false, 
                };
            var fixedLayout = new RegionLayout
                {
                    Name = "Sequenced Region", 
                    Slug = "regions/region-$(ContainerIndex:0)", 
                    HasContent = true, 
                    IsSequenced = true, 
                    IsExternal = true, 
                };

            projectLayout.Add(fixedLayout);

            // Create a new project with the given layout.
            this.project = new Project();
            this.project.ApplyLayout(projectLayout);

            // Create the format.
            var format = new MarkdownBufferFormat();

            // Parse the buffer lines.
            var inputContext = new BufferLoadContext(
                this.project, 
                this.inputPersistence);

            format.LoadProject(inputContext);

            // Using the same project layout, we create a new persistence and
            // write out the results.
            this.outputPersistence = new MemoryPersistence();
            this.outputContext = new BufferStoreContext(
                this.project, 
                this.outputPersistence);

            format.StoreProject(this.outputContext);
        }

        #endregion
    }
}