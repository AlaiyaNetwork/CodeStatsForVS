using Newtonsoft.Json;

namespace CodeStatsForVS.API.Models
{
    /// <summary>
    /// Суммарный опыт и опыт, набранный за последние 12 часов для кокретного языка или пользовательской машины.
    /// </summary>
    public class ProfileExp
    {
        /// <summary>
        /// Общее кол-во набранного опыта.
        /// </summary>
        [JsonProperty("xps")]
        public long Xps { get; set; }

        /// <summary>
        /// Опыт, набранный за последние 12 часов.
        /// </summary>
        [JsonProperty("new_xps")]
        public long NewXps { get; set; }
    }
}
