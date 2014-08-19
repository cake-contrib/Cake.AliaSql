// Get arguments passed to the script.
var target = Argument("target", "All");
var configuration = Argument("configuration", "Release");

var version = "0.1.1";

// Define directories.
var projectName = "Cake.AliaSql";
var sourceDir = "./src";
var buildDir = sourceDir + "/" + projectName + "/bin/" + configuration;
var buildResultDir = "./build";
var testResultsDir = buildResultDir + "/tests";
var nugetDir = buildResultDir + "/nuget";
var binDir = buildResultDir + "/bin";

var solutionFile = sourceDir + "/" + projectName + ".sln";

//////////////////////////////////////////////////////////////////////////

Task("Clean")
	.Description("Cleans the build and output directories.")
	.Does(() =>
{
	CleanDirectories(new DirectoryPath[] {
		buildResultDir, binDir, testResultsDir, nugetDir});
});

Task("Restore-NuGet-Packages")
	.Description("Restores all NuGet packages in solution.")
	.IsDependentOn("Clean")
	.Does(() =>
{
	NuGetRestore(solutionFile);
});

Task("Patch-Assembly-Info")
	.Description("Patches the AssemblyInfo files.")
	.IsDependentOn("Restore-NuGet-Packages")
	.Does(() =>
{
	var file = sourceDir + "/SolutionInfo.cs";
	CreateAssemblyInfo(file, new AssemblyInfoSettings {
		Product = projectName,
		Version = version,
		FileVersion = version,
		InformationalVersion = version,
		Copyright = "Copyright (c) Richard Simpson 2014"
	});
});

Task("Build")
	.Description("Builds the Cake source code.")
	.IsDependentOn("Patch-Assembly-Info")
	.Does(() =>
{
	MSBuild(solutionFile, settings =>
		settings.SetConfiguration(configuration)
			.UseToolVersion(MSBuildToolVersion.NET45));
});

Task("Run-Unit-Tests")
	.Description("Runs unit tests.")
	.IsDependentOn("Build")
	.Does(() =>
{
	XUnit("./src/**/bin/" + configuration + "/*.Tests.dll", new XUnitSettings {
		OutputDirectory = testResultsDir,
		XmlReport = true,
		HtmlReport = true
	});
});


Task("Copy-Files")
	.Description("Copy files to the output directory.")
	.IsDependentOn("Run-Unit-Tests")
	.Does(() =>
{
	CopyFileToDirectory(buildDir + "/Cake.AliaSql.dll", binDir);
	CopyFileToDirectory(buildDir + "/Cake.AliaSql.pdb", binDir);
    CopyFileToDirectory(buildDir + "/Cake.Core.dll", binDir);
    CopyFileToDirectory(buildDir + "/Cake.Core.xml", binDir);
    CopyFiles(new FilePath[] { "LICENSE", "README.md" }, binDir);
});

Task("Zip-Files")
	.Description("Zips all files.")
	.IsDependentOn("Copy-Files")
	.Does(() =>
{
	var filename = buildResultDir + "/Cake_AliaSql-bin-v" + version + ".zip";
	Zip(binDir, filename);
});

Task("Create-NuGet-Package")
	.Description("Creates the NuGet package.")
	.IsDependentOn("Copy-Files")
	.Does(() =>
{
	NuGetPack("./Cake.AliaSql.nuspec", new NuGetPackSettings {
		Version = version,
        BasePath = binDir,
        OutputDirectory = nugetDir,        
        Symbols = false,
        NoPackageAnalysis = true
	});
});

Task("Package")
	.Description("Zips package.")
	.IsDependentOn("Zip-Files")
	.IsDependentOn("Create-NuGet-Package");

Task("All")
	.Description("Final target.")
	.IsDependentOn("Package");

//////////////////////////////////////////////////////////////////////////

RunTarget(target);