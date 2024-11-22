namespace SteamExporter.Dev.Targets.Build;

internal sealed class DotnetTarget : ITarget
{
    public void Setup(Bullseye.Targets targets)
        => targets.Add(
            BuildTargets.Dotnet,
            "Builds the solution into a set of output binaries.",
            dependsOn: [BuildTargets.Clean],
            async () => await DotnetCli.Run("build", "/warnaserror")
                .ConfigureAwait(false));
}
