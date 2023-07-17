using FastWorkspace.Domain.Interfaces;
using Newtonsoft.Json;

namespace FastWorkspace.Domain.Jobs;

public abstract class BaseJob : IJob
{
    [JsonIgnore]
    public string DisplayName => string.IsNullOrWhiteSpace(Name) ? GetDefaultName() : Name;

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int Sequence { get; set; }

    public bool Enabled { get; set; } = true;

    public abstract string GetScript();

    public abstract string GetDefaultName();

    public abstract IJob Clone();
}
