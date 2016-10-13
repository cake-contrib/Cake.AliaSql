using Cake.Common.Tests.Fixtures;
using Cake.Testing;

namespace Cake.AliaSql.Tests
{
    public sealed class AliaSqlFixture : ToolFixture<AliaSqlSettings> 
    {
        protected override void RunTool()
        {
            var tool = new AliaSqlRunner(FileSystem, Environment, ProcessRunner, ToolLocator);
            tool.Run(Settings);
        }

        public AliaSqlFixture(bool scriptsFolderExists = true) : base("AliaSQL.exe")
        {
            Settings.Command = "Create";
            Settings.ConnectionString = "localhost";
            Settings.DatabaseName = "AdventureWorks";
            Settings.ScriptsFolder = "/Working/scripts";
            Environment.WorkingDirectory = "/Working";

            if (scriptsFolderExists)
            {
                FileSystem.CreateDirectory("/Working/scripts");
            }
        }
    }
}
