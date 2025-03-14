using Exiled.API.Features;
using Exiled.Events.EventArgs.Interfaces;

namespace SCP372Plugin.API
{
    public class SCP372EscapedEventArgs : IExiledEvent
    {
        public SCP372EscapedEventArgs(Player player)
        {
            Player = player;
        }

        public Player Player { get; }
    }
}
