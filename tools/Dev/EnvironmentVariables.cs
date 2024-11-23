namespace SteamExporter.Dev;

internal static class EnvironmentVariables
{
    public static readonly DevEnvironmentVariable LocalBuild = $"{Prefix}_LOCALBUILD";

    private const string Prefix = "STEAMEXPORTER";
}
