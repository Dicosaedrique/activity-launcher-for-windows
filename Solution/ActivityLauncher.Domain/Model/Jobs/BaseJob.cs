using ActivityLauncher.Domain.Interfaces;

namespace ActivityLauncher.Domain.Model.Jobs;

public abstract class BaseJob : IJob
{
    public string? Name { get; set; }

    public bool Enabled { get; set; } = true;

    public abstract string? GetScript();
}
