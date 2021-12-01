namespace MinecraftServerCli.Commands;

using System;
using System.IO;
using System.Linq;

public class GenerateCommandHandler
{
    public void RunCommand(String[] args)
    {
        if(args[0] == "-w" || args[0] == "--windows")
        {
            GenerateWindowsScript(args[1]);
            return;
        }

        if(args[0] == "-l" || args[0] == "--linux")
        {
            GenerateLinuxScript(args[1]);
            return;
        }

        if(args[0] == "-wl")
        {
            GenerateWindowsScript(args[1]);
            GenerateLinuxScript(args[1]);
            return;
        }
    }

    public void RunCommand(String[] args, String directory)
    {
        RunCommand(args.Append(directory).ToArray());
    }

    private void GenerateWindowsScript(String directory)
    {
        StreamWriter writer = new(File.Create(Path.Combine(directory, "start.bat")));

        writer.WriteLine("java -Xmx2560M -jar server.jar");

        writer.Close();
    }

    private void GenerateLinuxScript(String directory)
    {
        StreamWriter writer = new(File.Create(Path.Combine(directory, "start.sh")));

        writer.WriteLine("java -Xmx2560M -jar server.jar");

        writer.Close();
    }
}
