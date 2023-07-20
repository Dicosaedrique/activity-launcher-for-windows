using FastWorkspace.Domain.Interfaces;

namespace FastWorkspace.Domain.Model.Jobs;

public abstract class BaseJob : IJob
{
    public string? Name { get; set; }

    public bool Enabled { get; set; } = true;

    public abstract string? GetScript();
}
