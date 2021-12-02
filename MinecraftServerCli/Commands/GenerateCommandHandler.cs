namespace MinecraftServerCli.Commands;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using MinecraftServerCli.Commons;

using Newtonsoft.Json;

public class GenerateCommandHandler
{
    public void RunCommand(String[] args)
    {
        if(args.Length == 2)
        {
            if(args[0] == "-w" || args[0] == "--windows")
            {
                GenerateWindowsScript(args[1], "default");
                return;
            }

            if(args[0] == "-l" || args[0] == "--linux")
            {
                GenerateLinuxScript(args[1], "default");
                return;
            }

            if(args[0] == "-wl")
            {
                GenerateWindowsScript(args[1], "default");
                GenerateLinuxScript(args[1], "default");
                return;
            }
        }
        else
        {
            if(args[0] == "-w" || args[0] == "--windows")
            {
                GenerateWindowsScript(args[2], args[1]);
                return;
            }

            if(args[0] == "-l" || args[0] == "--linux")
            {
                GenerateLinuxScript(args[2], args[1]);
                return;
            }

            if(args[0] == "-wl")
            {
                GenerateWindowsScript(args[2], args[1]);
                GenerateLinuxScript(args[2], args[1]);
                return;
            }
        }
    }

    public void RunCommand(String[] args, String directory)
    {
        RunCommand(args.Append(directory).ToArray());
    }

    private void GenerateWindowsScript(String directory, String id)
    {
        List<StartupScript> startupScripts = JsonConvert.DeserializeObject<List<StartupScript>>(
            new StreamReader("./startup-scripts.json").ReadToEnd())!;

        StreamWriter writer = new(File.Create(Path.Combine(directory, "start.bat")));

        writer.WriteLine(startupScripts.First(xm => xm.Id == id).Windows
            .Replace("\\n", "\n"));

        writer.Close();
    }

    private void GenerateLinuxScript(String directory, String id)
    {
        List<StartupScript> startupScripts = JsonConvert.DeserializeObject<List<StartupScript>>(
            new StreamReader("./startup-scripts.json").ReadToEnd())!;

        StreamWriter writer = new(File.Create(Path.Combine(directory, "start.sh")));

        writer.WriteLine(startupScripts.First(xm => xm.Id == id).Linux
            .Replace("\\n", "\n"));

        writer.Close();
    }
}
