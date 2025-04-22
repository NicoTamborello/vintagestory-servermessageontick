using System;
using System.Collections.Generic;
using System.Linq;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace ScheduledMessages
{
    public class ScheduledMessagesMod : ModSystem
    {
        private ICoreServerAPI sapi;
        private DateTime lastMessageTime = DateTime.Now;
        private TimeSpan messageInterval = TimeSpan.FromSeconds(1200); // 20 minutes
        private int messageIndex = 0;
        private List<int> shuffledIndices = new List<int>();
        private Random rand = new Random();
        private int lastIndex = -1;
        private string lastMessageSent = "";

        private (string message, EnumChatType type)[] messages = new (string, EnumChatType)[]
        {
            ("This is the age of the Seraph...
"Life's but a walking shadow..."
— Macbeth", EnumChatType.Notification),
            ("This is the age of the Seraph...
"The world is indeed comic..."
— H. P. Lovecraft", EnumChatType.Notification),
            ("This is the age of the Seraph...
"Prosperity knits a man to the world..."
— C.S. Lewis", EnumChatType.Notification),
            ("This is the age of the Seraph...
"I cannot forget Carcosa..."
— Robert W. Chambers", EnumChatType.Notification),
            ("This is the age of the Seraph...
"And strange moons circle..."
— Robert W. Chambers", EnumChatType.Notification),
            ("This is the age of the Seraph...
"Ph'nglui mglw'nafh..."
— H.P. Lovecraft", EnumChatType.Notification),
            ("This is the age of the Seraph...
"A man can endure anything..."
— Haruki Murakami", EnumChatType.Notification),
            ("This is the age of the Seraph...
"If you're going through hell..."
— Winston Churchill", EnumChatType.Notification),
            ("This is the age of the Seraph...
"He who is not bold enough..."
— Memo, Silent Hill 2", EnumChatType.Notification),
            ("This is the age of the Seraph...
"The fear of blood..."
— Silent Hill Opening", EnumChatType.Notification),
            ("This is the age of the Seraph...
"The stiffest tree is most easily cracked..."
— Bruce Lee", EnumChatType.Notification),
            ("This is the age of the Seraph...
"The Old Ones were..."
— H.P. Lovecraft", EnumChatType.Notification),
            ("This is the age of the Seraph...
"It is only by way of pain..."
— Marquis de Sade", EnumChatType.Notification),
            ("This is the age of the Seraph...
"Horror is the badge of humanity..."
— Poppy Z. Brite", EnumChatType.Notification),
            ("This is the age of the Seraph...
"It is sometimes an appropriate response..."
— Philip K. Dick", EnumChatType.Notification),
            ("This is the age of the Seraph...
"We are not enemies..."
— Abraham Lincoln", EnumChatType.Notification),
            ("This is the age of the Seraph...
"He who has a why to live..."
— Friedrich Nietzsche", EnumChatType.Notification),
            ("This is the age of the Seraph...
"Extinction is the rule..."
— Carl Sagan", EnumChatType.Notification),
            ("This is the age of the Seraph...
"And it came to me that these trees..."
— Gene Wolfe", EnumChatType.Notification)
        };

        public override void StartServerSide(ICoreServerAPI api)
        {
            sapi = api;
            sapi.Event.RegisterGameTickListener(OnTick, 1000);

            sapi.ChatCommands.Create("lastquote")
                .WithDescription("Shows the last quote that was broadcast.")
                .RequiresPrivilege(Privilege.chat)
                .HandleWith((player, groupId, args) =>
                {
                    return TextCommandResult.Success(string.IsNullOrEmpty(lastMessageSent) ? "No quote sent yet." : lastMessageSent);
                });
        }

        private void OnTick(float deltaTime)
        {
            if (messages.Length == 0) return;

            if (DateTime.Now - lastMessageTime >= messageInterval)
            {
                lastMessageTime = DateTime.Now;

                if (shuffledIndices.Count == 0)
                {
                    shuffledIndices = Enumerable.Range(0, messages.Length).OrderBy(x => rand.Next()).ToList();

                    if (shuffledIndices[0] == lastIndex && messages.Length > 1)
                    {
                        int swapIndex = rand.Next(1, shuffledIndices.Count);
                        (shuffledIndices[0], shuffledIndices[swapIndex]) = (shuffledIndices[swapIndex], shuffledIndices[0]);
                    }
                }

                int index = shuffledIndices[0];
                shuffledIndices.RemoveAt(0);
                lastIndex = index;

                var (msg, type) = messages[index];
                sapi.SendMessageToGroup(0, msg, type);
                lastMessageSent = msg;
            }
        }
    }
}