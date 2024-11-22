using Microsoft.Extensions.DependencyInjection;
using SimpleExec;
using SteamExporter.Dev.Targets;

var serviceProvider = new ServiceCollection()
    .AddTargets()
    .BuildServiceProvider();

var targets = new Bullseye.Targets();
foreach (var target in serviceProvider.GetRequiredService<IEnumerable<ITarget>>())
{
    target.Setup(targets);
}

await targets.RunAndExitAsync(args, messageOnly: ex => ex is ExitCodeException)
    .ConfigureAwait(false);
