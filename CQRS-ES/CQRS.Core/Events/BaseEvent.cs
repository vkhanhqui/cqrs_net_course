using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRS.Core.Messages;

namespace CQRS.Core.Events
{
    public abstract class BaseEvent: Message
    {
        public int Version {get; set;}
        public string Type {get; set;}

        protected BaseEvent(string type)
        {
            this.Type = type;
        }
    }
}