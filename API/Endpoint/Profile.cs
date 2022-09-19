using RestSharp;

namespace CodeStatsForVS.API.Endpoint
{
    /// <summary>
    /// API по работе с профилем пользователя.
    /// </summary>
    public class Profile
    {
        /// <summary>
        /// Чтение общедоступной информации пользователя.
        /// </summary>
        /// <returns></returns>
        public static async Task<Models.Profile> GetProfileAsync()
        {
            try
            {
                var options = await General.GetLiveInstanceAsync();

                if (string.IsNullOrEmpty(options.Username) || string.IsNullOrEmpty(options.ProfileApiUrl))
                {
                    return null;
                }

                var client = new RestClient();
                var request = new RestRequest($"{options.ProfileApiUrl}/{options.Username}");
                var response = await client.GetAsync<Models.Profile>(request);

                return response;
            }
            catch (Exception ex)
            {
                await ex.LogAsync();

                return null;
            }
        }
    }
}
