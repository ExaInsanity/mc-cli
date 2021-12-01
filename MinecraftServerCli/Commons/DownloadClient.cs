namespace MinecraftServerCli.Commons;

using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

public class DownloadClient
{
    private HttpClient httpClient;

    public DownloadClient(HttpClient client)
    {
        this.httpClient = client;
    }

    public async Task<String> GetDownloadUrl(String version)
    {
        PrimeLocalCache().GetAwaiter().GetResult();

        JToken jToken = JToken.Parse(new StreamReader("./local-lm-cache.json").ReadToEnd());

        LaunchermetaVersion[] versions = ((JArray)jToken.SelectToken("versions")!).ToObject<LaunchermetaVersion[]>()!;

        if(!versions.Any(xm => xm.Id == version))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Specified version not found. If you are certain it exists, delete the cache file.");
            Console.ResetColor();
        }

        // get the version.json

        using HttpResponseMessage response = await httpClient.GetAsync(
            versions.First(xm => xm.Id == version).Url);

        jToken = JToken.Parse(await response.Content.ReadAsStringAsync());

        return jToken.SelectToken("downloads.server.url")!.Value<String>()!;
    }

    private async Task PrimeLocalCache()
    {
        if(File.Exists("./local-lm-cache.json") && 
            File.GetLastWriteTimeUtc("./local-lm-cache.json") >= (DateTime.UtcNow + TimeSpan.FromMinutes(15)))
        {
            return;
        }

        if(!File.Exists("./local-lm-cache.json"))
        {
            File.Create("./local-lm-cache.json");
        }

        using HttpResponseMessage response = await httpClient.GetAsync(Program.ManifestLocation);
        using Stream ms = await response.Content.ReadAsStreamAsync();
        using StreamWriter writer = new("./local-lm-cache.json", append: false);

        ms.Seek(0, SeekOrigin.Begin);
        ms.CopyTo(writer.BaseStream);

        writer.Flush();
        writer.Close();
    }

    internal async void ForcePrimeLocalCache()
    {
        if(!File.Exists("./local-lm-cache.json"))
        {
            File.Create("./local-lm-cache.json");
        }

        using HttpResponseMessage response = await httpClient.GetAsync(Program.ManifestLocation);
        using Stream ms = await response.Content.ReadAsStreamAsync();
        using StreamWriter writer = new("./local-lm-cache.json", append: false);

        ms.Seek(0, SeekOrigin.Begin);
        ms.CopyTo(writer.BaseStream);

        writer.Flush();
    }
}
