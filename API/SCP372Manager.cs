﻿using System.Collections.Generic;
using Exiled.API.Features;
using CustomPlayerEffects;
using SCP372Plugin.API;
using MEC;
using System.Linq;

namespace SCP372Plugin
{
    public static class SCP372Manager
    {
        public static Player Scp372Player { get; private set; }
        private static CoroutineHandle _visibilityCoroutine;
        private static bool _isTemporarilyVisible = false;
        private static bool _isOnSurface = false;

        public static void AssignScp372Player(Player player)
        {
            Scp372Player = player;
            player.SessionVariables["IsSCP372"] = true;
            EnsureInvisible(player); // make sure that the player is invisible at start, reffers to plugin,.cs

            SCP372Event.OnSCP372Assigned(new SCP372AssignedEventArgs(player)); // Trigger event

            // monitor surface
            Timing.RunCoroutine(MonitorSurface());
        }

        public static void TemporarilyMakeVisible(Player player, float duration)
        {
            if (_isTemporarilyVisible)
            {
                Timing.KillCoroutines(_visibilityCoroutine); // reset invisibility timer
            }

            EnsureVisible(player);
            _isTemporarilyVisible = true;

            _visibilityCoroutine = Timing.RunCoroutine(ResetVisibility(player, duration));
        }

        private static IEnumerator<float> ResetVisibility(Player player, float duration)
        {
            yield return Timing.WaitForSeconds(duration);

            if (player != null && player.IsAlive && !_isOnSurface)
            {
                EnsureInvisible(player);
            }

            _isTemporarilyVisible = false;
        }

        public static void EnsureInvisible(Player player)
        {
            if (_isTemporarilyVisible || _isOnSurface) return; // Make visible if he is on surface

            if (!player.GetEffect<Invisible>().IsEnabled)
            {
                player.EnableEffect<Invisible>(float.MaxValue);
                Log.Debug($"Player {player.Nickname} is now invisible.");
            }
        }

        public static void EnsureVisible(Player player)
        {
            if (player.GetEffect<Invisible>().IsEnabled)
            {
                player.DisableEffect<Invisible>();
                Log.Debug($"Player {player.Nickname} is now visible.");
            }
        }

        public static void MonitorState()
        {
            if (Scp372Player != null && !_isTemporarilyVisible)
            {
                if (!Scp372Player.GetEffect<Invisible>().IsEnabled && !_isOnSurface)
                {
                    Log.Debug($"Correction: Player {Scp372Player.Nickname} did not have the Invisible effect, reapplying.");
                    EnsureInvisible(Scp372Player);
                }
            }
        }

        public static void RemoveScp372Player(Player player)
        {
            if (player == Scp372Player)
            {
                Scp372Player = null;
                player.SessionVariables.Remove("IsSCP372");
                // stop invisibility
                player.DisableEffect<Invisible>();
                Log.Debug($"Player {player.Nickname} is no longer SCP-372. System deactivated.");
            }
        }

        public static void HandlePlayerEscape(Player player)
        {
            RemoveScp372Player(player);

            if (Plugin.Instance.Config.EnableCassieOnEscape)
            {
                string cassieMessage = Plugin.Instance.Translation.CassieMessageOnEscape;

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

        public static void HandlePlayerDeath(Player player)
        {
            if (player == Scp372Player)
            {
                RemoveScp372Player(player);

                // Trigger the SCP-372 death event
                SCP372Event.OnSCP372Died(new SCP372DiedEventArgs(player));

                if (Plugin.Instance.Config.EnableCassieOnDeath)
                {
                    string cassieMessage = Plugin.Instance.Translation.CassieMessageOnDeath;

                    if (!string.IsNullOrEmpty(cassieMessage))
                    {
                        Cassie.Message(cassieMessage);
                        Log.Debug($"CASSIE broadcasted: {cassieMessage}");
                    }
                }
                Log.Debug($"SCP-372 player {player.Nickname} has died. System deactivated.");
            }
        }

        private static IEnumerator<float> MonitorSurface()
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

        public static IEnumerator<float> MonitorVisibilityState()
        {
            while (true)
            {
                yield return Timing.WaitForSeconds(1f);
                MonitorState();
            }
        }

        public static void AssignScp372(Player player)
        {
            player.Role.Set(Plugin.Instance.Config.StartingRole);
            player.Health = Plugin.Instance.Config.StartingHealth;

            AssignScp372Player(player);

            // make sure that 372 is invisibile at the start, no need to wait x time
            EnsureInvisible(player);

            Room spawnRoom = Room.List.FirstOrDefault(room => room.Type == Plugin.Instance.Config.SpawnRoomType);

            if (spawnRoom != null)
            {
                player.Position = spawnRoom.Position + UnityEngine.Vector3.up;
                Log.Debug($"Player {player.Nickname} assigned as SCP-372 and spawned in {Plugin.Instance.Config.SpawnRoomType}.");
            }
            else
            {
                Log.Debug($"Room of type {Plugin.Instance.Config.SpawnRoomType} not found. Assigning SCP-372 to default spawn position.");
            }

            player.Broadcast(7, Plugin.Instance.Config.BroadcastMessage, default, true);
            Log.Debug($"Player {player.Nickname} has been assigned as SCP-372.");
        }
    }
}