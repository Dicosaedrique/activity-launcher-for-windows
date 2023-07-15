using Microsoft.Fast.Components.FluentUI.Infrastructure;

namespace FastWorkspace.Client.Common.Services;

public class FileBasedStaticAssetService : IStaticAssetService
{
    public async Task<string?> GetAsync(string assetUrl, bool useCache = false)
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync($"wwwroot/{assetUrl}");
        using var reader = new StreamReader(stream);
        return await reader.ReadToEndAsync();
    }
}