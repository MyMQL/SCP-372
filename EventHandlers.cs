using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using System.Collections.Generic;
using System.Linq;

namespace SCP372Plugin
{
    public class EventHandlers
    {
        private readonly Config config;

        public EventHandlers()
        {
            config = Plugin.Instance.Config;
        }

        public void OnRoundStarted()
        {
            
            Log.Debug("Round started. Checking for SCP-372 assignment...");

            if (Player.List.Count() < config.MinPlayers)
            {
                Log.Debug("Not enough players for SCP-372 to spawn.");
                return;
            }

            if (UnityEngine.Random.Range(0, 101) < config.SpawnChance)
            {
                List<Player> potentialPlayers = Player.List.Where(p => p.Role.Team == PlayerRoles.Team.ClassD).ToList();
                if (!potentialPlayers.Any())
                {
                    Log.Debug("No suitable players for SCP-372 assignment.");
                    return;
                }

                Player randomPlayer = potentialPlayers.ElementAt(new System.Random().Next(potentialPlayers.Count));
                Plugin.Instance.AssignScp372(randomPlayer);

                if (config.EnableCassieOnSpawn)
                {
                    Cassie.Message(config.CassieMessageOnSpawn);
                    Log.Debug($"Randomly assigned {randomPlayer.Nickname} as SCP-372.");
                }
            }
            else
            {
                Log.Debug("No SCP-372 spawned this round due to chance.");
            }
        }

        public void OnInteractingDoor(InteractingDoorEventArgs ev)
        {
            if (ev.Player == VisibilityManager.Instance.Scp372Player)
            {
                VisibilityManager.Instance.TemporarilyMakeVisible(ev.Player, config.VisibilityDuration);
            }
        }

        public void OnInteractingElevator(InteractingElevatorEventArgs ev)
        {
            if (ev.Player == VisibilityManager.Instance.Scp372Player)
            {
                VisibilityManager.Instance.TemporarilyMakeVisible(ev.Player, config.VisibilityDuration);
            }
        }

        public void OnShooting(ShootingEventArgs ev)
        {
            if (ev.Player == VisibilityManager.Instance.Scp372Player)
            {
                VisibilityManager.Instance.TemporarilyMakeVisible(ev.Player, config.VisibilityDuration);
            }
        }

        public void OnEscaping(EscapingEventArgs ev)
        {
            if (ev.Player == VisibilityManager.Instance.Scp372Player)
            {
                VisibilityManager.Instance.HandlePlayerEscape(ev.Player);
                Log.Debug($"Player {ev.Player.Nickname} escaped as SCP-372. System disabled.");
            }
        }

        public void OnPlayerDied(DiedEventArgs ev)
        {
            if (ev.Player.SessionVariables.ContainsKey("IsSCP372") && (bool)ev.Player.SessionVariables["IsSCP372"])
            {
                VisibilityManager.Instance.HandlePlayerDeath(ev.Player);
            }
        }

        public void OnUsingItem(UsingItemEventArgs ev)
        {
            if (ev.Player == VisibilityManager.Instance.Scp372Player && config.VisibleWhenUsingItems)
            {
                VisibilityManager.Instance.TemporarilyMakeVisible(ev.Player, config.VisibilityDuration);
            }
        }

        public void OnVoiceChatting(VoiceChattingEventArgs ev)
        {
            if (ev.Player == VisibilityManager.Instance.Scp372Player && config.VisibleWhenUsingVoiceChat)
            {
                VisibilityManager.Instance.TemporarilyMakeVisible(ev.Player, config.VoiceChatVisibilityDuration);
            }
        }
    }
}