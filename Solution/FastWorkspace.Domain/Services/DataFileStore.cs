using FastWorkspace.Domain.Common.Utils;
using FastWorkspace.Domain.Services.Declarations;
using Newtonsoft.Json;

namespace FastWorkspace.Domain.Services;

public class DataFileStore : IFileStorage
{
    private readonly IAppConfiguration _configuration;

    public DataFileStore(IAppConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<ResultWithContent<string>> ReadFileTextAsync(string filePath)
    {
        try
        {
            using Stream fileStream = File.OpenRead(filePath);
            using StreamReader reader = new StreamReader(fileStream);

            return new ResultWithContent<string>(await reader.ReadToEndAsync());
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return exception.AsResult<string>();
        }
    }

    public async Task<ResultWithContent<TResult>> ReadFileAsync<TResult>(string filePath)
    {
        try
        {
            var result = await ReadFileTextAsync(filePath);

            if (result.Success)
            {
                return new ResultWithContent<TResult>(JsonConvert.DeserializeObject<TResult>(result.Content!)!);
            }
            else
            {
                return new ResultWithContent<TResult>(result.Exception!);
            }

        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return exception.AsResult<TResult>();
        }
    }

    public async Task<Result> WriteTextToFileAsync(string filePath, string content)
    {
        try
        {
            using var fileStream = File.OpenWrite(filePath);
            using var writter = new StreamWriter(fileStream);

            await writter.WriteAsync(content);

            return Result.SuccessResult;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return exception.AsResult();
        }
    }

    public async Task<Result> WriteFileAsync(string filePath, object content)
    {
        try
        {
            return await WriteTextToFileAsync(filePath, JsonConvert.SerializeObject(content, Formatting.Indented));
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return exception.AsResult();
        }
    }

    public Result DeleteFile(string filePath)
    {
        try
        {
            File.Delete(filePath);
            return Result.SuccessResult;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return exception.AsResult();
        }
    }

    public IEnumerable<string> GetFiles(string path, string searchPattern = "*")
    {
        return Directory.GetFiles(path, searchPattern);
    }

    public string GetStorageFilePath(params string[] relativePaths)
    {
        var paths = new List<string>() { _configuration.GetFileStorageDirectoryPath()! };
        paths.AddRange(relativePaths);

        return Path.Combine(paths.ToArray());
    }

    public void CreateDirectoryIfNotExists(string directory)
    {
        var path = GetStorageFilePath(directory);

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
}
