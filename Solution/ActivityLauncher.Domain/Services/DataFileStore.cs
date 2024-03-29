﻿using ActivityLauncher.Domain.Common.Utils;
using ActivityLauncher.Domain.Services.Declarations;
using Newtonsoft.Json;

namespace ActivityLauncher.Domain.Services;

public class DataFileStore : IFileStorage
{
    private readonly IAppConfiguration _configuration;

    public DataFileStore(IAppConfiguration configuration)
    {
        _configuration = configuration;
    }

    public ResultWithContent<string> ReadFileText(string filePath)
    {
        try
        {
            return new ResultWithContent<string>(File.ReadAllText(filePath));
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return exception.AsResult<string>();
        }
    }

    public ResultWithContent<TResult> ReadFile<TResult>(string filePath)
    {
        try
        {
            var result = ReadFileText(filePath);

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

    public Result WriteTextToFile(string filePath, string content)
    {
        try
        {
            File.WriteAllText(filePath, content);
            return Result.SuccessResult;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return exception.AsResult();
        }
    }

    public Result WriteFile(string filePath, object content)
    {
        try
        {
            return WriteTextToFile(filePath, JsonConvert.SerializeObject(content, Formatting.Indented));
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

    public ResultWithContent<bool> CreateDirectory(string directory)
    {
        var path = GetStorageFilePath(directory);

        var exists = Directory.Exists(path);

        if (!exists)
        {
            try
            {
                Directory.CreateDirectory(path);
                return new ResultWithContent<bool>(true);
            }
            catch (Exception exception)
            {
                return exception.AsResult<bool>();
            }

        }

        return new ResultWithContent<bool>(false);
    }

    public ResultWithContent<bool> DeleteDirectory(string directory)
    {
        var path = GetStorageFilePath(directory);

        var exists = Directory.Exists(path);

        if (exists)
        {
            try
            {
                Directory.Delete(path, true);
                return new ResultWithContent<bool>(true);
            }
            catch (Exception exception)
            {
                return exception.AsResult<bool>();
            }

        }

        return new ResultWithContent<bool>(false);
    }
}
