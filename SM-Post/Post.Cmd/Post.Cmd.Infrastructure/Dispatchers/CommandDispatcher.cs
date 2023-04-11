using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRS.Core.Commands;
using CQRS.Core.Infrastructure;

namespace Post.Cmd.Infrastructure.Dispatchers
{
    public class CommandDispatcher : ICommandDispatcher
    {
        // Concrete Mediator: Mediator pattern
        private readonly Dictionary<Type, Func<BaseCommand, Task>> _handlers = new();

        public void RegisterHandler<T>(Func<T, Task> handler) where T : BaseCommand
        {
            if(_handlers.ContainsKey(typeof(T)))
            {
                throw new IndexOutOfRangeException("Could not register the same command handler");
            }
            _handlers.Add(typeof(T), x => handler((T) x)); // Cast BaseCommand (x) to specific Command (T)
        }

        public async Task SendAsync(BaseCommand command)
        {
            bool isRegisted = _handlers.TryGetValue(
                command.GetType(), out Func<BaseCommand, Task> handler);
            // command must be registed in _handlers before sending
            if(isRegisted)
            {
                await handler(command);
            }
            else
            {
                throw new ArgumentNullException(nameof(handler), "No command handler was registed");
            }
        }
    }
}