using System;
using System.Collections.Generic;
using System.Linq;
using DSharpPlus.Entities;

namespace BaclavaSharp.Framework
{
    public class CommandEvent : EventArgs
    {
        public CommandEvent(DiscordMessage message)
        {
            Message = message;
            Channel = Message.Channel;
            var argsRaw = Message.Content.Substring(2).Split(' ');
            Args = argsRaw.Skip(1).ToArray();
            Label = argsRaw[0];
        }

        public DiscordMessage Message { get; }
        public DiscordChannel Channel { get; }
        public string[] Args { get; }
        public string Label { get; }
        public DiscordUser Author { get; internal set; }
        public DiscordMember Member { get; internal set; }
        public DiscordGuild Guild { get; internal set; }
        public IReadOnlyList<DiscordRole> MentionedRoles { get; internal set; }
        public IReadOnlyList<DiscordChannel> MentionedChannels { get; internal set; }
        public IReadOnlyList<DiscordUser> MentionedUsers { get; internal set; }
    }
}