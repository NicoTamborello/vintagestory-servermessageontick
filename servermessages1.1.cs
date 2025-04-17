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
        (
        "§f[Tip]§f " +
        "Store your items before logging out! " +
        "§eAnd don't forget to feed your animals!"
        "§6- Author§6",
        EnumChatType.Chat
    ),
            (
        "§f[Tip]§f " +
        "Store your items before logging out! " +
        "§eAnd don't forget to feed your animals!"
        "§6- Author§6",
        EnumChatType.Chat
    ),
    (
        "§f[Tip]§f " +
        "Store your items before logging out! " +
        "§eAnd don't forget to feed your animals!"
        "§6- Author§6",
        EnumChatType.Chat
    ),
    (
        "§f[Tip]§f " +
        "Store your items before logging out! " +
        "§eAnd don't forget to feed your animals!"
        "§6- Author§6",
        EnumChatType.Chat
    ),
    (
       "§f[Tip]§f " +
        "Store your items before logging out! " +
        "§eAnd don't forget to feed your animals!"
        "§6- Author§6",
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
