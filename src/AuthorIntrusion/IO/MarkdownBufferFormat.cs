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
    using AuthorIntrusion.Cli.Transform;
    using AuthorIntrusion.Extensions.System.Collections.Generic;
    using AuthorIntrusion.Metadata;

    using YamlDotNet.RepresentationModel;
    using YamlDotNet.Serialization;

    /// <summary>
    /// Encapsulates the functionality for a buffer format that handles a Markdown file
    /// with YAML metadata (e.g., Jekyll pages).
    /// </summary>
    public class MarkdownBufferFormat : IFileBufferFormat
    {
        #region Public Properties

        /// <summary>
        /// Gets the current settings associated with the format.
        /// </summary>
        public IBufferFormatSettings Settings { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Loads the data from a string.
        /// </summary>
        /// <param name="project">
        /// The project.
        /// </param>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <param name="buffer">
        /// The buffer.
        /// </param>
        public void Load(Project project, string input, IProjectBuffer buffer)
        {
            using (var reader = new StringReader(input))
            {
                this.Load(project, reader, buffer);
            }
        }

        /// <summary>
        /// Loads a profile of a specific format into memory. Profiles are either
        /// internally identified by the format and may be stored as part of
        /// a project's settings.
        /// </summary>
        /// <param name="profileName">
        /// The name of the profile to load.
        /// </param>
        public void LoadProfile(string profileName)
        {
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
            using (Stream stream = persistence.GetProjectReadStream())
            {
                // Load information about the project into memory.
                this.Load(project, stream, project);
            }
        }

        /// <summary>
        /// Stores the specified buffer to the given persistence.
        /// </summary>
        /// <param name="project">
        /// The project.
        /// </param>
        /// <param name="persistence">
        /// The persistence.
        /// </param>
        /// <param name="stream">
        /// The stream.
        /// </param>
        /// <param name="buffer">
        /// The buffer.
        /// </param>
        public void Store(
            Project project, 
            IPersistence persistence, 
            Stream stream, 
            IProjectBuffer buffer)
        {
            using (var writer = new StreamWriter(stream))
            {
                this.Store(project, persistence, writer, buffer);
            }
        }

        /// <summary>
        /// Stores the specified project.
        /// </summary>
        /// <param name="project">
        /// The project.
        /// </param>
        /// <param name="persistence">
        /// The persistence.
        /// </param>
        /// <param name="writer">
        /// The writer.
        /// </param>
        /// <param name="buffer">
        /// The buffer.
        /// </param>
        public void Store(
            Project project, 
            IPersistence persistence, 
            TextWriter writer, 
            IProjectBuffer buffer)
        {
            // Write out the YAML header.
            this.StoreMetadata(writer, buffer);

            // Write out all the buffer lines.
            foreach (Block block in buffer.Blocks)
            {
                writer.WriteLine();
                writer.WriteLine(block.Text);
            }
        }

        /// <summary>
        /// Stores the specified buffer to the given persistence.
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
            using (Stream stream = persistence.GetProjectWriteStream())
            {
                // Load information about the project into memory.
                this.Store(project, persistence, stream, project);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Parses the YAML metadata.
        /// </summary>
        /// <param name="project">
        /// The project.
        /// </param>
        /// <param name="buffer">
        /// The buffer.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <exception cref="System.InvalidOperationException">
        /// Cannot parse YAML metadata with sequences of anything but scalars.
        /// or
        /// Cannot parse YAML metadata with mapping/dictionary values.
        /// </exception>
        private static void ParseYamlMetadata(
            Project project, IProjectBuffer buffer, string key, YamlNode node)
        {
            // For everything else, we keep it as a metadata entry.
            MetadataKey metadataKey = project.MetadataManager[key];
            MetadataValue metadataValue =
                buffer.Metadata.GetOrCreate(metadataKey);

            // Based on the type determines how we parse it.
            var scalar = node as YamlScalarNode;
            var sequence = node as YamlSequenceNode;

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

        /// <summary>
        /// Loads the Markdown file using the given reader.
        /// </summary>
        /// <param name="project">
        /// The project.
        /// </param>
        /// <param name="reader">
        /// The reader.
        /// </param>
        /// <param name="buffer">
        /// The buffer.
        /// </param>
        private void Load(
            Project project, TextReader reader, IProjectBuffer buffer)
        {
            // This is a complete load.
            buffer.Metadata.Clear();
            buffer.Blocks.Clear();

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
                        this.ReadYamlMetadata(project, reader, buffer);
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

                buffer.Blocks.Add(block);
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
        /// <param name="buffer">
        /// The buffer.
        /// </param>
        private void Load(Project project, Stream stream, IProjectBuffer buffer)
        {
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                this.Load(project, reader, buffer);
            }
        }

        /// <summary>
        /// Parses the YAML author.
        /// </summary>
        /// <param name="project">
        /// The project.
        /// </param>
        /// <param name="buffer">
        /// The buffer.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="node">
        /// The node.
        /// </param>
        private void ParseYamlAuthor(
            Project project, IProjectBuffer buffer, string key, YamlNode node)
        {
            string scalar = ((YamlScalarNode)node).Value;

            switch (key)
            {
                case "author":
                    buffer.Authors.PreferredName = scalar;
                    break;
            }
        }

        /// <summary>
        /// Parses the YAML title.
        /// </summary>
        /// <param name="project">
        /// The project.
        /// </param>
        /// <param name="buffer">
        /// The buffer.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="node">
        /// The node.
        /// </param>
        private void ParseYamlTitle(
            Project project, IProjectBuffer buffer, string key, YamlNode node)
        {
            string scalar = ((YamlScalarNode)node).Value;

            switch (key)
            {
                case "title":
                    buffer.Titles.Title = scalar;
                    break;

                case "subtitle":
                    buffer.Titles.Subtitle = scalar;
                    break;
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
        /// <param name="buffer">
        /// The buffer.
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
            Project project, TextReader reader, IProjectBuffer buffer)
        {
            // Build up the rest of the line.
            var builder = new StringBuilder();

            builder.AppendLine("---");

            // Load the rest of the lines. If we don't get the final "---", then we just skip
            // the metadata loading entirely.
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                // Add the line to the buffer.
                builder.AppendLine(line);

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
            string bufferText = builder.ToString();

            builder.Length = 0;

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
                    string key = ((YamlScalarNode)entry.Key).Value;

                    // Based on the key, we will put the value somewhere.
                    string lowerKey = key.ToLower();

                    switch (lowerKey)
                    {
                        case "author":
                            this.ParseYamlAuthor(
                                project, buffer, lowerKey, entry.Value);
                            break;

                        case "title":
                        case "subtitle":
                            this.ParseYamlTitle(
                                project, buffer, lowerKey, entry.Value);
                            break;

                        default:

                            // For everything else, treat it as metadata.
                            ParseYamlMetadata(project, buffer, key, entry.Value);
                            return;
                    }
                }
            }
        }

        /// <summary>
        /// Writes out the metadata to the given writer.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        /// <param name="buffer">
        /// The buffer.
        /// </param>
        /// <exception cref="System.NotImplementedException">
        /// </exception>
        private void StoreMetadata(TextWriter writer, IProjectBuffer buffer)
        {
            // Create a dictionary with the metadata and start adding elements.
            var metadata = new Dictionary<string, object>();

            metadata.AddIfNotEmpty("Title", buffer.Titles.Title);
            metadata.AddIfNotEmpty("Subtitle", buffer.Titles.Subtitle);

            metadata.AddIfNotEmpty("Author", buffer.Authors.PreferredName);

            // Loop through and add the rest of the metadata.
            foreach (MetadataKey key in buffer.Metadata.Keys)
            {
                metadata[key.Name] = buffer.Metadata[key];
            }

            // Write out the YAML header.
            var serializer = new Serializer();

            writer.WriteLine("---");
            serializer.Serialize(writer, metadata);
            writer.WriteLine("---");
        }

        #endregion
    }
}