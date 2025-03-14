using Exiled.API.Interfaces;
using PlayerRoles;
using Exiled.API.Enums;
using System.ComponentModel;

namespace SCP372Plugin
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        [Description("Duration of visibility after an action (e.g., shooting, opening doors).")]
        public float VisibilityDuration { get; set; } = 4.0f;

        [Description("Starting health for SCP-372.")]
        public float StartingHealth { get; set; } = 100f;

        [Description("Role assigned to SCP-372.")]
        public RoleTypeId StartingRole { get; set; } = RoleTypeId.ClassD;

        [Description("Broadcast message for SCP-372.")]
        public string BroadcastMessage { get; set; } = "<b><color=red>You are SCP-372!</color></b>";

        [Description("Percent chance for SCP-372 to spawn.")]
        public int SpawnChance { get; set; } = 30;

        [Description("Should the cassie announce that 372 spawned?")]
        public bool EnableCassieOnSpawn { get; set; } = true;

        [Description("Message announced by CASSIE when SCP-372 spawns.")]
        public string CassieMessageOnSpawn { get; set; } = "SCP 3 7 2 has breached containment. Exercise caution.";

        [Description("Should the cassie announce SCP-372 escaping?")]
        public bool EnableCassieOnEscape { get; set; } = true;

        [Description("Message announced by CASSIE when SCP-372 escapes.")]
        public string CassieMessageOnEscape { get; set; } = "SCP 3 7 2 has escaped.";

        [Description("Should the cassie announce SCP-372's death?")]
        public bool EnableCassieOnDeath { get; set; } = true;

        [Description("Message announced by CASSIE when SCP-372 dies.")]
        public string CassieMessageOnDeath { get; set; } = "SCP 3 7 2 has been neutralized.";

        [Description("Should SCP-372 be visible when using items (e.g., medkits, adrenaline)? If true, SCP-372 becomes visible when interacting with items.")]
        public bool VisibleWhenUsingItems { get; set; } = true;

        [Description("Determines if SCP-372 becomes visible when using voice chat (Q key).")]
        public bool VisibleWhenUsingVoiceChat { get; set; } = true;

        [Description("Duration (in seconds) for SCP-372 to remain visible after speaking.")]
        public float VoiceChatVisibilityDuration { get; set; } = 4f;

        [Description("W.I.P - I highly suggest leaving it as default, some rooms might be broken.")]
        public RoomType SpawnRoomType { get; set; } = RoomType.Hcz939;

        [Description("Minimum number of players required for SCP-372 to spawn.")]
        public int MinPlayers { get; set; } = 1;
    }
}