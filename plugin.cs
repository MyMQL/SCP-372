using System;
using System.Linq;
using Exiled.API.Features;
using MEC;
using PlayerRoles;
using System.Collections.Generic;

namespace SCP372Plugin
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "SCP372MyMQL";
        public override string Author => "MyMQL";
        public override Version Version => new Version(1, 4, 2);
        public override Version RequiredExiledVersion => new Version(9, 0, 1);

        public static Plugin Instance { get; private set; }
        private VisibilityManager visibilityManager;
        private EventHandlers eventHandlers;

        public override void OnEnabled()
        {
            Instance = this;
            visibilityManager = new VisibilityManager();
            eventHandlers = new EventHandlers(visibilityManager);

            Exiled.Events.Handlers.Player.InteractingDoor += eventHandlers.OnInteractingDoor;
            Exiled.Events.Handlers.Player.Shooting += eventHandlers.OnShooting;
            Exiled.Events.Handlers.Player.Escaping += eventHandlers.OnEscaping;
            Exiled.Events.Handlers.Player.Died += eventHandlers.OnPlayerDied;
            Exiled.Events.Handlers.Player.UsingItem += eventHandlers.OnUsingItem;
            Exiled.Events.Handlers.Player.VoiceChatting += eventHandlers.OnVoiceChatting;
            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStarted;

            Timing.RunCoroutine(MonitorVisibilityState());

            if (Config.Debug)
                Log.Info($"{Name} has been successfully enabled!");

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
            Exiled.Events.Handlers.Server.RoundStarted -= OnRoundStarted;

            visibilityManager = null;
            eventHandlers = null;
            Instance = null;

            if (Config.Debug)
                Log.Info($"{Name} has been disabled.");

            base.OnDisabled();
        }

        private IEnumerator<float> MonitorVisibilityState()
        {
            while (true)
            {
                yield return Timing.WaitForSeconds(1f);
                visibilityManager.MonitorState();
            }
        }

        private void OnRoundStarted()
        {
            if (Config.Debug)
                Log.Info("Round started. Checking for SCP-372 assignment...");

            if (Player.List.Count() < Config.MinPlayers)
            {
                if (Config.Debug)
                    Log.Warn("Not enough players for SCP-372 to spawn.");
                return;
            }

            if (new Random().Next(0, 100) < Config.SpawnChance)
            {
                var potentialPlayers = Player.List.Where(p => p.Role.Team == Team.ClassD).ToList();
                if (!potentialPlayers.Any())
                {
                    if (Config.Debug)
                        Log.Warn("No suitable players for SCP-372 assignment.");
                    return;
                }

                var randomPlayer = potentialPlayers.ElementAt(new Random().Next(potentialPlayers.Count));
                AssignScp372(randomPlayer);

                if (Config.EnableCassieOnSpawn)
                    Cassie.Message(Config.CassieMessageOnSpawn);

                if (Config.Debug)
                    Log.Info($"Randomly assigned {randomPlayer.Nickname} as SCP-372.");
            }
            else if (Config.Debug)
            {
                Log.Info("No SCP-372 spawned this round due to chance.");
            }
        }

        public void AssignScp372(Player player)
        {
            player.Role.Set(Config.StartingRole);
            player.Health = Config.StartingHealth;

            visibilityManager.AssignScp372Player(player);

            // make sure that 372 is invisibile at the start, no need to wait x time
            visibilityManager.EnsureInvisible(player);

            var spawnRoom = Room.List.FirstOrDefault(room => room.Type == Config.SpawnRoomType);

            if (spawnRoom != null)
            {
                player.Position = spawnRoom.Position + new UnityEngine.Vector3(0, 1, 0); 
                if (Config.Debug)
                    Log.Info($"Player {player.Nickname} assigned as SCP-372 and spawned in {Config.SpawnRoomType}.");
            }
            else
            {
                if (Config.Debug)
                    Log.Warn($"Room of type {Config.SpawnRoomType} not found. Assigning SCP-372 to default spawn position.");
            }

            player.Broadcast(7, Config.BroadcastMessage);

            if (Config.Debug)
                Log.Info($"Player {player.Nickname} has been assigned as SCP-372.");
        }
    }
}