using Exiled.API.Interfaces;
using PlayerRoles;

namespace SCP372Plugin
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false; // shows extra info
        public float VisibilityDuration { get; set; } = 2f; // Time that SCP-372 will take to get invisible again
        public RoleTypeId StartingRole { get; set; } = RoleTypeId.ClassD; // Class that SCP-372 uses to spawn, ig?
        public int StartingHealth { get; set; } = 100; // starting hp
        public string BroadcastMessage { get; set; } = "<b><color=red>You are SCP-372!</color></b>"; // broadcast info
    }
}


