using System.Collections.Generic;
using Exiled.API.Features;
using MEC;
using CustomPlayerEffects;

namespace SCP372Plugin
{
    public class VisibilityManager
    {
        public Player Scp372Player { get; private set; }
        private CoroutineHandle visibilityCoroutine;
        private bool isTemporarilyVisible = false;

        public void AssignScp372Player(Player player)
        {
            Scp372Player = player;
            EnsureInvisible(player); // Ustawienie niewidzialności natychmiast po przypisaniu
        }

        public void TemporarilyMakeVisible(Player player, float duration)
        {
            if (isTemporarilyVisible)
            {
                Timing.KillCoroutines(visibilityCoroutine); // Reset licznika czasu widzialności
            }

            EnsureVisible(player);
            isTemporarilyVisible = true;

            visibilityCoroutine = Timing.RunCoroutine(ResetVisibility(player, duration));
        }

        private IEnumerator<float> ResetVisibility(Player player, float duration)
        {
            yield return Timing.WaitForSeconds(duration);

            if (player != null && player.IsAlive)
            {
                EnsureInvisible(player);
            }

            isTemporarilyVisible = false;
        }

        public void EnsureInvisible(Player player)
        {
            if (isTemporarilyVisible) return;

            var effectsController = player.ReferenceHub.playerEffectsController;

            if (!effectsController.GetEffect<Invisible>().IsEnabled)
            {
                effectsController.EnableEffect<Invisible>(float.MaxValue); // Ustaw efekt na nieskończony czas
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

                if (!effectsController.GetEffect<Invisible>().IsEnabled)
                {
                    if (Plugin.Instance.Config.Debug)
                        Log.Warn($"Correction: Player {Scp372Player.Nickname} did not have the Invisible effect, reapplying.");
                    EnsureInvisible(Scp372Player);
                }
            }
        }
    }
}



