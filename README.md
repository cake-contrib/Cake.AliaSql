Cake.AliaSql
============

AliaSql Script Alias for Cake.

[![cake_aliasql MyGet Build Status](https://www.myget.org/BuildSource/Badge/cake_aliasql?identifier=9da3a1ac-97c3-49f3-bf99-7cf3a8c2034a)](https://www.myget.org/)

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
