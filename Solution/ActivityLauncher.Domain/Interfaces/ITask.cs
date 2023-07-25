namespace ActivityLauncher.Domain.Interfaces;

public interface ITask : IScriptable
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public DateTime CreationDate { get; }

    public bool IsValid();
}
