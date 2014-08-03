// <copyright file="DocBookBufferFormat.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.IO
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;

    using AuthorIntrusion.Buffers;
    using AuthorIntrusion.Cli.Transform;
    using AuthorIntrusion.Extensions.System.Xml;

    /// <summary>
    /// Encapsulates the functionality for a buffer format that handles a DocBook 5
    /// file.
    /// </summary>
    public class DocBookBufferFormat : IFileBufferFormat
    {
        #region Constants

        /// <summary>
        /// The DocBook 5 XML namespace.
        /// </summary>
        public const string DocBookNamespace = "http://docbook.org/ns/docbook";

        #endregion

        #region Fields

        /// <summary>
        /// The settings for the DocBook format.
        /// </summary>
        private readonly DocBookBufferFormatSettings settings;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DocBookBufferFormat"/> class.
        /// </summary>
        public DocBookBufferFormat()
        {
            this.settings = new DocBookBufferFormatSettings();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the current settings associated with the format.
        /// </summary>
        public IBufferFormatSettings Settings
        {
            get
            {
                return this.settings;
            }
        }

        #endregion

        #region Public Methods and Operators

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
        /// <exception cref="System.InvalidOperationException">
        /// Cannot read DocBook 5 files.
        /// </exception>
        public void LoadProject(
            Project project, 
            IPersistence persistence, 
            BufferFormatLoadOptions options)
        {
            throw new InvalidOperationException("Cannot read DocBook 5 files.");
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
        public void StoreProject(Project project, IPersistence persistence)
        {
            // Write out the project's stream first. This will recursively call
            // all of the other write operations.
            using (Stream stream = persistence.GetProjectWriteStream())
            {
                // Create the XML writer for the file.
                var settings = new XmlWriterSettings
                    {
                        Indent = true, 
                        IndentChars = "\t", 
                        NamespaceHandling = NamespaceHandling.OmitDuplicates, 
                        CloseOutput = true, 
                        ConformanceLevel = ConformanceLevel.Document, 
                        Encoding = Encoding.UTF8, 
                    };

                using (XmlWriter writer = XmlWriter.Create(stream, settings))
                {
                    // Write out the document start.
                    writer.WriteStartDocument();
                    writer.WriteStartElement(
                        this.settings.RootElement, DocBookNamespace);
                    writer.WriteAttributeString("version", "5.0");

                    // Write out the info tag.
                    writer.WriteStartElement("info");
                    writer.WriteElementString("title", project, "Title");
                    writer.WriteEndElement();

                    // Loop through and add all the lines.
                    foreach (Block block in project.Blocks)
                    {
                        writer.WriteElementString("para", block.Text);
                    }

                    // Write out the end of the document.
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                    writer.Close();
                }

                // Close the stream.
                stream.Close();
            }
        }

        #endregion
    }
}