// <copyright file="MarkdownBufferFormatTests.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using AuthorIntrusion.IO;

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
            Dictionary<string, string> metadata;
            IEnumerable<string> contents;
            format.Load(input, out metadata, out contents);

            // Verify the metadata.
            Assert.AreEqual(
                1, metadata.Count, "Number of metadata keys is unexpected.");
            Assert.IsTrue(
                metadata.ContainsKey("Title"), 
                "Could not find Title key in metadata.");
            Assert.AreEqual(
                "Unit Test", metadata["Title"], "Value of Title was unexpected.");

            // Verify the contents.
            List<string> output = contents.ToList();

            Assert.AreEqual(
                1, output.Count, "Number of output lines was unexpected.");
            Assert.AreEqual(
                "One Two Three.", output[0], "1st output line was unexpected.");
        }

        #endregion
    }
}