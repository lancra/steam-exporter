namespace SteamExporter.Dev;

internal sealed class DevEnvironmentVariable
{
    private static readonly string[] TrueEnvironmentVariableValues =
    [
        "1",
        "on",
        "true",
        "yes",
    ];

    private bool _hydratedValue;
    private string? _value;

    private DevEnvironmentVariable(string name) => Name = name;

    public string Name { get; }

    public string? Value
    {
        get
        {
            if (!_hydratedValue)
            {
                _value = Environment.GetEnvironmentVariable(Name);
                _hydratedValue = true;
            }

            return _value;
        }
    }

    public static implicit operator DevEnvironmentVariable(string name) => new(name);

    public bool AsFlag() => Value is not null && TrueEnvironmentVariableValues.Contains(Value, StringComparer.OrdinalIgnoreCase);
}
