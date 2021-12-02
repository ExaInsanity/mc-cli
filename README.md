# minecraft server cli

a very simple cli application to sownload the server software from mojangs servers. if you dont like this, cry about it (unless you're mojang in which case this was for legal reasons not made by me)

command reference can be found in [this file](./commands.md)

the `startup-scripts.json` file is pretty self-explanatory, another thing worth noting is that the tool automatically replaces any `\n`s with the newline character. you can use that to execute multiple commands.

okay bye now

---

oh right, build instructions:

clone the repository and run `dotnet build -c release`. your files will be in `MinecraftServerCli/bin/Release/net6.0`