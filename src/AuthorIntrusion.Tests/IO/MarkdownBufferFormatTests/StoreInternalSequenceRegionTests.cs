﻿// <copyright file="StoreInternalSequenceRegionTests.cs" company="Moonfire Games">
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
    /// Tests various aspects of storing an internal sequence region in Markdown.
    /// </summary>
    [TestFixture]
    public class StoreInternalSequenceRegionTests : MemoryPersistenceTestsBase
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
                1, 
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
                "# Chapter 1 [regions/region-1]", 
                string.Empty, 
                "One Two Three.", 
                string.Empty, 
                "# Cheese [regions/region-2]", 
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
                "# Chapter 1 [regions/region-1]", 
                "One Two Three.", 
                "# Cheese [regions/region-2]", 
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