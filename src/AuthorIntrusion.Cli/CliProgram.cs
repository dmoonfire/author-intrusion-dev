// <copyright file="CliProgram.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.Cli
{
    using System;

    using AuthorIntrusion.Cli.Transform;
    using AuthorIntrusion.Plugins;

    using CommandLine;

    /// <summary>
    /// Main program for the command-line interface (CLI).
    /// </summary>
    public class CliProgram
    {
        #region Methods

        /// <summary>
        /// Main entry point for the CLI application.
        /// </summary>
        /// <param name="args">
        /// The arguments from the command line.
        /// </param>
        private static void Main(string[] args)
        {
            // Parse the arguments into an options object.
            string invokedVerb = null;
            object invokedOptions = null;

            var options = new CliOptions();
            bool successful = Parser.Default.ParseArguments(
                args, 
                options, 
                (verb, subOptions) =>
                    {
                        // if parsing succeeds the verb name and correct instance
                        // will be passed to onVerbCommand delegate (string,object)
                        invokedVerb = verb;
                        invokedOptions = subOptions;
                    });

            if (!successful)
            {
                Environment.Exit(Parser.DefaultExitCodeFail);
            }

            // Set up the plugins.
            var container = new PluginContainer(new CliRegistry());
            container.AssertConfigurationIsValid();

            // Determine which command to run.
            if (invokedVerb == TransformOptions.LongName)
            {
                var transformOptions = (TransformOptions)invokedOptions;
                var transformCommand = container.GetInstance<TransformCommand>();

                transformCommand.Run(transformOptions);
            }
        }

        #endregion
    }
}