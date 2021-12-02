# command interface reference

- just `mc-server`: help message. the help isnt particularly detailed so you should continue reading here
- `-h, -?, --help`: also prints the help message
- `-v, --version`: prints some version information about the tool and the runtime

## install subcommand

this can be combined with the `generate` subcommand found below. you dont have to specify the path twice, just omit that.

- `install [version] [path]`: version and path are required. this just downloads the server jar for that version to the given path.

## generate subcommand

- `generate [-l|--linux|-w|--windows|-wl] (identifier) [path]`: choose either linux or windows - or both. windows will generate a .bat file, linux a .sh file, `-wl` will generate both. you can specify more defaults than the prewritten ones in the file, thats the identifier. it defaults to `default`, surprisingly. theres a bit more info on this file in the readme.

## link subcommand

- `link [version]`: just prints the direct download link for that version's server jar to console, for later use.

## refresh subcommand

- `refresh`: forces the tool to regenerate its local launchermeta cache. used if you're impatient after a new version releases and have recently used the tool or refreshed the cache.