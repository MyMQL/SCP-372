using Exiled.API.Interfaces;
using PlayerRoles;

namespace SCP372Plugin
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        // chance for 372 to spawn 50 means 50%
        public int SpawnChance { get; set; } = 50;

        // class that 372 spawns into, it would be much
        public RoleTypeId StartingRole { get; set; } = RoleTypeId.ClassD;

        // starting heatlh
        public float StartingHealth { get; set; } = 100f;

        // Visibility time in seconds
        public float VisibilityDuration { get; set; } = 2f;

        // broadcast after YOU being scp372
        public string BroadcastMessage { get; set; } = "You have been selected as SCP-372. Stay hidden!";

        // Should the cassie announce that 372 escaped?
        public bool EnableCassieOnEscape { get; set; } = true;

        // WORKS ONLY IF enablecassie = true, what message should the cassie announce?
        public string CassieMessageOnEscape { get; set; } = "SCP 3 7 2 has escaped.";
    }
}



