using System.ComponentModel;

namespace CodeStatsForVS
{
    public class General : BaseOptionModel<General>
    {
        [Category("CodeStats Settings")]
        [DisplayName("Machine Key")]
        [Description("Set your machine´s API Key here.")]
        [DefaultValue(true)]
        public string MachineKey { get; set; } = "";

        [Category("CodeStats Settings")]
        [DisplayName("API Url")]
        [Description("The Pulse API that requests get sent to. DO NOT CHANGE if you dont know what you are doing!")]
        [DefaultValue(true)]
        public string PulseApiUrl { get; set; } = "https://codestats.net/api/my/pulses";
    }
}
