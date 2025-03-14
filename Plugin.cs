using System;
using System.Linq;
using Exiled.API.Features;
using MEC;

namespace SCP372Plugin
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "SCP372";
        public override string Author => "MyMQL";
        private readonly Version _version = new Version(1, 6, 0);
        public override Version Version => _version;
        private readonly Version _requiredExiledVersion = new Version(9, 5, 1);
        public override Version RequiredExiledVersion => _requiredExiledVersion;

        public static Plugin Instance { get; private set; }
        private VisibilityManager visibilityManager;
        private EventHandlers eventHandlers;

        public override void OnEnabled()
        {
            Instance = this;
            visibilityManager = new VisibilityManager();
            eventHandlers = new EventHandlers();

            Exiled.Events.Handlers.Player.InteractingDoor += eventHandlers.OnInteractingDoor;
            Exiled.Events.Handlers.Player.Shooting += eventHandlers.OnShooting;
            Exiled.Events.Handlers.Player.Escaping += eventHandlers.OnEscaping;
            Exiled.Events.Handlers.Player.Died += eventHandlers.OnPlayerDied;
            Exiled.Events.Handlers.Player.UsingItem += eventHandlers.OnUsingItem;
            Exiled.Events.Handlers.Player.VoiceChatting += eventHandlers.OnVoiceChatting;
            Exiled.Events.Handlers.Server.RoundStarted += eventHandlers.OnRoundStarted;
            Exiled.Events.Handlers.Player.InteractingElevator += eventHandlers.OnInteractingElevator;

            var manager = new SCP372Manager();
            Timing.RunCoroutine(manager.MonitorVisibilityState());

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.InteractingDoor -= eventHandlers.OnInteractingDoor;
            Exiled.Events.Handlers.Player.Shooting -= eventHandlers.OnShooting;
            Exiled.Events.Handlers.Player.Escaping -= eventHandlers.OnEscaping;
            Exiled.Events.Handlers.Player.Died -= eventHandlers.OnPlayerDied;
            Exiled.Events.Handlers.Player.UsingItem -= eventHandlers.OnUsingItem;
            Exiled.Events.Handlers.Player.VoiceChatting -= eventHandlers.OnVoiceChatting;
            Exiled.Events.Handlers.Server.RoundStarted -= eventHandlers.OnRoundStarted;
            Exiled.Events.Handlers.Player.InteractingElevator -= eventHandlers.OnInteractingElevator;

            visibilityManager = null;
            eventHandlers = null;
            Instance = null;

            base.OnDisabled();
        }

        public void AssignScp372(Player player)
        {
            player.Role.Set(Config.StartingRole);
            player.Health = Config.StartingHealth;

            visibilityManager.AssignScp372Player(player);

            // make sure that 372 is invisibile at the start, no need to wait x time
            visibilityManager.EnsureInvisible(player);

            Room spawnRoom = Room.List.FirstOrDefault(room => room.Type == Config.SpawnRoomType);

            if (spawnRoom != null)
            {
                player.Position = spawnRoom.Position + UnityEngine.Vector3.up;
                Log.Debug($"Player {player.Nickname} assigned as SCP-372 and spawned in {Config.SpawnRoomType}.");
            }
            else
            {
                Log.Debug($"Room of type {Config.SpawnRoomType} not found. Assigning SCP-372 to default spawn position.");
            }

            player.Broadcast(7, Config.BroadcastMessage);
            Log.Debug($"Player {player.Nickname} has been assigned as SCP-372.");
        }
    }
}