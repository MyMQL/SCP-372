using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using MEC;

namespace SCP372Plugin
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "SCP372MyMQL";
        public override string Author => "MyMQL";
        public override Version RequiredExiledVersion => new Version(8, 14, 0);

        public static Plugin Instance { get; private set; }
        private VisibilityManager visibilityManager;

        public override void OnEnabled()
        {
            Instance = this;
            visibilityManager = new VisibilityManager();
            Exiled.Events.Handlers.Player.InteractingDoor += OnInteractingDoor;
            Exiled.Events.Handlers.Player.Shooting += OnShooting;
            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStarted;

            // Check if SC-P372 currently have invisibilty
            Timing.RunCoroutine(MonitorVisibilityState());

            if (Config.Debug)
                Log.Info($"{Name} has been successfully enabled!");

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.InteractingDoor -= OnInteractingDoor;
            Exiled.Events.Handlers.Player.Shooting -= OnShooting;
            Exiled.Events.Handlers.Server.RoundStarted -= OnRoundStarted;
            visibilityManager = null;
            Instance = null;

            if (Config.Debug)
                Log.Info($"{Name} has been disabled.");

            base.OnDisabled();
        }

        private void OnRoundStarted()
        {
            if (Config.Debug)
                Log.Info("Round started. Checking for SCP-372 assignment...");

            // Wylosowanie, czy SCP-372 ma się pojawić
            if (new Random().Next(0, 100) < Config.SpawnChance)
            {
                var potentialPlayers = Player.List;
                var randomPlayer = potentialPlayers.ElementAtOrDefault(new Random().Next(0, potentialPlayers.Count()));

                if (randomPlayer != null)
                {
                    AssignScp372(randomPlayer);

                    if (Config.Debug)
                        Log.Info($"Randomly assigned {randomPlayer.Nickname} as SCP-372.");
                }
                else
                {
                    if (Config.Debug)
                        Log.Warn("No players available to assign as SCP-372.");
                }
            }
            else
            {
                if (Config.Debug)
                    Log.Info("No SCP-372 spawned this round due to chance.");
            }
        }

        private IEnumerator<float> MonitorVisibilityState()
        {
            while (true)
            {
                yield return Timing.WaitForSeconds(1f);
                visibilityManager.MonitorState();
            }
        }

        public void AssignScp372(Player player)
        {
            player.Role.Set(Config.StartingRole);
            player.Health = Config.StartingHealth;
            visibilityManager.AssignScp372Player(player);

            // Do not touch it, it makes 372 invisible at start
            visibilityManager.EnsureInvisible(player);

            player.Broadcast(7, Config.BroadcastMessage);

            if (Config.Debug)
                Log.Info($"Player {player.Nickname} has been assigned as SCP-372.");
        }

        private void OnInteractingDoor(Exiled.Events.EventArgs.Player.InteractingDoorEventArgs ev)
        {
            if (ev.Player == visibilityManager.Scp372Player)
            {
                visibilityManager.TemporarilyMakeVisible(ev.Player, Config.VisibilityDuration);
            }
        }

        private void OnShooting(Exiled.Events.EventArgs.Player.ShootingEventArgs ev)
        {
            if (ev.Player == visibilityManager.Scp372Player)
            {
                visibilityManager.TemporarilyMakeVisible(ev.Player, Config.VisibilityDuration);
            }
        }
    }
}



