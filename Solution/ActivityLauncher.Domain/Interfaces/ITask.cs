namespace ActivityLauncher.Domain.Interfaces;

public interface ITask : IScriptable
{
    public string? Name { get; set; }

    public bool Enabled { get; set; }
}
