﻿using System.Text;
using ActivityLauncher.Domain.Interfaces;

namespace ActivityLauncher.Domain.Model.Tasks;

public class ProgramTask : BaseTask, ICloneable<ProgramTask>
{
    public string ProgramName { get; set; } = string.Empty;

    public string ProgramFilePath { get; set; } = string.Empty;

    public string ArgumentList { get; set; } = string.Empty;

    public override string? GetScript()
    {
        if (!string.IsNullOrWhiteSpace(ProgramName))
        {
            return $"Start-Process {ProgramName}";
        }

        if (!string.IsNullOrWhiteSpace(ProgramFilePath))
        {
            var builder = new StringBuilder();

            builder.Append($"Start-Process -FilePath \"{ProgramFilePath}\"");

            if (!string.IsNullOrWhiteSpace(ArgumentList))
            {
                builder.Append($" -ArgumentList \"{ArgumentList}\"");
            }

            return builder.ToString();
        }
        else
        {
            return null;
        }
    }

    public ProgramTask Clone()
    {
        return new ProgramTask()
        {
            Name = Name,
            ProgramName = ProgramName,
            Enabled = Enabled,
            ProgramFilePath = ProgramFilePath,
            ArgumentList = ArgumentList,
        };
    }
}