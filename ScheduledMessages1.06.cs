using System;
using System.Linq;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace ScheduledMessages
{
    public class ScheduledMessagesMod : ModSystem
    {
        private ICoreServerAPI sapi;
        private DateTime lastMessageTime = DateTime.Now;
        private TimeSpan messageInterval = TimeSpan.FromSeconds(1200); // was 600 for 10 minutes in seconds - set to 60 seconds now
        private int messageIndex = 0;

        // Each message has content and a message type
        //  = red  = green  = yellow  = aqua  = gold  = white
        private (string message, EnumChatType type)[] messages = new (string, EnumChatType)[]
{
("This is the age of the Seraph...\n\"Life's but a walking shadow, a poor player that struts and frets his hour upon the stage, and then is heard no more. It is a tale told by an idiot, full of sound and fury, signifying nothing.\"\n— Macbeth", EnumChatType.Notification),
("This is the age of the Seraph...\n\"The world is indeed comic, but the joke is on mankind.\"\n— H. P. Lovecraft", EnumChatType.Notification),
("This is the age of the Seraph...\n\"Prosperity knits a man to the world. He feels that he is finding his place in it, while really it is finding its place in him.\"\n— C.S. Lewis", EnumChatType.Notification),
("This is the age of the Seraph...\n\"I cannot forget Carcosa where black stars hang in the heavens; where the shadows of men's thoughts lengthen in the afternoon, when the twin suns sink into the lake of Hali; and my mind will bear forever the memory of the Pallid Mask. I pray God will curse the writer, as the writer has cursed the world with this beautiful, stupendous creation, terrible in its simplicity, irresistible in its truth—a world which now trembles before the King in Yellow.\"\n— Robert W. Chambers, The King in Yellow", EnumChatType.Notification),
("This is the age of the Seraph...\n\"And strange moons circle through the skies. But stranger still is Lost Carcosa.\"\n— Robert W. Chambers, The King in Yellow", EnumChatType.Notification),
("This is the age of the Seraph...\n\"Ph'nglui mglw'nafh Cthulhu R'lyeh wgah'nagl fhtagn. In his house at R'lyeh dead Cthulhu waits dreaming.\"\n— H.P. Lovecraft, The Call of Cthulhu", EnumChatType.Notification),
("This is the age of the Seraph...\n\"A man can endure anything, as long as he knows the end will come.\"\n— Haruki Murakami", EnumChatType.Notification),
("This is the age of the Seraph...\n\"If you're going through hell, keep going.\"\n— Winston Churchill", EnumChatType.Notification),
("This is the age of the Seraph...\n\"He who is not bold enough to be stared at from across the abyss is not bold enough to stare into it himself.\"\n— Memo, Silent Hill 2", EnumChatType.Notification),
("This is the age of the Seraph...\n\"The fear of blood tends to create fear for the flesh.\"\n— Silent Hill Opening", EnumChatType.Notification),
("This is the age of the Seraph...\n\"Notice that the stiffest tree is most easily cracked, while the bamboo or willow survives by bending with the wind.\"\n— Bruce Lee", EnumChatType.Notification),
("This is the age of the Seraph...\n\"The Old Ones were, the Old Ones are, and the Old Ones shall be. Not in the spaces we know, but between them. They walk serene and primal, undimensioned and to us unseen.\"\n— H.P. Lovecraft", EnumChatType.Notification),
("This is the age of the Seraph...\n\"It is only by way of pain one arrives at pleasure.\"\n— Marquis de Sade", EnumChatType.Notification),
("This is the age of the Seraph...\n\"Horror is the badge of humanity, worn proudly, self-righteously, and often falsely.\"\n— Poppy Z. Brite, Exquisite Corpse", EnumChatType.Notification),
("This is the age of the Seraph...\n\"It is sometimes an appropriate response to reality to go insane.\"\n— Philip K. Dick, VALIS", EnumChatType.Notification),
("This is the age of the Seraph...\n\"We are not enemies, but friends. We must not be enemies. Though passion may have strained it must not break our bonds of affection. The mystic chords of memory will yet swell when again touched, as surely they will be, by the better angels of our nature.\"\n— Abraham Lincoln", EnumChatType.Notification),
("This is the age of the Seraph...\n\"He who has a why to live can bear almost any how.\"\n— Friedrich Nietzsche", EnumChatType.Notification),
("This is the age of the Seraph...\n\"Extinction is the rule. Survival is the exception.\"\n— Carl Sagan", EnumChatType.Notification),
("This is the age of the Seraph...\n\"And it came to me that these trees had been hardly smaller when I was yet unborn, and had stood as they stood now when I was a child playing among the cypresses and peaceful tombs of our necropolis, and that they would stand yet, drinking in the last light of the dying sun, even as now, when I had been dead as long as those who rested there.\"\n— Gene Wolfe, The Book of the New Sun", EnumChatType.Notification)
};

        public override void StartServerSide(ICoreServerAPI api)
        {
            sapi = api;
            sapi.Event.RegisterGameTickListener(OnTick, 1000); // Run OnTick() every 1 second
        }

        private void OnTick(float deltaTime)
        {
            if (messages.Length == 0) return;

            if (DateTime.Now - lastMessageTime >= messageInterval)
            {
                lastMessageTime = DateTime.Now;

                var (msg, type) = messages[messageIndex];
                sapi.SendMessageToGroup(0, msg, type);

                messageIndex = (messageIndex + 1) % messages.Length;
            }
        }
    }
}



