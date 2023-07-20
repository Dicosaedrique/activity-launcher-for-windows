namespace ActivityLauncher.Domain.Interfaces;

public interface IJob : IScriptable
{
    public string? Name { get; set; }

    public bool Enabled { get; set; }
}
