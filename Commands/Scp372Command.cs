using System;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;

namespace SCP372Plugin
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Scp372Command : ICommand
    {
        public string Command => "scp372";
        public string[] Aliases => Array.Empty<string>();
        public string Description => "Assigns a player as SCP-372.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("scp372.spawn"))
            {
                response = "You do not have permission to use this command.";
                return false;
            }

            if (arguments.Count != 1)
            {
                response = "Specify a player ID. Usage: scp372 <player_id>";
                return false;
            }

            if (!int.TryParse(arguments.At(0), out int playerId))
            {
                response = "Invalid player ID.";
                return false;
            }

            Player player = Player.Get(playerId);
            if (player == null)
            {
                response = "Player not found.";
                return false;
            }

            SCP372Manager.Instance.AssignScp372(player);
            response = $"Player {player.Nickname} has been assigned as SCP-372.";
            return true;
        }
    }
}