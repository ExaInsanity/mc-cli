namespace MinecraftServerCli.Commons;

using System;

using Newtonsoft.Json;

public class StartupScript
{
    [JsonProperty("id")]
    public String Id { get; set; } = null!;

    [JsonProperty("windows")]
    public String Windows { get; set; } = null!;

    [JsonProperty("linux")]
    public String Linux { get; set; } = null!;
}
