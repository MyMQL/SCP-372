using System.Collections.Generic;
using Exiled.API.Features;
using CustomPlayerEffects;
using SCP372Plugin.API;
using MEC;

namespace SCP372Plugin
{
    public class VisibilityManager
    {
        public static VisibilityManager Instance { get; private set; }
        public Player Scp372Player { get; private set; }
        private CoroutineHandle _visibilityCoroutine;
        private bool _isTemporarilyVisible = false;
        private bool _isOnSurface = false;

        public VisibilityManager()
        {
            Instance = this;
        }

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
            if (_isTemporarilyVisible)
            {
                Timing.KillCoroutines(_visibilityCoroutine); // reset invisibility timer
            }

            EnsureVisible(player);
            _isTemporarilyVisible = true;

            _visibilityCoroutine = Timing.RunCoroutine(ResetVisibility(player, duration));
        }

        private IEnumerator<float> ResetVisibility(Player player, float duration)
        {
            yield return Timing.WaitForSeconds(duration);

            if (player != null && player.IsAlive && !_isOnSurface)
            {
                EnsureInvisible(player);
            }

            _isTemporarilyVisible = false;
        }

        public void EnsureInvisible(Player player)
        {
            if (_isTemporarilyVisible || _isOnSurface) return; // Make visible if he is on surface

            var effectsController = player.ReferenceHub.playerEffectsController;

            if (!effectsController.GetEffect<Invisible>().IsEnabled)
            {
                effectsController.EnableEffect<Invisible>(float.MaxValue); 
                Log.Debug($"Player {player.Nickname} is now invisible.");
            }
        }

        public void EnsureVisible(Player player)
        {
            var effectsController = player.ReferenceHub.playerEffectsController;

            if (effectsController.GetEffect<Invisible>().IsEnabled)
            {
                effectsController.DisableEffect<Invisible>();
                Log.Debug($"Player {player.Nickname} is now visible.");
            }
        }

        public void MonitorState()
        {
            if (Scp372Player != null && !_isTemporarilyVisible)
            {
                var effectsController = Scp372Player.ReferenceHub.playerEffectsController;

                if (!effectsController.GetEffect<Invisible>().IsEnabled && !_isOnSurface)
                {
                    Log.Debug($"Correction: Player {Scp372Player.Nickname} did not have the Invisible effect, reapplying.");
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
                Log.Debug($"Player {player.Nickname} is no longer SCP-372. System deactivated.");
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
                    Log.Debug($"CASSIE broadcasted: {cassieMessage}");
                }
                else
                {
                    Log.Debug("CassieMessageOnEscape in config is empty or null!");
                }
            }

            SCP372Event.OnSCP372Escaped(new SCP372EscapedEventArgs(player)); // Trigger event
            Log.Debug($"SCP-372 player {player.Nickname} escaped and is no longer SCP-372.");
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
                        Log.Debug($"CASSIE broadcasted: {cassieMessage}");
                    }
                }
                Log.Debug($"SCP-372 player {player.Nickname} has died. System deactivated.");
            }
        }

        private IEnumerator<float> MonitorSurface()
        {
            while (Scp372Player != null && Scp372Player.IsAlive)
            {
                // check if the player is on surface
                if (Scp372Player.CurrentRoom.Zone == Exiled.API.Enums.ZoneType.Surface)
                {
                    if (!_isOnSurface)
                    {
                        _isOnSurface = true;
                        EnsureVisible(Scp372Player); // if he is on the surface, make him visible
                        Log.Debug($"SCP-372 detected on the surface. Now visible.");
                    }
                }
                else
                {
                    if (_isOnSurface)
                    {
                        _isOnSurface = false;
                        EnsureInvisible(Scp372Player); // if he is inside now, change him to invisible
                        Log.Debug($"SCP-372 left the surface. Now invisible.");
                    }
                }

                yield return Timing.WaitForSeconds(1f); // check every seconds if he is on surface
            }
        }
    }
}