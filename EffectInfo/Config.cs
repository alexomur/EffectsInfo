using Exiled.API.Interfaces;

namespace EffectInfo
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        // Default hint text
        public string HintText { get; set; } = "\n\n\n\n\n\n\n\n\n<align=left>          Effects:</align>";
    }
}