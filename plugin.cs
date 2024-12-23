﻿using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using PlayerRoles;
using PluginAPI.Events;

namespace SCP372Plugin
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "SCP372MyMQL";
        public override string Author => "MyMQL";
        public override Version RequiredExiledVersion => new Version(9, 0, 1);
        public override Version Version => new Version(1, 3, 0);

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
            Exiled.Events.Handlers.Player.Died += OnPlayerDied;
            Exiled.Events.Handlers.Player.UsingItem += OnUsingItem;
            Exiled.Events.Handlers.Player.VoiceChatting += OnVoiceChatting;

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
            Exiled.Events.Handlers.Player.Died -= OnPlayerDied;
            Exiled.Events.Handlers.Player.UsingItem -= OnUsingItem;
            Exiled.Events.Handlers.Player.VoiceChatting -= OnVoiceChatting;
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

        private void OnInteractingDoor(InteractingDoorEventArgs ev)
        {
            if (ev.Player == visibilityManager.Scp372Player)
            {
                visibilityManager.TemporarilyMakeVisible(ev.Player, Config.VisibilityDuration);
            }
        }

        private void OnShooting(ShootingEventArgs ev)
        {
            if (ev.Player == visibilityManager.Scp372Player)
            {
                visibilityManager.TemporarilyMakeVisible(ev.Player, Config.VisibilityDuration);
            }
        }

        private void OnEscaping(EscapingEventArgs ev)
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

        private void OnPlayerDied(DiedEventArgs ev)
        {
            // Sprawdź, czy zmarły gracz to SCP-372
            if (ev.Player.SessionVariables.ContainsKey("IsSCP372") && (bool)ev.Player.SessionVariables["IsSCP372"])
            {
                visibilityManager.HandlePlayerDeath(ev.Player);
            }
        }

        private void OnUsingItem(UsingItemEventArgs ev)
        {
            if (ev.Player == visibilityManager.Scp372Player && Config.VisibleWhenUsingItems)
            {
                visibilityManager.TemporarilyMakeVisible(ev.Player, Config.VisibilityDuration);
            }
        }

        private void OnVoiceChatting(VoiceChattingEventArgs ev)
        {
            if (ev.Player == visibilityManager.Scp372Player && Config.VisibleWhenUsingVoiceChat)
            {
                // Tymczasowo pokazuje SCP-372 na czas określony w konfiguracji
                visibilityManager.TemporarilyMakeVisible(ev.Player, Config.VoiceChatVisibilityDuration);
            }
        }

    }
}



