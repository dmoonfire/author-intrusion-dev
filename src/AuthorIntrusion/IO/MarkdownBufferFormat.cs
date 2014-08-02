// <copyright file="MarkdownBufferFormat.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.IO
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    using YamlDotNet.RepresentationModel;

    /// <summary>
    /// Encapsulates the functionality for a buffer format that handles a Markdown file
    /// with YAML metadata (e.g., Jekyll pages).
    /// </summary>
    public class MarkdownBufferFormat : IFileBufferFormat
    {
        #region Public Methods and Operators

        /// <summary>
        /// Loads the data from a string.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <param name="metadata">
        /// The metadata.
        /// </param>
        /// <param name="lines">
        /// The lines.
        /// </param>
        public void Load(
            string input, 
            out Dictionary<string, string> metadata, 
            out IEnumerable<string> lines)
        {
            using (var reader = new StringReader(input))
            {
                this.Load(reader, out metadata, out lines);
            }
        }

        /// <summary>
        /// Loads project data from the persistence layer and populates the project.
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
        public void LoadProject(
            Project project, 
            IPersistence persistence, 
            BufferFormatLoadOptions options)
        {
            Dictionary<string, string> metadata;
            IEnumerable<string> lines;

            using (Stream stream = persistence.GetProjectReadStream())
            {
                this.Load(stream, out metadata, out lines);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads the Markdown file using the given reader.
        /// </summary>
        /// <param name="reader">
        /// The reader.
        /// </param>
        /// <param name="metadata">
        /// The metadata.
        /// </param>
        /// <param name="content">
        /// The content.
        /// </param>
        private void Load(
            TextReader reader, 
            out Dictionary<string, string> metadata, 
            out IEnumerable<string> content)
        {
            // Initialize our output variables.
            var lines = new List<string>();

            metadata = new Dictionary<string, string>();
            content = lines;

            // Loop through the lines, starting with looking for the metadata.
            bool first = true;
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                // If we are on the first line and it appears to be metadata, then parse
                // that first.
                if (first)
                {
                    // We are no longer the first line.
                    first = false;

                    // Look for YAML. If we find it, parse the the metadata data. This will
                    // advance the reader until after the next "---" in the stream.
                    if (line == "---")
                    {
                        this.ReadYamlMetadata(reader, metadata);
                        continue;
                    }
                }

                // For the rest of the lines, we add them to the buffer while normalizing
                // the wrapping and line combinations.
                lines.Add(line);
            }
        }

        /// <summary>
        /// Loads the metadata and content from the given stream.
        /// </summary>
        /// <param name="stream">
        /// The stream to read.
        /// </param>
        /// <param name="metadata">
        /// The metadata.
        /// </param>
        /// <param name="lines">
        /// The lines.
        /// </param>
        private void Load(
            Stream stream, 
            out Dictionary<string, string> metadata, 
            out IEnumerable<string> lines)
        {
            using (var reader = new StreamReader(stream))
            {
                this.Load(reader, out metadata, out lines);
            }
        }

        /// <summary>
        /// Loads metadata from a YAML format.
        /// </summary>
        /// <param name="reader">
        /// The reader.
        /// </param>
        /// <param name="metadata">
        /// The metadata.
        /// </param>
        /// <exception cref="FileLoadException">
        /// Cannot load file without a proper YAML header.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Cannot parse YAML metadata with non-scalar values.
        /// </exception>
        private void ReadYamlMetadata(
            TextReader reader, Dictionary<string, string> metadata)
        {
            // Build up the rest of the line.
            var buffer = new StringBuilder();

            buffer.AppendLine("---");

            // Load the rest of the lines. If we don't get the final "---", then we just skip
            // the metadata loading entirely.
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                // Add the line to the buffer.
                buffer.AppendLine(line);

                // If we get to the next "---", then we stop loading.
                if (line == "---")
                {
                    break;
                }
            }

            // If we got to the end of the buffer without a line, we have a problem.
            if (line == null)
            {
                throw new FileLoadException(
                    "Cannot load file without a proper YAML header.");
            }

            // Get the string of the buffer so we can parse it.
            string bufferText = buffer.ToString();

            buffer.Length = 0;

            // Load the YAML buffer into memory.
            using (var yamlReader = new StringReader(bufferText))
            {
                // Parse the stream into YAML.
                var yaml = new YamlStream();

                yaml.Load(yamlReader);

                // Pull out the root node, which is always a mapping.
                var mapping = (YamlMappingNode)yaml.Documents[0].RootNode;

                foreach (
                    KeyValuePair<YamlNode, YamlNode> entry in mapping.Children)
                {
                    // Pull out the key value pairs.
                    var key = (YamlScalarNode)entry.Key;
                    var value = entry.Value as YamlScalarNode;

                    if (value == null)
                    {
                        throw new InvalidOperationException(
                            "Cannot parse YAML metadata with non-scalar values.");
                    }

                    // Put the values in the metadata dictionary.
                    metadata[key.Value] = value.Value;
                }
            }
        }

        #endregion
    }
}