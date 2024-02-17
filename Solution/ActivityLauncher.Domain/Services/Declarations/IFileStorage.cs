using ActivityLauncher.Domain.Common.Utils;

namespace ActivityLauncher.Domain.Services.Declarations;

public interface IFileStorage
{
    public Task<ResultWithContent<string>> ReadFileTextAsync(string filePath);

    public Task<ResultWithContent<TResult>> ReadFileAsync<TResult>(string filePath);

    public Task<Result> WriteTextToFileAsync(string filePath, string content);

    public Task<Result> WriteFileAsync(string filePath, object content);

    public Result DeleteFile(string filePath);

    public IEnumerable<string> GetFiles(string path, string searchPattern = "*");

    public string GetStorageFilePath(params string[] relativePaths);

    public ResultWithContent<bool> CreateDirectory(string directory);

    public ResultWithContent<bool> DeleteDirectory(string directory);
}
