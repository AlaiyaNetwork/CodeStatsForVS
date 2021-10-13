using System.ComponentModel;

using Community.VisualStudio.Toolkit;

namespace CodeStatsForVS
{
    internal partial class OptionsProvider
    {
        // Register the options with these attributes on your package class:
        // [ProvideOptionPage(typeof(OptionsProvider.GeneralOptions), "CodeStatsForVS", "General", 0, 0, true)]
        // [ProvideProfile(typeof(OptionsProvider.GeneralOptions), "CodeStatsForVS", "General", 0, 0, true)]
        public class GeneralOptions : BaseOptionPage<General> { }
    }

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
