namespace SteamExporter.Dev.Targets.Build;

internal sealed class PublishTarget : ITarget
{
    private static readonly PublishProject[] Projects =
    [
        new("cli", "src/Cli"),
    ];

    public void Setup(Bullseye.Targets targets)
        => targets.Add(
            BuildTargets.Publish,
            "Publishes projects to a directory as executables for release.",
            dependsOn: [BuildTargets.Dotnet],
            forEach: Projects,
            Execute);

    private static async Task Execute(PublishProject project)
    {
        var executablePath = string.Format(default, ArtifactPaths.ExectuableFormat, project.Name);
        await DotnetCli
            .Run(
                "publish",
                project.Path,
                $"--output {executablePath}",
                "--no-build")
            .ConfigureAwait(false);
    }
}
