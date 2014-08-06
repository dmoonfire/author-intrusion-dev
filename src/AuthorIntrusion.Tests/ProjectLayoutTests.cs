// <copyright file="ProjectLayoutTests.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.Tests
{
    using System;

    using AuthorIntrusion.Buffers;

    using MarkdownLog;

    using NUnit.Framework;

    /// <summary>
    /// Tests functionality for applying layouts and the resulting regions.
    /// </summary>
    [TestFixture]
    public class ProjectLayoutTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// Tests applying a single region from the default.
        /// </summary>
        [Test]
        public void ApplySingleLayout()
        {
            // Set up the layout.
            var projectLayout = new RegionLayout
                {
                    Name = "Project", 
                    Slug = "project", 
                    HasContent = false, 
                };
            projectLayout.InnerLayouts.Add(
                new RegionLayout
                    {
                        Name = "Region 1", 
                        Slug = "region-1", 
                        HasContent = true, 
                    });

            // Create a new project with the given layout.
            var project = new Project();
            project.ApplyLayout(projectLayout);

            // Assert the results.
            Assert.AreEqual(
                2, project.Regions.Count, "Number of regions is unexpected.");
            Assert.IsTrue(
                project.Regions.ContainsKey("project"), 
                "Cannot find root project region.");
            Assert.IsTrue(
                project.Regions.ContainsKey("region-1"), "Cannot find region 1.");

            Assert.AreEqual(
                1, 
                project.Regions["project"].Blocks.Count, 
                "The root region has an unexpected number of blocks.");

            Assert.AreEqual(
                0, 
                project.Regions["region-1"].Blocks.Count, 
                "The region-1 region has an unexpected number of blocks.");

            // Write out the final state.
            var markdown = new MarkdownContainer();

            markdown.Append(new Header("Final Project State"));
            project.ToMarkdown(markdown);

            Console.WriteLine(markdown);
        }

        #endregion
    }
}