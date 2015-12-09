using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.AliaSql
{
    /// <summary>
    /// Contains settings used by <see cref="AliaSqlRunner"/>.
    /// </summary>
    public class AliaSqlSettings : ToolSettings
    {       
        /// <summary>
        /// The Connection String to connect to the target database.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// The database to run scripts against.
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// The scripts folder containing Create, Update, and/or TestData scripts.
        /// </summary>
        public DirectoryPath ScriptsFolder { get; set; }

        /// <summary>
        /// The AliaSql command to run. (ex: TestData, Rebuild)
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// The username used for authenticating with the database.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The password used for authenticating with the database.
        /// </summary>
        public string Password { get; set; }
    }
}
