using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using System.Linq;

namespace SCP372Plugin
{
    public class EventHandlers
    {
        private readonly VisibilityManager visibilityManager;

        public EventHandlers(VisibilityManager visibilityManager)
        {
            this.visibilityManager = visibilityManager;
        }

        public void OnRoundStarted()
        {
            if (Plugin.Instance.Config.Debug)
                Log.Info("Round started. Checking for SCP-372 assignment...");

            if (Player.List.Count() < Plugin.Instance.Config.MinPlayers)
            {
                if (Plugin.Instance.Config.Debug)
                    Log.Warn("Not enough players for SCP-372 to spawn.");
                return;
            }

            if (new System.Random().Next(0, 100) < Plugin.Instance.Config.SpawnChance)
            {
                var potentialPlayers = Player.List.Where(p => p.Role.Team == PlayerRoles.Team.ClassD).ToList();
                if (!potentialPlayers.Any())
                {
                    if (Plugin.Instance.Config.Debug)
                        Log.Warn("No suitable players for SCP-372 assignment.");
                    return;
                }

                var randomPlayer = potentialPlayers.ElementAt(new System.Random().Next(potentialPlayers.Count));
                Plugin.Instance.AssignScp372(randomPlayer);

                if (Plugin.Instance.Config.EnableCassieOnSpawn)
                    Cassie.Message(Plugin.Instance.Config.CassieMessageOnSpawn);

                if (Plugin.Instance.Config.Debug)
                    Log.Info($"Randomly assigned {randomPlayer.Nickname} as SCP-372.");
            }
            else if (Plugin.Instance.Config.Debug)
            {
                Log.Info("No SCP-372 spawned this round due to chance.");
            }
        }

        public void OnInteractingDoor(InteractingDoorEventArgs ev)
        {
            if (ev.Player == visibilityManager.Scp372Player)
            {
                visibilityManager.TemporarilyMakeVisible(ev.Player, Plugin.Instance.Config.VisibilityDuration);
            }
        }

        public void OnInteractingElevator(InteractingElevatorEventArgs ev)
        {
            if (ev.Player == visibilityManager.Scp372Player)
            {
                visibilityManager.TemporarilyMakeVisible(ev.Player, Plugin.Instance.Config.VisibilityDuration);
            }
        }

        public void OnShooting(ShootingEventArgs ev)
        {
            if (ev.Player == visibilityManager.Scp372Player)
            {
                visibilityManager.TemporarilyMakeVisible(ev.Player, Plugin.Instance.Config.VisibilityDuration);
            }
        }

        public void OnEscaping(EscapingEventArgs ev)
        {
            if (ev.Player == visibilityManager.Scp372Player)
            {
                visibilityManager.HandlePlayerEscape(ev.Player);

                if (Plugin.Instance.Config.Debug)
                {
                    Log.Info($"Player {ev.Player.Nickname} escaped as SCP-372. System disabled.");
                }
            }
        }

        public void OnPlayerDied(DiedEventArgs ev)
        {
            if (ev.Player.SessionVariables.ContainsKey("IsSCP372") && (bool)ev.Player.SessionVariables["IsSCP372"])
            {
                visibilityManager.HandlePlayerDeath(ev.Player);
            }
        }

        public void OnUsingItem(UsingItemEventArgs ev)
        {
            if (ev.Player == visibilityManager.Scp372Player && Plugin.Instance.Config.VisibleWhenUsingItems)
            {
                visibilityManager.TemporarilyMakeVisible(ev.Player, Plugin.Instance.Config.VisibilityDuration);
            }
        }

        public void OnVoiceChatting(VoiceChattingEventArgs ev)
        {
            if (ev.Player == visibilityManager.Scp372Player && Plugin.Instance.Config.VisibleWhenUsingVoiceChat)
            {
                visibilityManager.TemporarilyMakeVisible(ev.Player, Plugin.Instance.Config.VoiceChatVisibilityDuration);
            }
        }
    }
}