var configuration = Argument("configuration", "Debug");
var target = Argument("target", "default");
var outputDirectory = Argument("output", "./out");
var project = Argument("project", "JuanIpCamera.ConsoleApp");

// clean
var cleanSettings = new DotNetCoreCleanSettings
{
    Configuration = configuration
};

Task("clean")
    .Does(() => 
    {
        EnsureDirectoryExists(outputDirectory);
        CleanDirectory(outputDirectory);
        DotNetCoreClean(project, cleanSettings);
    });

// build
var buildSettings = new DotNetCoreBuildSettings
{
    Configuration = configuration
};

Task("build")
    .Does(() => 
    {
        DotNetCoreBuild(project, buildSettings);
    });

// publish
var publishSettings = new DotNetCorePublishSettings
{
    Configuration = configuration,
    OutputDirectory = outputDirectory,
    MSBuildSettings = new DotNetCoreMSBuildSettings
    {
        TreatAllWarningsAs = MSBuildTreatAllWarningsAs.Error
    }
};

Task("publish")
    .Does(() => 
    {
        DotNetCorePublish(project, publishSettings);
    });

// default
Task("default")
    .IsDependentOn("publish");

// run
RunTarget(target);