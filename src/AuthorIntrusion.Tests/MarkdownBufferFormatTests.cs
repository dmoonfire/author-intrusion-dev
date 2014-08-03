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
            MetadataDictionary metadata;
            BlockCollection contents;

            format.Load(project, input, out metadata, out contents);

            // Verify the contents.
            Assert.AreEqual(
                2, contents.Count, "Number of output lines was unexpected.");
            Assert.AreEqual(
                "One Two Three.", contents[0], "1st output line was unexpected.");
            Assert.AreEqual(
                "Four Five Six.", contents[1], "2nd output line was unexpected.");
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
                    "Title: Unit Test", 
                    "---", 
                    string.Empty, 
                    "One Two Three.", 
                };
            string input = lines.Join();

            // Create the format.
            var format = new MarkdownBufferFormat();

            // Parse the buffer lines.
            var project = new Project();
            MetadataDictionary metadata;
            BlockCollection contents;

            format.Load(project, input, out metadata, out contents);

            // Verify the metadata.
            MetadataKey titleKey = project.MetadataManager["Title"];

            Assert.AreEqual(
                1, metadata.Count, "Number of metadata keys is unexpected.");
            Assert.IsTrue(
                metadata.ContainsKey(titleKey), 
                "Could not find Title key in metadata.");
            Assert.AreEqual(
                "Unit Test", 
                metadata[titleKey].Value, 
                "Value of Title was unexpected.");

            // Verify the contents.
            Assert.AreEqual(
                1, contents.Count, "Number of output lines was unexpected.");
            Assert.AreEqual(
                "One Two Three.", contents[0], "1st output line was unexpected.");
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
            MetadataDictionary metadata;
            BlockCollection contents;

            format.Load(project, input, out metadata, out contents);

            // Verify the metadata.
            Assert.AreEqual(
                0, metadata.Count, "Number of metadata keys is unexpected.");

            // Verify the contents.
            Assert.AreEqual(
                1, contents.Count, "Number of output lines was unexpected.");
            Assert.AreEqual(
                "One Two Three.", contents[0], "1st output line was unexpected.");
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
                    "Title: Unit Test", 
                    "---", 
                    "One Two Three.", 
                };
            string input = lines.Join();

            // Create the format.
            var format = new MarkdownBufferFormat();

            // Parse the buffer lines.
            var project = new Project();
            MetadataDictionary metadata;
            BlockCollection contents;

            format.Load(project, input, out metadata, out contents);

            // Verify the metadata.
            MetadataKey titleKey = project.MetadataManager["Title"];

            Assert.AreEqual(
                1, metadata.Count, "Number of metadata keys is unexpected.");
            Assert.IsTrue(
                metadata.ContainsKey(titleKey), 
                "Could not find Title key in metadata.");
            Assert.AreEqual(
                "Unit Test", 
                metadata[titleKey].Value, 
                "Value of Title was unexpected.");

            // Verify the contents.
            Assert.AreEqual(
                1, contents.Count, "Number of output lines was unexpected.");
            Assert.AreEqual(
                "One Two Three.", contents[0], "1st output line was unexpected.");
        }

        /// <summary>
        /// Tests reading input with only metadata.
        /// </summary>
        [Test]
        public void YamlMetadataOnly()
        {
            // Create the test input.
            var lines = new List<string> { "---", "Title: Unit Test", "---", };
            string input = lines.Join();

            // Create the format.
            var format = new MarkdownBufferFormat();

            // Parse the buffer lines.
            var project = new Project();
            MetadataDictionary metadata;
            BlockCollection contents;

            format.Load(project, input, out metadata, out contents);

            // Verify the metadata.
            MetadataKey titleKey = project.MetadataManager["Title"];

            Assert.AreEqual(
                1, metadata.Count, "Number of metadata keys is unexpected.");
            Assert.IsTrue(
                metadata.ContainsKey(titleKey), 
                "Could not find Title key in metadata.");
            Assert.AreEqual(
                "Unit Test", 
                metadata[titleKey].Value, 
                "Value of Title was unexpected.");

            // Verify the contents.
            Assert.AreEqual(
                0, contents.Count, "Number of output lines was unexpected.");
        }

        #endregion
    }
}