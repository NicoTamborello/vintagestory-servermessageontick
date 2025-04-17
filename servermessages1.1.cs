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
        "§fThis is the age of the Seraph...§f " +
        "Lifes but a walking shadow, a poor player " +
        "That struts and frets his hour upon the stage, " +
        "And then is heard no more. It is a tale"
        "Told by an idiot, full of sound and fury, " +
        "Signifying nothing. " +
        "§6- Macbeth§6",
        EnumChatType.Chat
    ),
    (
        "§fThis is the age of the Seraph...§f " +
        "The world is indeed comic, but the joke is on mankind. " +
        "§ - H. P. Lovecraft§6",
        EnumChatType.Chat
    ),
    (
        "§fThis is the age of the Seraph...§f " +
        "Prosperity knits a man to the world " +
        "He feels that he is finding his place in it, " +
        "while really it is finding its place in him. " +
        "§6- C.S. Lewis§6",
        EnumChatType.Chat
    ),
    (
        "§fThis is the age of the Seraph...§f " +
        "I cannot forget Carcosa where black stars hang in the heavens; where " +
        "the shadows of men's thoughts lengthen in the afternoon, when the " +
        "twin suns sink into the lake of Hali; and my mind will bear for ever the " +
        "memory of the Pallid Mask. I pray God will curse the writer, as the writer " +
        "has cursed the world with this beautiful, stupendous creation, terrible in " +
        "its simplicity, irresistible in its truth—a world which now trembles " +
        "before the King in Yellow. " +
        "while really it is finding its place in him. " +
        "§6- Robert W. Chambers, The King in Yellow§6",
        EnumChatType.Chat
    ),
    (
       "§fThis is the age of the Seraph...§f " +
        "And strange moons circle through the skies " +
        "But Stranger still is " +
        "Lost Carcosa. " +
        "§6- Robert W. Chambers, The King in Yellow§6",
        EnumChatType.Chat
    )
    (
        "§fWords§f " +
        "Ph'nglui mglw'nafh Cthulhu R'lyeh wgah'nagl fhtagn. " +
        "In his house at R'lyeh dead Cthulhu waits dreaming " +
        "§6- H.P. Lovecraft, The Call of Cthulhu§6",
        EnumChatType.Chat
    ),
    (
        "§fWords§f " +
        "Words " +
        "Words " +
        "§6- Author§6",
        EnumChatType.Chat
    ),
    (
        "§fWords§f " +
        "Words " +
        "Words " +
        "§6- Author§6",
        EnumChatType.Chat
    ),
    (
        "§fWords§f " +
        "Words " +
        "Words " +
        "§6- Author§6",
        EnumChatType.Chat
    ),
    (
        "§fWords§f " +
        "Words " +
        "Words " +
        "§6- Author§6",
        EnumChatType.Chat
    ),
    (
        "§fWords§f " +
        "Words " +
        "Words " +
        "§6- Author§6",
        EnumChatType.Chat
    ),
    (
        "§fWords§f " +
        "Words " +
        "Words " +
        "§6- Author§6",
        EnumChatType.Chat
    ),
    (
        "§fWords§f " +
        "Words " +
        "Words " +
        "§6- Author§6",
        EnumChatType.Chat
    ),
    (
        "§fWords§f " +
        "Words " +
        "Words " +
        "§6- Author§6",
        EnumChatType.Chat
    ),
    (
        "§fWords§f " +
        "Words " +
        "Words " +
        "§6- Author§6",
        EnumChatType.Chat
    ),
    (
        "§fWords§f " +
        "Words " +
        "Words " +
        "§6- Author§6",
        EnumChatType.Chat
    ),
    (
        "§fWords§f " +
        "Words " +
        "Words " +
        "§6- Author§6",
        EnumChatType.Chat
    ),    
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
