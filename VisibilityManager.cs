using System.Collections.Generic;
using Exiled.API.Features;
using MEC;
using CustomPlayerEffects;
using PlayerRoles;
using SCP372Plugin.API;

namespace SCP372Plugin
{
    public class VisibilityManager
    {
        public Player Scp372Player { get; private set; }
        private CoroutineHandle visibilityCoroutine;
        private bool isTemporarilyVisible = false;
        private bool isOnSurface = false;

        public void AssignScp372Player(Player player)
        {
            Scp372Player = player;
            player.SessionVariables["IsSCP372"] = true;
            EnsureInvisible(player); // make sure that the player is invisible at start, reffers to plugin,.cs

            SCP372Event.OnSCP372Assigned(new SCP372AssignedEventArgs(player)); // Trigger event

            // monitor surface
            Timing.RunCoroutine(MonitorSurface());
        }

        public void TemporarilyMakeVisible(Player player, float duration)
        {
            if (isTemporarilyVisible)
            {
                Timing.KillCoroutines(visibilityCoroutine); // reset invisibility timer
            }

            EnsureVisible(player);
            isTemporarilyVisible = true;

            visibilityCoroutine = Timing.RunCoroutine(ResetVisibility(player, duration));
        }

        private IEnumerator<float> ResetVisibility(Player player, float duration)
        {
            yield return Timing.WaitForSeconds(duration);

            if (player != null && player.IsAlive && !isOnSurface)
            {
                EnsureInvisible(player);
            }

            isTemporarilyVisible = false;
        }

        public void EnsureInvisible(Player player)
        {
            if (isTemporarilyVisible || isOnSurface) return; // Do not make visible if he is on surface

            var effectsController = player.ReferenceHub.playerEffectsController;

            if (!effectsController.GetEffect<Invisible>().IsEnabled)
            {
                effectsController.EnableEffect<Invisible>(float.MaxValue); 
                if (Plugin.Instance.Config.Debug)
                    Log.Info($"Player {player.Nickname} is now invisible.");
            }
        }

        public void EnsureVisible(Player player)
        {
            var effectsController = player.ReferenceHub.playerEffectsController;

            if (effectsController.GetEffect<Invisible>().IsEnabled)
            {
                effectsController.DisableEffect<Invisible>();
                if (Plugin.Instance.Config.Debug)
                    Log.Info($"Player {player.Nickname} is now visible.");
            }
        }

        public void MonitorState()
        {
            if (Scp372Player != null && !isTemporarilyVisible)
            {
                var effectsController = Scp372Player.ReferenceHub.playerEffectsController;

                if (!effectsController.GetEffect<Invisible>().IsEnabled && !isOnSurface)
                {
                    if (Plugin.Instance.Config.Debug)
                        Log.Warn($"Correction: Player {Scp372Player.Nickname} did not have the Invisible effect, reapplying.");
                    EnsureInvisible(Scp372Player);
                }
            }
        }

        public void RemoveScp372Player(Player player)
        {
            if (player == Scp372Player)
            {
                Scp372Player = null;
                player.SessionVariables.Remove("IsSCP372");
                // stop invisibility
                player.ReferenceHub.playerEffectsController.DisableEffect<Invisible>();

                if (Plugin.Instance.Config.Debug)
                    Log.Info($"Player {player.Nickname} is no longer SCP-372. System deactivated.");
            }
        }

        public void HandlePlayerEscape(Player player)
        {
            RemoveScp372Player(player);

            if (Plugin.Instance.Config.EnableCassieOnEscape)
            {
                string cassieMessage = Plugin.Instance.Config.CassieMessageOnEscape;

                if (!string.IsNullOrEmpty(cassieMessage))
                {
                    Cassie.Message(cassieMessage);
                    if (Plugin.Instance.Config.Debug)
                        Log.Info($"CASSIE broadcasted: {cassieMessage}");
                }
                else if (Plugin.Instance.Config.Debug)
                {
                    Log.Warn("CassieMessageOnEscape in config is empty or null!");
                }
            }

            SCP372Event.OnSCP372Escaped(new SCP372EscapedEventArgs(player)); // Trigger event

            if (Plugin.Instance.Config.Debug)
                Log.Info($"SCP-372 player {player.Nickname} escaped and is no longer SCP-372.");
        }

        public void HandlePlayerDeath(Player player)
        {
            if (player == Scp372Player)
            {
                RemoveScp372Player(player);

                // Trigger the SCP-372 death event
                SCP372Event.OnSCP372Died(new SCP372DiedEventArgs(player));

                if (Plugin.Instance.Config.EnableCassieOnDeath)
                {
                    string cassieMessage = Plugin.Instance.Config.CassieMessageOnDeath;

                    if (!string.IsNullOrEmpty(cassieMessage))
                    {
                        Cassie.Message(cassieMessage);
                        if (Plugin.Instance.Config.Debug)
                            Log.Info($"CASSIE broadcasted: {cassieMessage}");
                    }
                }

                if (Plugin.Instance.Config.Debug)
                    Log.Info($"SCP-372 player {player.Nickname} has died. System deactivated.");
            }
        }

        private IEnumerator<float> MonitorSurface()
        {
            while (Scp372Player != null && Scp372Player.IsAlive)
            {
                // check if the player is on surface
                if (Scp372Player.CurrentRoom.Zone == Exiled.API.Enums.ZoneType.Surface)
                {
                    if (!isOnSurface)
                    {
                        isOnSurface = true;
                        EnsureVisible(Scp372Player); // if he is on the surface, make him visible
                        if (Plugin.Instance.Config.Debug)
                            Log.Info($"SCP-372 detected on the surface. Now visible.");
                    }
                }
                else
                {
                    if (isOnSurface)
                    {
                        isOnSurface = false;
                        EnsureInvisible(Scp372Player); // if he is inside now, change him to invisible
                        if (Plugin.Instance.Config.Debug)
                            Log.Info($"SCP-372 left the surface. Now invisible.");
                    }
                }

                yield return Timing.WaitForSeconds(1f); // check every seconds if he is on surface
            }
        }
    }
}



