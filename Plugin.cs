using System;
using Exiled.API.Features;
using MEC;

namespace SCP372Plugin
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "SCP372";
        public override string Author => "MyMQL";
        public override Version Version { get; } = new Version(1, 6, 1);
        public override Version RequiredExiledVersion { get; } = new Version(9, 5, 1);

        public static Plugin Instance { get; private set; }
        private SCP372Manager manager;
        private EventHandlers eventHandlers;

        public override void OnEnabled()
        {
            Instance = this;
            manager = new SCP372Manager();
            eventHandlers = new EventHandlers();

            Exiled.Events.Handlers.Player.InteractingDoor += eventHandlers.OnInteractingDoor;
            Exiled.Events.Handlers.Player.Shooting += eventHandlers.OnShooting;
            Exiled.Events.Handlers.Player.Escaping += eventHandlers.OnEscaping;
            Exiled.Events.Handlers.Player.Died += eventHandlers.OnPlayerDied;
            Exiled.Events.Handlers.Player.UsingItem += eventHandlers.OnUsingItem;
            Exiled.Events.Handlers.Player.VoiceChatting += eventHandlers.OnVoiceChatting;
            Exiled.Events.Handlers.Server.RoundStarted += eventHandlers.OnRoundStarted;
            Exiled.Events.Handlers.Player.InteractingElevator += eventHandlers.OnInteractingElevator;


            Timing.RunCoroutine(manager.MonitorVisibilityState());

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.InteractingDoor -= eventHandlers.OnInteractingDoor;
            Exiled.Events.Handlers.Player.Shooting -= eventHandlers.OnShooting;
            Exiled.Events.Handlers.Player.Escaping -= eventHandlers.OnEscaping;
            Exiled.Events.Handlers.Player.Died -= eventHandlers.OnPlayerDied;
            Exiled.Events.Handlers.Player.UsingItem -= eventHandlers.OnUsingItem;
            Exiled.Events.Handlers.Player.VoiceChatting -= eventHandlers.OnVoiceChatting;
            Exiled.Events.Handlers.Server.RoundStarted -= eventHandlers.OnRoundStarted;
            Exiled.Events.Handlers.Player.InteractingElevator -= eventHandlers.OnInteractingElevator;

            manager = null;
            eventHandlers = null;
            Instance = null;

            base.OnDisabled();
        }
    }
}
