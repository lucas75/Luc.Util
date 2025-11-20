using System;

namespace Lwx.Archetypes.Common;

[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple = false)]
public sealed class BaseWorkerAttribute : Attribute
{
    public string? HealthEndpoint { get; init; }
    public string? ReadinessEndpoint { get; init; }
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class QueueTransformerAttribute : Attribute
{
    public string InputConnectionStringKey { get; }
    public string InputEventHubNameKey { get; }
    public string OutputConnectionStringKey { get; }
    public string OutputEventHubNameKey { get; }

    public QueueTransformerAttribute(string inputConnectionStringKey, string inputEventHubNameKey, string outputConnectionStringKey, string outputEventHubNameKey)
    {
        InputConnectionStringKey = inputConnectionStringKey;
        InputEventHubNameKey = inputEventHubNameKey;
        OutputConnectionStringKey = outputConnectionStringKey;
        OutputEventHubNameKey = outputEventHubNameKey;
    }
}
