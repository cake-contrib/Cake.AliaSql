Cake.AliaSql [![cake_aliasql MyGet Build Status](https://www.myget.org/BuildSource/Badge/cake_aliasql?identifier=ea3bdebc-3992-40c0-99c0-dc48f0af2641)](https://www.myget.org/feed/cake_aliasql/package/Cake.AliaSql)
============

AliaSql Script Alias for Cake.



To Install:
```cmd
if not exist tools\Cake.AliaSql\Cake.AliaSql.dll ( 
	echo Installing Cake.AliaSql...
	tools\nuget\nuget.exe install Cake.AliaSql -OutputDirectory tools -ExcludeVersion -NonInteractive -NoCache
	echo.
)
```

To Grab AliaSQL:
```cmd
if not exist tools\AliaSQL\tools\AliaSQL.exe ( 
    echo Installing AliaSQL...
    tools\nuget\nuget.exe install AliaSQL -OutputDirectory tools -ExcludeVersion -NonInteractive -NoCache
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
			ToolPath = "./tools/AliaSQL/tools/AliaSQL.exe",
			ConnectionString = ".\sqldb",
			DatabaseName = "TestDatabase",
			ScriptsFolder = "./src/Database/Scripts",
		});
	});
```

Nightly Packages
=================
You can find CI builds at:
https://www.myget.org/feed/cake_aliasql/package/Cake.AliaSql

_Warning:_ These builds maybe be broken or unstable.
