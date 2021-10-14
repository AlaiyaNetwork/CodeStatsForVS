using Newtonsoft.Json;

namespace CodeStatsForVS
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

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public PulseXp() { }

        /// <summary>
        /// Конструктор, инициализирующий новый язык, при первом использовании.
        /// </summary>
        /// <param name="language">Название языка.</param>
        public PulseXp(string language)
        {
            Language = language;
            Xp = 1;
        }

        /// <summary>
        /// Конструктор, иниализирующий новый язык с указание кол-ва очков.
        /// </summary>
        /// <param name="language">Название языка.</param>
        /// <param name="xp">Очки опыта.</param>
        public PulseXp(string language, int xp)
        {
            Language = language;
            Xp = Xp;
        }
    }
}
