// Enviroment
var isRunningOnAppVeyor = AppVeyor.IsRunningOnAppVeyor;
var isRunningOnWindows = IsRunningOnWindows();

// Arguments.
var target = Argument("target", "Default");
var configuration = "Release";

// Define directories.
var solutionFile = new FilePath("Xamarin.Android.AVLoadingIndicatorViewBinding.sln");
var artifactsDirectory = new DirectoryPath("artifacts");

// Versioning.
var version = EnvironmentVariable ("APPVEYOR_BUILD_VERSION") ?? Argument("version", "9.9.9");

Setup((context) =>
{
	Information("AppVeyor: {0}", isRunningOnAppVeyor);
	Information ("Running on Windows: {0}", isRunningOnWindows);
	Information("Configuration: {0}", configuration);
});

Task("Clean")
	.Does(() =>
	{	
		CleanDirectory(artifactsDirectory);

		DotNetBuild(solutionFile, settings => settings
				.SetConfiguration(configuration)
				.WithTarget("Clean")
				.SetVerbosity(Verbosity.Minimal));
	});

Task("Restore")
	.Does(() => 
	{
		NuGetRestore(solutionFile);
	});

Task("Build")
	.IsDependentOn("Clean")
	.IsDependentOn("Restore")
	.Does(() =>  
	{	
		DotNetBuild(solutionFile, settings => settings
					.SetConfiguration(configuration)
					.WithTarget("Build")
					.SetVerbosity(Verbosity.Minimal));
	});

Task ("NuGet")
	.IsDependentOn ("Build")
	.Does (() =>
	{
		NuGetPack ("./nuspec/Xamarin.Android.AVLoadingIndicatorView.nuspec", 
			new NuGetPackSettings 
				{ 
					Version = version,
					Verbosity = NuGetVerbosity.Detailed,
					OutputDirectory = artifactsDirectory,
					BasePath = "./",
					ArgumentCustomization = args => args.Append("-NoDefaultExcludes")		
				});	
	});

Task("Default")
	.IsDependentOn("NuGet")
	.Does(() => {});

RunTarget(target);