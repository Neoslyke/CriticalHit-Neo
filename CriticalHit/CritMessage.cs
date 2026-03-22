using Newtonsoft.Json;

namespace CriticalHit;

public class CritMessage
{
    [JsonProperty("MessageSettings")]
    public Dictionary<string, int[]> Messages = new Dictionary<string, int[]>();
}