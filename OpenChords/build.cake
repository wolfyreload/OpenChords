string solutionName = "OpenChords.sln";

var configuration = Argument("configuration_arg", "Release");
	
// ARGUMENTS
var target = Argument("target", "Default");

// TASKS
Task("Restore-NuGet-Packages")
    .Does(() =>
{
    NuGetRestore(solutionName);
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    MSBuild(solutionName, settings =>
        settings.SetConfiguration(configuration)
            .SetVerbosity(Verbosity.Minimal)
            .UseToolVersion(MSBuildToolVersion.VS2022)
    );
});

// TASK TARGETS
Task("Default")
    .IsDependentOn("Build");

// EXECUTION
RunTarget(target);