namespace MinecraftServerCli;

using System;
using System.Net.Http;
using System.Runtime.CompilerServices;

using MinecraftServerCli.Commands;
using MinecraftServerCli.Commons;

public static class Program
{
    public static DownloadClient DownloadClient { get; private set; }
    public static HttpClient HttpClient { get; private set; }
    public static String Version => "0.0.1";

    public const String ManifestLocation = "https://launchermeta.mojang.com/mc/game/version_manifest.json";

    static Program()
    {
        HttpClient = new();
        DownloadClient = new(HttpClient);
    }

    public static void Main(String[] args)
    {
        if(args.Length == 0)
        {
            PrintHelp();
            return;
        }

        switch(args[0].ToLower())
        {
            case "-v":
            case "--version":
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"---\nTool version {Version}, on .NET version {Environment.Version}\n---");
                Console.ResetColor();
                break;
            case "install":
                new InstallCommandHandler().RunCommand(args[1..]);
                break;
            case "refresh":
                DownloadClient.ForcePrimeLocalCache();
                break;
            case "generate":
                new GenerateCommandHandler().RunCommand(args[1..]);
                break;
            case "link":
                Console.WriteLine(DownloadClient.GetDownloadUrl(args[1]).GetAwaiter().GetResult());
                break;
            case "-h":
            case "-?":
            case "--help":
            default:
                PrintHelp();
                break;
        }
    }

    private static void PrintHelp()
    {
        Console.WriteLine(@"
This tool allows you to download the official minecraft server software. It allows you to generate startup scripts or get direct download links, all with one command.

See https://github.com/exainsanity/mc-cli for the full command reference.

Options:
    -v, --version: Prints the tool version and the dotnet runtime version to console.

Subcommands:
    install         Downloads the minecraft server software.
    refresh         Forcibly refreshes the local tool cache.
    generate        Generates startup scripts either from a specified template or based on default values.
    link            Prints the direct download link into console.

All of the subcommands support further configuration, which is documented in the repository.
");
    }
}
