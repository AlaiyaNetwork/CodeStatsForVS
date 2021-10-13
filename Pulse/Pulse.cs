using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

using Newtonsoft.Json;

namespace CodeStatsForVS.Pulse
{
    public class Pulse
    {
        [JsonProperty("coded_at")]
        public DateTime CodedAt { get; set; } = DateTime.Now;

        [JsonProperty("xps")]
        public List<PulseXp> Experiences { get; set; } = new();

        private static readonly HttpClient _client = new();

        public Pulse()
        {
            if (!_client.DefaultRequestHeaders.Contains("User-Agent"))
                _client.DefaultRequestHeaders.Add("User-Agent", "CodeStats.Client/0.5.8");
        }

        public void IncrementExperience(string language)
        {
            var result = Experiences.FirstOrDefault(pulseXp => pulseXp.Language == language);
            if (result == null)
            {
                Experiences.Add(new PulseXp
                {
                    Language = language,
                    Xp = 1,
                });
                return;
            }

            result.Xp++;
        }

        public async Task ExecuteAsync()
        {
            var options = await General.GetLiveInstanceAsync();

            var apiKey = options.MachineKey;
            var pluseApiUrl = options.PulseApiUrl;

            if (apiKey == "")
                return;

            var requestContent = new StringContent(JsonConvert.SerializeObject(this, Formatting.Indented));
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            requestContent.Headers.Add("X-API-Token", apiKey);

            var response = await _client.PostAsync(pluseApiUrl, requestContent);
            await response.Content.ReadAsStringAsync();
            Experiences.Clear();
        }
    }
}
