using ActivityLauncher.Domain.Interfaces;

namespace ActivityLauncher.Domain.Model.Tasks;

public abstract class BaseTask : ITask
{
    public string? Name { get; set; }

    public bool Enabled { get; set; } = true;

    public abstract string? GetScript();
}
