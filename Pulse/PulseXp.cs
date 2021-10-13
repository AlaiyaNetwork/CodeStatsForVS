using Newtonsoft.Json;

namespace CodeStatsForVS.Pulse
{
    /// <summary>
    /// Класс, описывающий структуру данных для CodeStats.
    /// </summary>
    public class PulseXp
    {
        /// <summary>
        /// Название языка.
        /// </summary>
        [JsonProperty("language")]
        public string Language { get; set; }

        /// <summary>
        /// Очки опыта.
        /// </summary>
        [JsonProperty("xp")]
        public int Xp { get; set; }
    }
}
