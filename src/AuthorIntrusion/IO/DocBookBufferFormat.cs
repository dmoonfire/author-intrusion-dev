// <copyright file="DocBookBufferFormat.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.IO
{
    using AuthorIntrusion.Extensions.System.Xml;
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;

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

        #region Public Methods and Operators

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
                    writer.WriteStartElement("article", DocBookNamespace);
                    writer.WriteAttributeString("version", "5.0");

                    // Write out the info tag.
                    writer.WriteStartElement("info");
                    writer.WriteElementString("title", project, "Title");

                    // Write out the end of the document.
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
            }
        }

        #endregion
    }
}