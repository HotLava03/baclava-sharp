using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace BaclavaSharp.Framework
{
    public abstract class Command
    {
        protected Command()
        {
            Aliases = new string[0];
        }

        public string Name { get; internal set; }
        public string Description { get; internal set; }
        public string[] Aliases { get; internal set; }

        internal async Task Execute(MessageCreateEventArgs e)
        {
            var cmdEvent = new CommandEvent(e.Message)
            {
                Author = e.Author,
                Guild = e.Guild,
                MentionedChannels = e.MentionedChannels,
                MentionedRoles = e.MentionedRoles,
                MentionedUsers = e.MentionedUsers
            };
            if (e.Author is DiscordMember member) cmdEvent.Member = member;

            var returned = await OnCommand(cmdEvent);
            if (!string.IsNullOrEmpty(returned)) await e.Channel.SendMessageAsync(returned);
        }

        protected abstract Task<string> OnCommand(CommandEvent e);
    }
}