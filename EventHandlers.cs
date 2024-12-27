using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;

namespace SCP372Plugin
{
    public class EventHandlers
    {
        private readonly VisibilityManager visibilityManager;

        public EventHandlers(VisibilityManager visibilityManager)
        {
            this.visibilityManager = visibilityManager;
        }

        public void OnInteractingDoor(InteractingDoorEventArgs ev)
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