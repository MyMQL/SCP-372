using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using System.Collections.Generic;
using System.Linq;

namespace SCP372Plugin
{
    public class EventHandlers
    {
        public void OnRoundStarted()
        {
            
            Log.Debug("Round started. Checking for SCP-372 assignment...");

            if (Player.List.Count() < Plugin.Instance.Config.MinPlayers)
            {
                Log.Debug("Not enough players for SCP-372 to spawn.");
                return;
            }

            if (UnityEngine.Random.Range(0, 101) < Plugin.Instance.Config.SpawnChance)
            {
                List<Player> potentialPlayers = Player.List.Where(p => p.Role.Team == PlayerRoles.Team.ClassD).ToList();
                if (!potentialPlayers.Any())
                {
                    Log.Debug("No suitable players for SCP-372 assignment.");
                    return;
                }

                Player randomPlayer = potentialPlayers.GetRandomValue();
                SCP372Manager.Instance.AssignScp372(randomPlayer);

                if (Plugin.Instance.Config.EnableCassieOnSpawn)
                {
                    Cassie.Message(Plugin.Instance.Config.CassieMessageOnSpawn);
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
            if (ev.Player == SCP372Manager.Instance.Scp372Player)
            {
                SCP372Manager.Instance.TemporarilyMakeVisible(ev.Player, Plugin.Instance.Config.VisibilityDuration);
            }
        }

        public void OnInteractingElevator(InteractingElevatorEventArgs ev)
        {
            if (ev.Player == SCP372Manager.Instance.Scp372Player)
            {
                SCP372Manager.Instance.TemporarilyMakeVisible(ev.Player, Plugin.Instance.Config.VisibilityDuration);
            }
        }

        public void OnShooting(ShootingEventArgs ev)
        {
            if (ev.Player == SCP372Manager.Instance.Scp372Player)
            {
                SCP372Manager.Instance.TemporarilyMakeVisible(ev.Player, Plugin.Instance.Config.VisibilityDuration);
            }
        }

        public void OnEscaping(EscapingEventArgs ev)
        {
            if (ev.Player == SCP372Manager.Instance.Scp372Player)
            {
                SCP372Manager.Instance.HandlePlayerEscape(ev.Player);
                Log.Debug($"Player {ev.Player.Nickname} escaped as SCP-372. System disabled.");
            }
        }

        public void OnPlayerDied(DiedEventArgs ev)
        {
            if (ev.Player.SessionVariables.ContainsKey("IsSCP372") && (bool)ev.Player.SessionVariables["IsSCP372"])
            {
                SCP372Manager.Instance.HandlePlayerDeath(ev.Player);
            }
        }

        public void OnUsingItem(UsingItemEventArgs ev)
        {
            if (ev.Player == SCP372Manager.Instance.Scp372Player && Plugin.Instance.Config.VisibleWhenUsingItems)
            {
                SCP372Manager.Instance.TemporarilyMakeVisible(ev.Player, Plugin.Instance.Config.VisibilityDuration);
            }
        }

        public void OnVoiceChatting(VoiceChattingEventArgs ev)
        {
            if (ev.Player == SCP372Manager.Instance.Scp372Player && Plugin.Instance.Config.VisibleWhenUsingVoiceChat)
            {
                SCP372Manager.Instance.TemporarilyMakeVisible(ev.Player, Plugin.Instance.Config.VoiceChatVisibilityDuration);
            }
        }
    }
}