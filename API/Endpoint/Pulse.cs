using System.Linq;
using System.Net.Http;

using CodeStatsForVS.API.Models;

using Newtonsoft.Json;

namespace CodeStatsForVS.API.Endpoint
{
    /// <summary>
    /// API по работе с импульсами на сервер.
    /// </summary>
    public class Pulse
    {
        /// <summary>
        /// Данные текущего импульса.
        /// </summary>
        private Models.Pulse _pulse = new();

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
            {
                _client.DefaultRequestHeaders.Add("User-Agent", "CodeStatsForVS/1.2");
            }             
        }

        /// <summary>
        /// Добавление очков опыта.
        /// </summary>
        /// <param name="language">
        /// Название языка.
        /// </param>
        public void IncrementExperience(string language)
        {
            var result = _pulse.Experiences
                .FirstOrDefault(pulseXp => pulseXp.Language == language);

            if (result == null)
            {
                _pulse.Experiences.Add(new PulseExp(language));
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
            try
            {
                var options = await General.GetLiveInstanceAsync();

                if (string.IsNullOrEmpty(options.MachineKey) || string.IsNullOrEmpty(options.PulseApiUrl))
                {
                    return;
                }

                StringContent requestContent = new(JsonConvert.SerializeObject(this, Formatting.Indented));
                requestContent.Headers.ContentType = new("application/json");
                requestContent.Headers.Add("X-API-Token", options.MachineKey);

                var response = await _client.PostAsync(options.PulseApiUrl, requestContent);
                await response.Content.ReadAsStringAsync();
                _pulse = new();
            }
            catch (Exception ex)
            {
                await ex.LogAsync();
            }     
        }
    }
}
