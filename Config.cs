using Exiled.API.Interfaces;
using PlayerRoles;

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
    }
}