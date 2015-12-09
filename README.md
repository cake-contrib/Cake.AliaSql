Cake.AliaSql [![cake_aliasql MyGet Build Status](https://www.myget.org/BuildSource/Badge/cake_aliasql?identifier=ea3bdebc-3992-40c0-99c0-dc48f0af2641)](https://www.myget.org/feed/cake_aliasql/package/Cake.AliaSql)
============

AliaSql Script Alias for [Cake](https://github.com/cake-build/cake) build system.

### Example usage:
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

Nightly Packages
=================
You can find CI builds at:
https://www.myget.org/feed/cake_aliasql/package/Cake.AliaSql

_Warning:_ These builds maybe be broken or unstable.
