using ActivityLauncher.Domain.Common.Utils;

namespace ActivityLauncher.Domain.Services.Declarations;

public interface IFileStorage
{
    public ResultWithContent<string> ReadFileText(string filePath);

    public ResultWithContent<TResult> ReadFile<TResult>(string filePath);

    public Result WriteTextToFile(string filePath, string content);

    public Result WriteFile(string filePath, object content);

    public Result DeleteFile(string filePath);

    public IEnumerable<string> GetFiles(string path, string searchPattern = "*");

    public string GetStorageFilePath(params string[] relativePaths);

    public ResultWithContent<bool> CreateDirectory(string directory);

    public ResultWithContent<bool> DeleteDirectory(string directory);
}
