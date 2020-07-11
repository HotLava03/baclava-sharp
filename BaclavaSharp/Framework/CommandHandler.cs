using System;
using System.Collections.Generic;
using BaclavaSharp.Commands.Basic;
using BaclavaSharp.Commands.Owner;

namespace BaclavaSharp.Framework
{
    public class CommandHandler
    {
        private readonly List<Command> _commands;

        public CommandHandler()
        {
            _commands = new List<Command>();
        }

        public Command FindByAlias(string alias)
        {
            return _commands.Find(cmd => cmd.Name.ToLower().Equals(alias.ToLower()) 
                                         || Array.Find(cmd.Aliases,
                                             name => name.ToLower().Equals(alias.ToLower())) != null);
        }

        public void RegisterAll()
        {
            _commands.AddRange(new Command[]
            {
                // new Help(),
                new Eval()
            });
        }
    }
}