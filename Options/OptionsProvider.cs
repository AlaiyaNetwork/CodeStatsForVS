namespace CodeStatsForVS
{
    internal partial class OptionsProvider
    {
        // Register the options with these attributes on your package class:
        // [ProvideOptionPage(typeof(OptionsProvider.GeneralOptions), "CodeStatsForVS", "General", 0, 0, true)]
        // [ProvideProfile(typeof(OptionsProvider.GeneralOptions), "CodeStatsForVS", "General", 0, 0, true)]
        public class GeneralOptions : BaseOptionPage<General> { }
    }
}
