namespace FastWorkspace.Domain.Interfaces;

public interface IJob : IScriptable, ICloneable<IJob>
{
    public string DisplayName { get; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int Sequence { get; set; }

    public bool Enabled { get; set; }
}
