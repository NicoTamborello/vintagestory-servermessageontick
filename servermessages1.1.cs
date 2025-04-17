using System;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace ScheduledMessages
{
    public class ScheduledMessagesMod : ModSystem
    {
        private ICoreServerAPI sapi;
        private double lastMessageTime = 0;
        private double messageInterval = 600; // 10 minutes in seconds
        private int messageIndex = 0;
        
        // Each message has content and a message type
        // §c = red §a = green §e = yellow §b = aqua §6 = gold §f = white
        private (string message, EnumChatType type)[] messages = new (string, EnumChatType)[]
        {
            ("§a[Tip]§f Store your items before logging out!", EnumChatType.Chat),
            ("§6[Announcement]§f Server restart in 30 minutes.", EnumChatType.Chat),
            ("§b[Community]§f Join our Discord: §9discord.gg/example", EnumChatType.Chat),
            ("§e[Help]§f Type §l/help§r for a list of commands.", EnumChatType.Chat)
            (
        "§a[Tip]§f " +
        "Store your items before logging out! " +
        "§eAnd don't forget to feed your animals!"
        "§6- Author§6",
        EnumChatType.Chat
    ),
    (
        "§6[Announcement]§f " +
        "Server restart in 30 minutes. " +
        "§cSave your progress now!",
        EnumChatType.Chat
    ),
    (
        "§b[Community]§f " +
        "Join our Discord: §9discord.gg/example " +
        "§7We host weekly events!",
        EnumChatType.Chat
    ),
    (
        "§e[Help]§f " +
        "Type §l/help§r for commands. " +
        "§dUse §l/spawn§r to return to spawn.",
        EnumChatType.Chat
    )    
        };

        public override void StartServerSide(ICoreServerAPI api)
        {
            sapi = api;
            sapi.Event.RegisterGameTickListener(OnTick, 1000); // Run OnTick() every 1 second
        }

        private void OnTick(float deltaTime)
        {
            double currentTime = sapi.World.Calendar.TotalHours * 3600; // Convert game hours to seconds

            if (currentTime - lastMessageTime >= messageInterval)
            {
                lastMessageTime = currentTime;

                var (msg, type) = messages[messageIndex];
                sapi.SendMessageToGroup(0, msg, type);

                messageIndex = (messageIndex + 1) % messages.Length;
            }
        }
    }
}
