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
        "§fThis is the age of the Seraph...§f " +
        "Ph'nglui mglw'nafh Cthulhu R'lyeh wgah'nagl fhtagn. " +
        "In his house at R'lyeh dead Cthulhu waits dreaming " +
        "§6- H.P. Lovecraft, The Call of Cthulhu§6",
        EnumChatType.Chat
    ),
    (
        "§fThis is the age of the Seraph...§f " +
        "A man can endure anything, as long as he knows the end will come. " +
        "§6- Haruki Murakami§6",
        EnumChatType.Chat
    ),
    (
        "§fThis is the age of the Seraph...§f " +
        "If you're going through hell, keep going." +
        "§6- Winston Churchill§6",
        EnumChatType.Chat
    ),
    (
        "§fThis is the age of the Seraph...§f " +
        "He who is not bold enough to be stared at  " +
        "from across the abyss is not bold enough " +
        "to stare into it himself " +
        "§6- Memo, Silent Hill 2§6",
        EnumChatType.Chat
    ),
    (
        "§fThis is the age of the Seraph...§f " +
        "The fear of blood tends to create fear for the flesh. " +
        "§6- Silent Hill Opening§6",
        EnumChatType.Chat
    ),
    (
        "§fThis is the age of the Seraph...§f " +
        "Notice that the stiffest tree is most easily cracked, while " +
        "the bamboo or willow survives by bending with the wind. " +
        "§6- Bruce Lee§6",
        EnumChatType.Chat
    ),
    (
        "§fThis is the age of the Seraph...§f " +
        "The Old Ones were, the Old Ones are, and the Old Ones shall be. Not in " +
        "the spaces we know, but between them. They walk serene and primal, " +
        "undimensioned and to us unseen. " +
        "§6- H.P. Lovecraft§6",
        EnumChatType.Chat
    ),
    (
        "§fThis is the age of the Seraph...§f " +
        "It is only by way of pain one arrives at pleasure " +
        "§6- Marquis de Sade§6",
        EnumChatType.Chat
    ),
    (
        "§fThis is the age of the Seraph...§f " +
        "Horror is the badge of humanity, worn proudly, self-righteously, and " +
        "often falsely " +
        "§6- Poppy Z. Brite, Exquisite Corpse§6",
        EnumChatType.Chat
    ),
    (
        "§fThis is the age of the Seraph...§f " +
        "It is sometimes an appropriate response to reality to go insane. " +
        "§6- Philip K. Dick, VALIS§6",
        EnumChatType.Chat
    ),
    (
        "§fThis is the age of the Seraph...§f " +
        "We are not enemies, but friends. We must not be enemies. " +
        "Though passion may have strained it must not break our bonds of affection. " +
        "The mystic chords of memory, will yet swell when again touched, as surely " + 
        "they will be, by the better angels of our nature " +
        "§6- Abraham Lincoln§6",
        EnumChatType.Chat
    ),
    (
        "§fThis is the age of the Seraph...§f " +
        "He who has a why to live can bear almost any how. " +
        "§6- Friedrich Nietzsche§6",
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
