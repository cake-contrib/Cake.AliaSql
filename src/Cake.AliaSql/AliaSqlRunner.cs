using System;
using System.Collections.Generic;
using System.Globalization;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.AliaSql
{
    /// <summary>
    /// Runner for AliaSQL.
    /// </summary>
    public sealed class AliaSqlRunner : Tool<AliaSqlSettings>
    {
        private readonly IFileSystem _fileSystem;

		/// <summary>
		/// Initializes a new instance of the <see cref="AliaSqlRunner"/> class.
		/// </summary>
		/// <param name="fileSystem"></param>
		/// <param name="environment"></param>
		/// <param name="processRunner"></param>
		/// <param name="tools"></param>
	    public AliaSqlRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator tools)
			: base(fileSystem, environment, processRunner, tools)
	    {
		    _fileSystem = fileSystem;
	    }

        /// <summary>
        /// Runs AliaSql with the provided settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public void Run(AliaSqlSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }

            Run(settings, GetArguments(settings));
        }

        private ProcessArgumentBuilder GetArguments(AliaSqlSettings settings)
        {
            if (string.IsNullOrWhiteSpace(settings.Command))
            {                           
               throw new ArgumentException(
                    string.Format(CultureInfo.InvariantCulture, "{0}: Command not specified or missing ({1})", GetToolName(), settings.Command));
            }

            if (string.IsNullOrWhiteSpace(settings.ConnectionString))
            {                           
               throw new ArgumentException(
                    string.Format(CultureInfo.InvariantCulture, "{0}: ConnectionString not specified or missing ({1})", GetToolName(), settings.ConnectionString));
            }

            if (string.IsNullOrWhiteSpace(settings.DatabaseName))
            {                           
               throw new ArgumentException(
                    string.Format(CultureInfo.InvariantCulture, "{0}: DatabaseName not specified or missing ({1})", GetToolName(), settings.DatabaseName));
            }

            if (settings.ScriptsFolder==null || !_fileSystem.GetDirectory(settings.ScriptsFolder).Exists)
            {                           
               throw new ArgumentException(
                    string.Format(CultureInfo.InvariantCulture, "{0}: ScriptsFolder not specified or missing ({1})", GetToolName(), settings.ScriptsFolder));
            }

            // AliaSql Format: [Command] [Database Server] [Database Name] [Scripts path] ([username] [password]?)
            var builder = new ProcessArgumentBuilder();
            builder.AppendQuoted(settings.Command);
            builder.AppendQuoted(settings.ConnectionString);
            builder.AppendQuoted(settings.DatabaseName);
            builder.AppendQuoted(settings.ScriptsFolder.FullPath);

            // If we have user authentication info, use it.
            if (!string.IsNullOrEmpty(settings.UserName))
            {
                builder.AppendQuoted(settings.UserName);

                if (!string.IsNullOrEmpty(settings.Password))
                {
                    builder.AppendQuotedSecret(settings.Password);
                }
            }

            return builder;
        }

        /// <summary>
        /// Get AliaSql tool name.
        /// </summary>
        /// <returns></returns>
        protected override string GetToolName()
        {
            return "AliaSql";
        }

        /// <summary>
        /// Gets the possible names of the AliaSql's tool executable.
        /// </summary>
        /// <returns>The tool executable name.</returns>
        protected override IEnumerable<string> GetToolExecutableNames()
        {
            return new[] { "AliaSQL.exe" };
        }
    }
}
