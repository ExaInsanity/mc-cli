namespace MinecraftServerCli.Commons;

using System;

using Newtonsoft.Json;

public class LaunchermetaVersion
{
    [JsonProperty("id")]
    public String Id { get; set; } = null!;

    [JsonProperty("type")]
    public String Type { get; set; } = null!;

    [JsonProperty("url")]
    public String Url { get; set; } = null!;

    [JsonProperty("time")]
    public DateTimeOffset Time { get; set; }

    [JsonProperty("releaseTime")]
    public DateTimeOffset ReleaseTime { get; set; }
}
