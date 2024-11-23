using System.Text;
using static SimpleExec.Command;

namespace SteamExporter.Dev.Targets.Build;

internal class CoverageTarget : ITarget
{
    private static readonly CompositeFormat AssemblyPatternFormat = CompositeFormat.Parse("{0}SteamExporter.**{1}");

    public void Setup(Bullseye.Targets targets)
        => targets.Add(
            BuildTargets.Coverage,
            "Generates code coverage reports for test results.",
            dependsOn: [BuildTargets.Test],
            Execute);

    private static async Task Execute()
    {
        string[] assemblyFilterPatterns =
        [
            string.Format(null, AssemblyPatternFormat, "+", string.Empty),
            string.Format(null, AssemblyPatternFormat, "-", "Dev"),
            string.Format(null, AssemblyPatternFormat, "-", "Facts"),
            string.Format(null, AssemblyPatternFormat, "-", "Tests"),
        ];

        var commitId = await GitCli.GetCommitId()
            .ConfigureAwait(false);

        string[] arguments =
        [
            "reportgenerator",
            $"-assemblyFilters:{string.Join(';', assemblyFilterPatterns)}",
            $"-reports:{ArtifactPaths.TestResults}/*/*/coverage.cobertura.xml",
            $"-targetdir:{ArtifactPaths.Tests}/coverage",
            $"-tag:{commitId}",
            "-reporttypes:Html",
            "-title:\"Steam Exporter\"",
        ];

        await RunAsync("dotnet", string.Join(' ', arguments))
            .ConfigureAwait(false);
    }
}
