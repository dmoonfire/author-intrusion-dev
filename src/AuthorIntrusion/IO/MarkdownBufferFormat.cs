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

    using AuthorIntrusion.Buffers;
    using AuthorIntrusion.Metadata;

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
        /// <param name="project">
        /// </param>
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
            Project project, 
            string input, 
            out MetadataDictionary metadata, 
            out BlockCollection lines)
        {
            using (var reader = new StringReader(input))
            {
                this.Load(project, reader, out metadata, out lines);
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
            MetadataDictionary metadata;
            BlockCollection blocks;

            using (Stream stream = persistence.GetProjectReadStream())
            {
                // Load information about the project into memory.
                this.Load(project, stream, out metadata, out blocks);
            }

            // Completely replace the metadata in the project.
            project.Metadata.Set(metadata);

            // Copy the lines into the project.
            project.Blocks.AddRange(blocks);
        }

        /// <summary>
        /// Writes out the project to the given persistence using the
        /// format instance.
        /// </summary>
        /// <param name="project">
        /// The project to write out.
        /// </param>
        /// <param name="persistence">
        /// The persistence layer to use.
        /// </param>
        /// <exception cref="System.InvalidOperationException">
        /// Cannot write out files with Markdown.
        /// </exception>
        public void StoreProject(Project project, IPersistence persistence)
        {
            throw new InvalidOperationException(
                "Cannot write out files with Markdown.");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads the Markdown file using the given reader.
        /// </summary>
        /// <param name="project">
        /// The project.
        /// </param>
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
            Project project, 
            TextReader reader, 
            out MetadataDictionary metadata, 
            out BlockCollection content)
        {
            // Initialize our output variables.
            metadata = new MetadataDictionary();
            content = new BlockCollection();

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
                        this.ReadYamlMetadata(project, reader, metadata);
                        continue;
                    }
                }

                // Because AI-flavored Markdown is based on Github, we pretty much ignore
                // blank lines.
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                // For the rest of the lines, we add them to the buffer while normalizing
                // the wrapping and line combinations.
                var block = new Block(line);

                content.Add(block);
            }
        }

        /// <summary>
        /// Loads the metadata and content from the given stream.
        /// </summary>
        /// <param name="project">
        /// The project.
        /// </param>
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
            Project project, 
            Stream stream, 
            out MetadataDictionary metadata, 
            out BlockCollection lines)
        {
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                this.Load(project, reader, out metadata, out lines);
            }
        }

        /// <summary>
        /// Loads metadata from a YAML format.
        /// </summary>
        /// <param name="project">
        /// The project.
        /// </param>
        /// <param name="reader">
        /// The reader.
        /// </param>
        /// <param name="metadata">
        /// The metadata.
        /// </param>
        /// <exception cref="System.IO.FileLoadException">
        /// Cannot load file without a proper YAML header.
        /// </exception>
        /// <exception cref="System.InvalidOperationException">
        /// Cannot parse YAML metadata with sequences of anything but scalars.
        /// or
        /// Cannot parse YAML metadata with mapping/dictionary values.
        /// </exception>
        /// <exception cref="FileLoadException">
        /// Cannot load file without a proper YAML header.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Cannot parse YAML metadata with non-scalar values.
        /// </exception>
        private void ReadYamlMetadata(
            Project project, TextReader reader, MetadataDictionary metadata)
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
                    var scalar = entry.Value as YamlScalarNode;
                    var sequence = entry.Value as YamlSequenceNode;

                    MetadataKey metadataKey = project.MetadataManager[key.Value];
                    MetadataValue metadataValue =
                        metadata.GetOrCreate(metadataKey);

                    if (scalar != null)
                    {
                        // Pull out the single value.
                        metadataValue.Add(scalar.Value);
                    }
                    else if (sequence != null)
                    {
                        // Loop through the sequence and pull in the values.
                        foreach (YamlNode seqValue in sequence.Children)
                        {
                            var seqScalar = seqValue as YamlScalarNode;

                            if (seqScalar == null)
                            {
                                throw new InvalidOperationException(
                                    "Cannot parse YAML metadata with sequences of anything but scalars.");
                            }

                            metadataValue.Add(seqScalar.Value);
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException(
                            "Cannot parse YAML metadata with mapping/dictionary values.");
                    }
                }
            }
        }

        #endregion
    }
}