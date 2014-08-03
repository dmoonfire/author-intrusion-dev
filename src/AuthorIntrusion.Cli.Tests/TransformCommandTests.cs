// <copyright file="TransformCommandTests.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.Cli.Tests
{
    using System.IO;

    using AuthorIntrusion.Cli.Transform;

    using NUnit.Framework;

    /// <summary>
    /// Contains the simple commands to exercise the `transform` command from the
    /// CLI.
    /// </summary>
    public class TransformCommandTests : WorkingDirectoryTestsBase
    {
        #region Public Methods and Operators

        /// <summary>
        /// Tests loading a simple markdown file into memory and then write it out.
        /// </summary>
        [Test]
        public void SimpleMarkdownToMarkdownChapter()
        {
            // Create the options and populate the values.
            var options = new TransformOptions
                {
                    Input =
                        Path.Combine(
                            this.SamplesDirectory.FullName, 
                            "Frankenstein Markdown", 
                            "chapter-01.markdown"), 
                    Output =
                        Path.Combine(this.WorkingDirectory.FullName, "output.xml"), 
                };

            // Create the transform command and run the job.
            var command = this.Container.GetInstance<TransformCommand>();

            command.Run(options);
        }

        #endregion
    }
}