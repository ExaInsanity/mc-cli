namespace MinecraftServerCli.Commands;

using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

public class InstallCommandHandler
{
    public void RunCommand(String[] args)
    {
        RunCommandAsync(args).GetAwaiter().GetResult();
    }

    private async Task RunCommandAsync(String[] args)
    {
        String downloadUrl = await Program.DownloadClient.GetDownloadUrl(args[0]);
        String serverJarPath = Path.Combine(args[1], "server.jar");

        if(!Directory.Exists(args[1]))
        {
            Directory.CreateDirectory(args[1]);
        }

        if(!File.Exists(serverJarPath))
        {
            File.Create(serverJarPath).Close();
        }

        using HttpResponseMessage response = await Program.HttpClient.GetAsync(downloadUrl);
        using Stream ms = await response.Content.ReadAsStreamAsync();
        using StreamWriter writer = new(serverJarPath);

        ms.Seek(0, SeekOrigin.Begin);
        ms.CopyTo(writer.BaseStream);
        writer.Flush();

        if(args.Length < 3)
        {
            return;
        }

        if(args[2] == "generate")
        {
            new GenerateCommandHandler().RunCommand(args[3..], args[1]);
        }
    }
}
