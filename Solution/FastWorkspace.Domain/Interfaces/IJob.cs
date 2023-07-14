namespace FastWorkspace.Domain.Interfaces;

public interface IJob : IScriptable
{
    public string Name { get; }

    public string? Description { get; }

    public int Sequence { get; }
}
