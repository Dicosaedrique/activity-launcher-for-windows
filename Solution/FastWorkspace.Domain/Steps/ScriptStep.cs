namespace FastWorkspace.Domain.Steps;

public class ScriptStep : BaseStep
{
    public string Script { get; set; } = string.Empty;

    public override string GetScript()
    {
        return Script;
    }
}
