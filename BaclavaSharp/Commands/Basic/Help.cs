using System.Threading.Tasks;
using BaclavaSharp.Framework;

namespace BaclavaSharp.Commands.Basic
{
    public class Help : Command
    {
        public Help()
        {
            Name = "help";
            Description = "Get some help.";
            Aliases = new[] {"plshelp"};
        }

        protected override Task<string> OnCommand(CommandEvent e)
        {
            return Task.FromResult("no help for you");
        }
    }
}