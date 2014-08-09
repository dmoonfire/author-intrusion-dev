// <copyright file="MarkdownBufferFormatTests.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.Tests
{
    using System.Collections.Generic;

    using AuthorIntrusion.Buffers;
    using AuthorIntrusion.IO;
    using AuthorIntrusion.Metadata;

    using MfGames.Extensions.System.Collections.Generic;

    using NUnit.Framework;

    /// <summary>
    /// Tests various parsing various input into the MarkdownBufferFormat.
    /// </summary>
    [TestFixture]
    public class MarkdownBufferFormatTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// Tests reading a blank-line separated content.
        /// </summary>
        [Test]
        public void BlankSeparatedParagraphs()
        {
            // Create the test input.
            var lines = new List<string>
                {
                    "One Two Three.", 
                    string.Empty, 
                    "Four Five Six.", 
                };
            string input = lines.Join();

            // Create the format.
            var format = new MarkdownBufferFormat();

            // Parse the buffer lines.
            var project = new Project();
            BlockCollection contents = project.Blocks;
            var context = new BufferLoadContext(project, null);

            format.Load(context, input, project);

            // Verify the contents.
            Assert.AreEqual(
                2, contents.Count, "Number of output lines was unexpected.");
            Assert.AreEqual(
                "One Two Three.", 
                contents[0].Text, 
                "1st output line was unexpected.");
            Assert.AreEqual(
                "Four Five Six.", 
                contents[1].Text, 
                "2nd output line was unexpected.");
        }

        /// <summary>
        /// Tests reading contents that have a leading blank line before the text.
        /// </summary>
        [Test]
        public void LeadingBlankLineYamlMetadata()
        {
            // Create the test input.
            var lines = new List<string>
                {
                    "---", 
                    "Scalar: Unit Test", 
                    "---", 
                    string.Empty, 
                    "One Two Three.", 
                };
            string input = lines.Join();

            // Create the format.
            var format = new MarkdownBufferFormat();

            // Parse the buffer lines.
            var project = new Project();
            MetadataDictionary metadata = project.Metadata;
            BlockCollection contents = project.Blocks;
            var context = new BufferLoadContext(project, null);

            format.Load(context, input, project);

            // Verify the metadata.
            MetadataKey titleKey = project.MetadataManager["Scalar"];

            Assert.AreEqual(
                1, metadata.Count, "Number of metadata keys is unexpected.");
            Assert.IsTrue(
                metadata.ContainsKey(titleKey), 
                "Could not find Scalar key in metadata.");
            Assert.AreEqual(
                "Unit Test", 
                metadata[titleKey].Value, 
                "Value of Scalar was unexpected.");

            // Verify the contents.
            Assert.AreEqual(
                1, contents.Count, "Number of output lines was unexpected.");
            Assert.AreEqual(
                "One Two Three.", 
                contents[0].Text, 
                "1st output line was unexpected.");
        }

        /// <summary>
        /// Tests reading a single inline region.
        /// </summary>
        [Test]
        public void LoadInlineSingleRegion()
        {
            // Create the test input.
            var lines = new List<string>
                {
                    "# Region 1", 
                    string.Empty, 
                    "Text in region 1.", 
                };
            string input = lines.Join();

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

            // Create the format.
            var format = new MarkdownBufferFormat();

            // Parse the buffer lines.
            var context = new BufferLoadContext(project, null);

            format.Load(context, input, project);

            // Verify the contents of the project.
            Region region1 = project.Regions["region-1"];

            Assert.AreEqual(
                1, 
                project.Blocks.Count, 
                "Number of lines in the project was unexpected.");
            Assert.AreEqual(
                BlockType.Region, 
                project.Blocks[0].BlockType, 
                "The block type of project's link block is unexpected.");
            Assert.AreEqual(
                region1, 
                project.Blocks[0].LinkedRegion, 
                "The linked region of the link type is unexpected.");

            // Get the second region.
            Assert.AreEqual(
                1, 
                region1.Blocks.Count, 
                "Number of lines in region 1 was unexpected.");
            Assert.AreEqual(
                "Text in region 1.", 
                region1.Blocks[0].Text, 
                "The text in region 1 was unexpected.");
        }

        /// <summary>
        /// Tests reading input that has no metadata.
        /// </summary>
        [Test]
        public void NoYamlMetadata()
        {
            // Create the test input.
            var lines = new List<string> { "One Two Three.", };
            string input = lines.Join();

            // Create the format.
            var format = new MarkdownBufferFormat();

            // Parse the buffer lines.
            var project = new Project();
            MetadataDictionary metadata = project.Metadata;
            BlockCollection contents = project.Blocks;
            var context = new BufferLoadContext(project, null);

            format.Load(context, input, project);

            // Verify the metadata.
            Assert.AreEqual(
                0, metadata.Count, "Number of metadata keys is unexpected.");

            // Verify the contents.
            Assert.AreEqual(
                1, contents.Count, "Number of output lines was unexpected.");
            Assert.AreEqual(
                "One Two Three.", 
                contents[0].Text, 
                "1st output line was unexpected.");
        }

        /// <summary>
        /// Tests reading in a single line Markdown with a single metadata.
        /// </summary>
        [Test]
        public void SimpleYamlMetadata()
        {
            // Create the test input.
            var lines = new List<string>
                {
                    "---", 
                    "Scalar: Unit Test", 
                    "---", 
                    "One Two Three.", 
                };
            string input = lines.Join();

            // Create the format.
            var format = new MarkdownBufferFormat();

            // Parse the buffer lines.
            var project = new Project();
            MetadataDictionary metadata = project.Metadata;
            BlockCollection contents = project.Blocks;
            var context = new BufferLoadContext(project, null);

            format.Load(context, input, project);

            // Verify the metadata.
            MetadataKey titleKey = project.MetadataManager["Scalar"];

            Assert.AreEqual(
                1, metadata.Count, "Number of metadata keys is unexpected.");
            Assert.IsTrue(
                metadata.ContainsKey(titleKey), 
                "Could not find Scalar key in metadata.");
            Assert.AreEqual(
                "Unit Test", 
                metadata[titleKey].Value, 
                "Value of Scalar was unexpected.");

            // Verify the contents.
            Assert.AreEqual(
                1, contents.Count, "Number of output lines was unexpected.");
            Assert.AreEqual(
                "One Two Three.", 
                contents[0].Text, 
                "1st output line was unexpected.");
        }

        /// <summary>
        /// Tests reading input with only an author.
        /// </summary>
        [Test]
        public void YamlAuthorOnly()
        {
            // Create the test input.
            var lines = new List<string> { "---", "Author: Unit Test", "---", };
            string input = lines.Join();

            // Create the format.
            var format = new MarkdownBufferFormat();

            // Parse the buffer lines.
            var project = new Project();
            MetadataDictionary metadata = project.Metadata;
            var context = new BufferLoadContext(project, null);

            format.Load(context, input, project);

            // Verify the metadata.
            Assert.AreEqual(
                0, metadata.Count, "Number of metadata keys is unexpected.");

            // Verify the title.
            Assert.AreEqual(
                "Unit Test", 
                project.Authors.PreferredName, 
                "Primary name was unexpected.");
        }

        /// <summary>
        /// Tests reading input with only metadata.
        /// </summary>
        [Test]
        public void YamlMetadataOnly()
        {
            // Create the test input.
            var lines = new List<string> { "---", "Scalar: Unit Test", "---", };
            string input = lines.Join();

            // Create the format.
            var format = new MarkdownBufferFormat();

            // Parse the buffer lines.
            var project = new Project();
            MetadataDictionary metadata = project.Metadata;
            BlockCollection contents = project.Blocks;
            var context = new BufferLoadContext(project, null);

            format.Load(context, input, project);

            // Verify the metadata.
            MetadataKey titleKey = project.MetadataManager["Scalar"];

            Assert.AreEqual(
                1, metadata.Count, "Number of metadata keys is unexpected.");
            Assert.IsTrue(
                metadata.ContainsKey(titleKey), 
                "Could not find Scalar key in metadata.");
            Assert.AreEqual(
                "Unit Test", 
                metadata[titleKey].Value, 
                "Value of Scalar was unexpected.");

            // Verify the contents.
            Assert.AreEqual(
                0, contents.Count, "Number of output lines was unexpected.");
        }

        /// <summary>
        /// Tests reading input with only title.
        /// </summary>
        [Test]
        public void YamlTitleOnly()
        {
            // Create the test input.
            var lines = new List<string> { "---", "Title: Unit Test", "---", };
            string input = lines.Join();

            // Create the format.
            var format = new MarkdownBufferFormat();

            // Parse the buffer lines.
            var project = new Project();
            MetadataDictionary metadata = project.Metadata;
            var context = new BufferLoadContext(project, null);

            format.Load(context, input, project);

            // Verify the metadata.
            Assert.AreEqual(
                0, metadata.Count, "Number of metadata keys is unexpected.");

            // Verify the title.
            Assert.AreEqual(
                "Unit Test", project.Titles.Title, "Title was unexpected.");
        }

        #endregion
    }
}