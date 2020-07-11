using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BaclavaSharp.Framework;
using DSharpPlus;
using DSharpPlus.Entities;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace BaclavaSharp.Commands.Owner
{
    public class Eval : Command
    {
        public Eval()
        {
            Name = "cseval";
            Description = "Eval C# code";
            Aliases = new[] {"csharp"};
        }

        protected override async Task<string> OnCommand(CommandEvent e)
        {
            if (e.Author.Id != 362753440801095681) return null;
            var input = e.Message.Content;

            var index1 = input.IndexOf('\n', input.IndexOf("```", StringComparison.Ordinal) + 3) + 1;
            var index2 = input.LastIndexOf("```", StringComparison.Ordinal);

            if (index1 == -1 || index2 == -1) return "Wrap the code in a code block.";
            var code = input.Substring(index1, index2 - index1);
            try
            {
                var options = ScriptOptions.Default
                    .AddReferences(typeof(object).GetTypeInfo().Assembly.Location,
                        typeof(Enumerable).GetTypeInfo().Assembly.Location,
                        typeof(DiscordClient).GetTypeInfo().Assembly.Location,
                        typeof(DiscordMessage).GetTypeInfo().Assembly.Location)
                    .AddImports("DSharpPlus", "DSharpPlus.Entities", "DSharpPlus.EventArgs", "DSharpPlus.Net", "System",
                        "System.Linq",
                        "System.Collections", "System.Collections.Generic", "System.Threading.Tasks");

                var result = await CSharpScript.EvaluateAsync(code, options,
                    new RoslynGlobals
                    {
                        Client = Program.Client,
                        Channel = e.Channel
                    }
                );

                if (result != null) return result.ToString();
                await e.Message.CreateReactionAsync(DiscordEmoji.FromUnicode("✅"));
                return null;
            }
            catch (Exception ex)
            {
                await e.Message.CreateReactionAsync(DiscordEmoji.FromUnicode("❌"));
                await (await e.Member.CreateDmChannelAsync()).SendMessageAsync($"**{ex.GetType()}**: {ex.Message}");
            }

            return null;
        }

        public class RoslynGlobals
        {
            public DiscordClient Client { get; set; }
            public DiscordChannel Channel { get; set; }
        }
    }
}