using ActivityLauncher.Domain.Enums;
using ActivityLauncher.Domain.Interfaces;

namespace ActivityLauncher.Domain.Model.Tasks;

public abstract class BaseTask : ITask
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string? Name { get; set; }

    public DateTime CreationDate { get; set; } = DateTime.Now;

    public abstract TaskType TaskType { get; }

    public string? GetScript()
    {
        return IsValid() ? GetValidScript() : null;
    }

    protected abstract string GetValidScript();

    public abstract bool IsValid();
}
