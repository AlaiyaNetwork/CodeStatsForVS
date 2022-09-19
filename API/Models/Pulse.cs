using System.Collections.Generic;

using Newtonsoft.Json;

namespace CodeStatsForVS.API.Models
{
    /// <summary>
    /// Класс, описывающий структуру данных набранного опыта с момента последней отправки данных на сервер.
    /// </summary>
    public class Pulse
    {
        /// <summary>
        /// Временная метка (должна быть не старше недели).
        /// </summary>
        [JsonProperty("coded_at")]
        public DateTime CodedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// Списки <see cref="PulseXp"/> на отправку.
        /// </summary>
        [JsonProperty("xps")]
        public List<PulseExp> Experiences { get; set; } = new();
    }
}
