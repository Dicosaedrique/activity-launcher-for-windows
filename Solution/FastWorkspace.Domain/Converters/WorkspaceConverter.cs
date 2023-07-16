using FastWorkspace.Domain.Interfaces;
using FastWorkspace.Domain.Jobs;
using Newtonsoft.Json;

namespace FastWorkspace.Domain.Converters;

public class WorkspaceConverter : JsonConverter<Workspace>
{
    public override Workspace? ReadJson(JsonReader reader, Type objectType, Workspace? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        return serializer.Deserialize<WorkspaceSerializable>(reader).ToWorkspace();
    }

    public override void WriteJson(JsonWriter writer, Workspace? value, JsonSerializer serializer)
    {
        if (value == null) throw new ArgumentNullException(nameof(value));
        serializer.Serialize(writer, new WorkspaceSerializable(value));
    }

    private struct WorkspaceSerializable
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public List<BrowserTabsJob> BrowserTabsJobs { get; set; } = new();

        public List<ExplorerFolderJob> ExplorerFolderJobs { get; set; } = new();

        public List<ProgramFileJob> ProgramFileJobs { get; set; } = new();

        public List<ProgramJob> ProgramJobs { get; set; } = new();

        public List<ScriptJob> ScriptJobs { get; set; } = new();

        public List<TerminalJob> TerminalJobs { get; set; } = new();

        public List<TerminalTabsJob> TerminalTabsJobs { get; set; } = new();

        public List<VSCodeJob> VSCodeJobs { get; set; } = new();

        public List<VSSolutionJob> VSSolutionJobs { get; set; } = new();

        public WorkspaceSerializable(Workspace workspace)
        {
            Id = workspace.Id;
            Name = workspace.Name;
            Description = workspace.Description;
            CreationDate = workspace.CreationDate;
            LastModifiedDate = workspace.LastModifiedDate;
            InitializeJobs(workspace.Jobs);
        }

        public Workspace ToWorkspace()
        {
            return new Workspace(Id, Name, Description, CreationDate, LastModifiedDate, GetJobs());
        }

        private void InitializeJobs(IEnumerable<IJob> jobs)
        {
            foreach (var job in jobs)
            {
                switch (job)
                {
                    case BrowserTabsJob browserTabsJob:
                        BrowserTabsJobs.Add(browserTabsJob);
                        break;
                    case ExplorerFolderJob explorerFolderJob:
                        ExplorerFolderJobs.Add(explorerFolderJob);
                        break;
                    case ProgramFileJob programFileJob:
                        ProgramFileJobs.Add(programFileJob);
                        break;
                    case ProgramJob programJob:
                        ProgramJobs.Add(programJob);
                        break;
                    case ScriptJob scriptJob:
                        ScriptJobs.Add(scriptJob);
                        break;
                    case TerminalJob terminalJob:
                        TerminalJobs.Add(terminalJob);
                        break;
                    case TerminalTabsJob terminalTabsJob:
                        TerminalTabsJobs.Add(terminalTabsJob);
                        break;
                    case VSCodeJob vSCodeJob:
                        VSCodeJobs.Add(vSCodeJob);
                        break;
                    case VSSolutionJob vSSolutionJob:
                        VSSolutionJobs.Add(vSSolutionJob);
                        break;
                    default:
                        throw new ArgumentException($"Couldn't identify job type \"{job.GetType().Name}\". Please consider adding it to the list of job that are processed by WorkspaceSerializable.InitializeJobs.");
                }
            }
        }

        private IEnumerable<IJob> GetJobs()
        {
            var jobs = new List<IJob>();

            jobs.AddRange(BrowserTabsJobs);
            jobs.AddRange(ExplorerFolderJobs);
            jobs.AddRange(ProgramFileJobs);
            jobs.AddRange(ProgramJobs);
            jobs.AddRange(ScriptJobs);
            jobs.AddRange(TerminalJobs);
            jobs.AddRange(TerminalTabsJobs);
            jobs.AddRange(VSCodeJobs);
            jobs.AddRange(VSSolutionJobs);

            return jobs;
        }
    }
}
