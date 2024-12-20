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
        public override Version Version => new Version(1, 1, 0);

        public static Plugin Instance { get; private set; }
        private VisibilityManager visibilityManager;

        public override void OnEnabled()
        {
            Instance = this;
            visibilityManager = new VisibilityManager();
            Exiled.Events.Handlers.Player.InteractingDoor += OnInteractingDoor;
            Exiled.Events.Handlers.Player.Shooting += OnShooting;
            Exiled.Events.Handlers.Player.Escaping += OnEscaping; // new handler
            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStarted;

            // monitor if 372 should have invisibility but it doesnt, if it should then do it
            Timing.RunCoroutine(MonitorVisibilityState());

            if (Config.Debug)
                Log.Info($"{Name} has been successfully enabled!");

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.InteractingDoor -= OnInteractingDoor;
            Exiled.Events.Handlers.Player.Shooting -= OnShooting;
            Exiled.Events.Handlers.Player.Escaping -= OnEscaping; // remove handler
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

            // random spawn chance system
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

            // make sure that 372 is invisibile at the start, no need to wait x time
            visibilityManager.EnsureInvisible(player);

            player.Broadcast(7, Config.BroadcastMessage);


            if (Config.EnableCassieOnSpawn && !string.IsNullOrEmpty(Config.CassieMessageOnSpawn))
            {
                Cassie.Message(Config.CassieMessageOnSpawn);
                if (Config.Debug)
                    Log.Info($"CASSIE broadcasted: {Config.CassieMessageOnSpawn}");
            }

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

        private void OnEscaping(Exiled.Events.EventArgs.Player.EscapingEventArgs ev)
        {
            if (ev.Player == visibilityManager.Scp372Player)
            {
                // disable 372 system after escape
                visibilityManager.HandlePlayerEscape(ev.Player);

                if (Config.Debug)
                {
                    Log.Info($"Player {ev.Player.Nickname} escaped as SCP-372. System disabled.");
                }
            }
        }
    }
}



