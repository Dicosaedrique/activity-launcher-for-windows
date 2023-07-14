using FastWorkspace.Domain.Interfaces;
using System.Text;

namespace FastWorkspace.Domain.Workspace;

public class Workspace : IScriptable
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool Enabled { get; set; } = true;

    public DateTime CreationDate { get; init; } = DateTime.Now;

    public DateTime LastModifiedDate { get; set; }

    private readonly List<IStep> _steps = new();

    public void AddStep(IStep step)
    {
        _steps.Add(step);
    }

    public bool RemoveStep(IStep step)
    {
        return _steps.Remove(step);
    }

    public void ClearSteps()
    {
        _steps.Clear();
    }

    public string GetScript()
    {
        var builder = new StringBuilder();

        builder.Append(GetScriptHeader());

        foreach (var step in _steps)
        {
            builder.Append(GetStepScript(step));
        }

        builder.Append(GetScriptFooter());

        return builder.ToString();
    }

    private string GetScriptHeader()
    {
        const string template = "##################################################################################################\n# Workspace: {0}\n# Description: {1}\n#\n# Script generated with love by Fast Workspace <3\n##################################################################################################\n\n#region STEPS\n\n";
        return string.Format(template, Name, Description);
    }

    private string GetStepScript(IStep step)
    {
        const string template = "###################################\n# Step {0} - {1} ({2})\n\n{5}\n\n\n";
        return string.Format(template, step.Sequence, step.Name, step.Description, step.GetScript());
    }

    private string GetScriptFooter()
    {
        const string template = "#endregion STEPS\n\n# Script generated the {0:d} at {0:t}\n\n# Thanks for using Fast Workspace!";
        return string.Format(template, DateTime.Now);
    }
}
