using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

using Newtonsoft.Json;

namespace CodeStatsForVS
{
    /// <summary>
    /// Класс API для работы с CodeStats.
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
        public List<PulseXp> Experiences { get; set; } = new();

        /// <summary>
        /// Экземпляр <see cref="HttpClient"/>.
        /// </summary>
        private static readonly HttpClient _client = new();

        /// <summary>
        /// Инициализация <see cref="Pulse"/>.
        /// </summary>
        public Pulse()
        {
            if (!_client.DefaultRequestHeaders.Contains("User-Agent"))
                _client.DefaultRequestHeaders.Add("User-Agent", "CodeStatsForVS/1.2");
        }

        /// <summary>
        /// Добавление очков опыта.
        /// </summary>
        /// <param name="language">
        /// Название языка.
        /// </param>
        public void IncrementExperience(string language)
        {
            var result = Experiences
                .FirstOrDefault(pulseXp => pulseXp.Language == language);

            if (result == null)
            {
                Experiences.Add(new PulseXp(language));
                return;
            }

            result.Xp++;
        }

        /// <summary>
        /// Отправка результатов на сервер.
        /// </summary>
        /// <returns></returns>
        public async Task ExecuteAsync()
        {
            var options = await General.GetLiveInstanceAsync();

            var apiKey = options.MachineKey;
            var pluseApiUrl = options.PulseApiUrl;

            if (apiKey == "")
                return;

            StringContent requestContent = new(JsonConvert.SerializeObject(this, Formatting.Indented));
            requestContent.Headers.ContentType = new("application/json");
            requestContent.Headers.Add("X-API-Token", apiKey);

            var response = await _client.PostAsync(pluseApiUrl, requestContent);
            await response.Content.ReadAsStringAsync();
            Experiences.Clear();
        }
    }
}
