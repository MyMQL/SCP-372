using Exiled.API.Interfaces;
using System.ComponentModel;

namespace SCP372Plugin
{
    public class Translation : ITranslation
    {
        [Description("Message announced by CASSIE when SCP-372 spawns.")]
        public string CassieMessageOnSpawn { get; set; } = "SCP 3 7 2 has breached containment. Exercise caution.";

        [Description("Message announced by CASSIE when SCP-372 escapes.")]
        public string CassieMessageOnEscape { get; set; } = "SCP 3 7 2 has escaped.";

        [Description("Message announced by CASSIE when SCP-372 dies.")]
        public string CassieMessageOnDeath { get; set; } = "SCP 3 7 2 has been neutralized.";
    }
}
