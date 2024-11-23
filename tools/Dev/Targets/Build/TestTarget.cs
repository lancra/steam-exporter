namespace SteamExporter.Dev.Targets.Build;

internal class TestTarget : ITarget
{
    private static readonly TestSuite[] Suites =
    [
        new(
            BuildTargets.TestUnit,
            "Tests individual components of the application.",
            [new("facts", "tests/Facts")]),
    ];

    public void Setup(Bullseye.Targets targets)
    {
        foreach (var suite in Suites)
        {
            SetupSuiteTarget(targets, suite);
        }

        targets.Add(
            BuildTargets.Test,
            "Executes automated test suites.",
            dependsOn: Suites.Select(suite => suite.Name)
                .ToArray());
    }

    private static void SetupSuiteTarget(Bullseye.Targets targets, TestSuite suite)
        => targets.Add(
            suite.Name,
            suite.Description,
            dependsOn: [BuildTargets.Dotnet],
            forEach: suite.Projects,
            async project => await DotnetCli
                .Run(
                    $"test {project.Path}",
                    "--no-build",
                    "--collect \"XPlat Code Coverage\"",
                    "--logger trx",
                    $"--results-directory {string.Format(null, ArtifactPaths.TestResultFormat, project.Name)}")
                .ConfigureAwait(false));
}
