using System.Collections.Generic;

using Newtonsoft.Json;

namespace CodeStatsForVS.API.Models
{
    /// <summary>
    /// Профиль пользователя.
    /// </summary>
    public class Profile
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        [JsonProperty("user")]
        public string Username { get; set; }

        /// <summary>
        /// Общее кол-во набранного опыта.
        /// </summary>
        [JsonProperty("total_xp")]
        public long TotalXp { get; set; }

        /// <summary>
        /// Кол-во набранного опыта за последине 12 часов.
        /// </summary>
        [JsonProperty("new_xp")]
        public long NewXp { get; set; }

        /// <summary>
        /// Словарь, описывающий кол-во набранного опыта для каждого отдельного дня.
        /// </summary>
        [JsonProperty("dates")]
        public Dictionary<string, long> Dates { get; set; }

        /// <summary>
        /// Словарь, описывающий кол-во набранного опыта для каждой пользовательской машины.
        /// </summary>
        [JsonProperty("machines")]
        public Dictionary<string, ProfileExp> Machines { get; set; }

        /// <summary>
        /// Словарь, описывающий кол-во набранного опыта для каждого языка.
        /// </summary>
        [JsonProperty("languages")]
        public Dictionary<string, ProfileExp> Languages { get; set; }
    }
}
