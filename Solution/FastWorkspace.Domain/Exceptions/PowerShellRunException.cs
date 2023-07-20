using System.Management.Automation;

namespace FastWorkspace.Domain.Exceptions;

public class PowerShellRunException : Exception
{
    public readonly IEnumerable<ErrorRecord> ErrorRecords = new List<ErrorRecord>();

    public PowerShellRunException(string message) : base(message) { }

    public PowerShellRunException(IEnumerable<ErrorRecord> errorRecords)
        : base()
    {
        ErrorRecords = errorRecords;
    }

    public PowerShellRunException(string message, IEnumerable<ErrorRecord> errorRecords)
        : base(message)
    {
        ErrorRecords = errorRecords;
    }
}
