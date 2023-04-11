using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRS.Core.Events;

namespace Post.Common.Events
{
    public class PostRemovedEvent : BaseEvent
    {
        public PostRemovedEvent(): base(nameof(PostRemovedEvent))
        {

        }
    }
}