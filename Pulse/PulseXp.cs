using Newtonsoft.Json;

namespace CodeStatsForVS.Pulse
{
    public class PulseXp
    {
        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("xp")]
        public int Xp { get; set; }
    }
}
