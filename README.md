Cake.AliaSql
============

AliaSql Script Alias for Cake.

Example:
```C#
Task("RebuildDatabase")
	.Does(() => {
		AliaSql("Rebuild", new AliaSqlSettings
		{
			ToolPath = "./tools/AliaSql/AliaSql.exe",
			ConnectionString = ".\sqldb",
			DatabaseName = "TestDatabase",
			ScriptsFolder = "./src/Database/Scripts",
		});
	});
```
