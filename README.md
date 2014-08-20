Cake.AliaSql
============

AliaSql Script Alias for Cake.

[![cake_aliasql MyGet Build Status](https://www.myget.org/BuildSource/Badge/cake_aliasql?identifier=ea3bdebc-3992-40c0-99c0-dc48f0af2641)](https://www.myget.org/)

To Install:
```cmd
if not exist tools\Cake.AliaSql\Cake.AliaSql.dll ( 
	echo Installing Cake.AliaSql...
	tools\nuget\nuget.exe install Cake.AliaSql -OutputDirectory tools -ExcludeVersion -NonInteractive -NoCache
	echo.
)
```

Example:
```C#
#r "tools/Cake.AliaSql/Cake.AliaSql.dll"

...

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
