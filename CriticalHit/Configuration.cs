using Newtonsoft.Json;

namespace CriticalHit;
public class Configuration
{
    [JsonProperty("Enable")]
    public bool Enable = true;
    [JsonProperty("CriticalHitsOnly")]
    public bool NoCritMessages = true;

    [JsonConverter(typeof(WeaponTypeDictionaryConverter))]
    [JsonProperty("Messages")]
    public Dictionary<WeaponType, CritMessage> CritMessages { get; set; } = new Dictionary<WeaponType, CritMessage>();

    public void Write(string path)
    {
        using var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write);
        this.Write(stream);
    }

    public void Write(Stream stream)
    {
        var value = JsonConvert.SerializeObject(this, (Formatting)1);
        using var streamWriter = new StreamWriter(stream);
        streamWriter.Write(value);
    }

    public void Read(string path)
    {
        if (File.Exists(path))
        {
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                this.Read(stream);
            }
        }
    }

    public void Read(Stream stream)
    {
        using var streamReader = new StreamReader(stream);
        var deserializedConfig = JsonConvert.DeserializeObject<Configuration>(streamReader.ReadToEnd())!;
        this.CopyFrom(deserializedConfig);
    }

    public void CopyFrom(Configuration sourceConfig)
    {
        this.Enable = sourceConfig.Enable;
        this.NoCritMessages = sourceConfig.NoCritMessages;
        this.CritMessages = sourceConfig.CritMessages;
    }
}