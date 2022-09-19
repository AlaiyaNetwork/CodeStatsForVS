using System.ComponentModel;

namespace CodeStatsForVS
{
    /// <summary>
    /// Extension settings.
    /// </summary>
    public class General : BaseOptionModel<General>
    {
        [Category("CodeStats Settings")]
        [DisplayName("Machine Key")]
        [Description("Set your machine´s API Key here.")]
        [DefaultValue(true)]
        public string MachineKey { get; set; } = "";

        [Category("CodeStats Settings")]
        [DisplayName("Pulse API Url")]
        [Description("The Pulse API that requests get sent to. DO NOT CHANGE if you dont know what you are doing!")]
        [DefaultValue(true)]
        public string PulseApiUrl { get; set; } = "https://codestats.net/api/my/pulses";

        [Category("CodeStats Settings")]
        [DisplayName("Profile API Url")]
        [Description("The Profile API that read public user's information. DO NOT CHANGE if you dont know what you are doing!")]
        [DefaultValue(true)]
        public string ProfileApiUrl { get; set; } = "https://codestats.net/api/users";

        [Category("CodeStats Settings")]
        [DisplayName("User Name")]
        [Description("Set your username, if you need check your statistics into VS interface!")]
        [DefaultValue(true)]
        public string Username { get; set; } = "";

        public General() : base()
        {
            Saved += delegate 
            { 
                VS.StatusBar.ShowMessageAsync("Options Saved!").FireAndForget(); 
            };
        }
    }
}
