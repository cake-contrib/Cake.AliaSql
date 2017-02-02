```cake
#tool "AliaSQL"
#addin "Cake.AliaSql"

...

Task("RebuildDatabase")
	.Does(() => {
		AliaSql("Rebuild", new AliaSqlSettings
		{
			ToolPath = "./tools/AliaSQL/tools/AliaSQL.exe",
			ConnectionString = ".\\sqldb",
			DatabaseName = "TestDatabase",
			ScriptsFolder = "./src/Database/Scripts",
		});
	});
```