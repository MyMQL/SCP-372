using System;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Interfaces;

namespace SCP372Plugin.API
{
    public class SCP372DiedEventArgs : IExiledEvent
    {
        public SCP372DiedEventArgs(Player player)
        {
            Player = player;
        }

        public Player Player { get; }
    }
}
