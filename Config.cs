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

        // Duration of visibility after an action (e.g., shooting, opening doors)
        public float VisibilityDuration { get; set; } = 4.0f;

        // Starting health for SCP-372
        public float StartingHealth { get; set; } = 100f;

        // Role assigned to SCP-372
        public RoleTypeId StartingRole { get; set; } = RoleTypeId.ClassD;

        // Broadcast message for SCP-372
        public string BroadcastMessage { get; set; } = "<b><color=red>You are SCP-372!</color></b>";

        // Percent chance for SCP-372 to spawn
        public int SpawnChance { get; set; } = 30;

        // Should the cassie announce that 372 spawned?
        public bool EnableCassieOnSpawn { get; set; } = true;

        // Message announced by CASSIE when SCP-372 spawns
        public string CassieMessageOnSpawn { get; set; } = "SCP 3 7 2 has breached containment. Exercise caution.";

        // Should the cassie announce SCP-372 escaping?
        public bool EnableCassieOnEscape { get; set; } = true;

        // Message announced by CASSIE when SCP-372 escapes
        public string CassieMessageOnEscape { get; set; } = "SCP 3 7 2 has escaped.";

        // Should the cassie announce SCP-372's death?
        public bool EnableCassieOnDeath { get; set; } = true;

        // Message announced by CASSIE when SCP-372 dies
        public string CassieMessageOnDeath { get; set; } = "SCP 3 7 2 has been neutralized.";

        // Should SCP-372 be visible when using items (e.g., medkits, adrenaline)?
        // If true, SCP-372 becomes visible when interacting with items.
        // If false, SCP-372 remains invisible while using items.
        public bool VisibleWhenUsingItems { get; set; } = true;

        // Determines if SCP-372 becomes visible when using voice chat (Q key).
        // If true, SCP-372 becomes visible when speaking and remains visible for the specified duration after stopping.
        public bool VisibleWhenUsingVoiceChat { get; set; } = true;

        // Duration (in seconds) for SCP-372 to remain visible after speaking.
        public float VoiceChatVisibilityDuration { get; set; } = 4f;

        // Typ pokoju, w którym SCP-372 ma się zrespić. Przykład: "Hcz049".
        [Description("W.I.P - I highly suggest leaving it as default, some rooms might be broken.")]
        public RoomType SpawnRoomType { get; set; } = RoomType.Hcz939;

        // Minimalna liczba graczy wymagana, aby SCP-372 mógł się pojawić.
        public int MinPlayers { get; set; } = 1;
    }
}