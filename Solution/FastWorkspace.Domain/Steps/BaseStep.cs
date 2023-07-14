using FastWorkspace.Domain.Interfaces;

namespace FastWorkspace.Domain.Steps;

public abstract class BaseStep : IStep
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int Sequence { get; set; }

    public bool Enabled { get; set; } = true;

    public abstract string GetScript();
}
