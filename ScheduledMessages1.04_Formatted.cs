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
        private TimeSpan messageInterval = TimeSpan.FromSeconds(60); // was 600 for 10 minutes in seconds - set to 60 seconds now
        private int messageIndex = 0;

        // Each message has content and a message type
        // §c = red §a = green §e = yellow §b = aqua §6 = gold §f = white
        private (string message, EnumChatType type)[] messages = new (string, EnumChatType)[]
{
("§fThis is the age of the Seraph...\n§f\"Life's but a walking shadow, a poor player that struts and frets his hour upon the stage, and then is heard no more. It is a tale told by an idiot, full of sound and fury, signifying nothing.\"\n§7— Macbeth", EnumChatType.Notification),
("§fThis is the age of the Seraph...\n§f\"The world is indeed comic, but the joke is on mankind.\"\n§7— H. P. Lovecraft", EnumChatType.Notification),
("§fThis is the age of the Seraph...\n§f\"Prosperity knits a man to the world. He feels that he is finding his place in it, while really it is finding its place in him.\"\n§7— C.S. Lewis", EnumChatType.Notification),
("§fThis is the age of the Seraph...\n§f\"I cannot forget Carcosa where black stars hang in the heavens; where the shadows of men's thoughts lengthen in the afternoon, when the twin suns sink into the lake of Hali; and my mind will bear forever the memory of the Pallid Mask. I pray God will curse the writer, as the writer has cursed the world with this beautiful, stupendous creation, terrible in its simplicity, irresistible in its truth—a world which now trembles before the King in Yellow.\"\n§7— Robert W. Chambers, The King in Yellow", EnumChatType.Notification),
("§fThis is the age of the Seraph...\n§f\"And strange moons circle through the skies. But stranger still is Lost Carcosa.\"\n§7— Robert W. Chambers, The King in Yellow", EnumChatType.Notification),
("§fThis is the age of the Seraph...\n§f\"Ph'nglui mglw'nafh Cthulhu R'lyeh wgah'nagl fhtagn. In his house at R'lyeh dead Cthulhu waits dreaming.\"\n§7— H.P. Lovecraft, The Call of Cthulhu", EnumChatType.Notification),
("§fThis is the age of the Seraph...\n§f\"A man can endure anything, as long as he knows the end will come.\"\n§7— Haruki Murakami", EnumChatType.Notification),
("§fThis is the age of the Seraph...\n§f\"If you're going through hell, keep going.\"\n§7— Winston Churchill", EnumChatType.Notification),
("§fThis is the age of the Seraph...\n§f\"He who is not bold enough to be stared at from across the abyss is not bold enough to stare into it himself.\"\n§7— Memo, Silent Hill 2", EnumChatType.Notification),
("§fThis is the age of the Seraph...\n§f\"The fear of blood tends to create fear for the flesh.\"\n§7— Silent Hill Opening", EnumChatType.Notification),
("§fThis is the age of the Seraph...\n§f\"Notice that the stiffest tree is most easily cracked, while the bamboo or willow survives by bending with the wind.\"\n§7— Bruce Lee", EnumChatType.Notification),
("§fThis is the age of the Seraph...\n§f\"The Old Ones were, the Old Ones are, and the Old Ones shall be. Not in the spaces we know, but between them. They walk serene and primal, undimensioned and to us unseen.\"\n§7— H.P. Lovecraft", EnumChatType.Notification),
("§fThis is the age of the Seraph...\n§f\"It is only by way of pain one arrives at pleasure.\"\n§7— Marquis de Sade", EnumChatType.Notification),
("§fThis is the age of the Seraph...\n§f\"Horror is the badge of humanity, worn proudly, self-righteously, and often falsely.\"\n§7— Poppy Z. Brite, Exquisite Corpse", EnumChatType.Notification),
("§fThis is the age of the Seraph...\n§f\"It is sometimes an appropriate response to reality to go insane.\"\n§7— Philip K. Dick, VALIS", EnumChatType.Notification),
("§fThis is the age of the Seraph...\n§f\"We are not enemies, but friends. We must not be enemies. Though passion may have strained it must not break our bonds of affection. The mystic chords of memory will yet swell when again touched, as surely they will be, by the better angels of our nature.\"\n§7— Abraham Lincoln", EnumChatType.Notification),
("§fThis is the age of the Seraph...\n§f\"He who has a why to live can bear almost any how.\"\n§7— Friedrich Nietzsche", EnumChatType.Notification),
("§fThis is the age of the Seraph...\n§f\"Extinction is the rule. Survival is the exception.\"\n§7— Carl Sagan", EnumChatType.Notification),
("§fThis is the age of the Seraph...\n§f\"And it came to me that these trees had been hardly smaller when I was yet unborn, and had stood as they stood now when I was a child playing among the cypresses and peaceful tombs of our necropolis, and that they would stand yet, drinking in the last light of the dying sun, even as now, when I had been dead as long as those who rested there.\"\n§7— Gene Wolfe, The Book of the New Sun", EnumChatType.Notification)
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


 


