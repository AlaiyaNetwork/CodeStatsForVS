using Newtonsoft.Json;

namespace CodeStatsForVS.API.Models
{
    /// <summary>
    /// Класс, описывающий структуру данных набранного опыта для конкретного языка.
    /// </summary>
    public class PulseExp
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
        public PulseExp() { }

        /// <summary>
        /// Конструктор, инициализирующий новый язык, при первом использовании.
        /// </summary>
        /// <param name="language">Название языка.</param>
        public PulseExp(string language)
        {
            Language = language;
            Xp = 1;
        }

        /// <summary>
        /// Конструктор, иниализирующий новый язык с указание кол-ва очков.
        /// </summary>
        /// <param name="language">Название языка.</param>
        /// <param name="xp">Очки опыта.</param>
        public PulseExp(string language, int xp)
        {
            Language = language;
            Xp = xp;
        }
    }
}
