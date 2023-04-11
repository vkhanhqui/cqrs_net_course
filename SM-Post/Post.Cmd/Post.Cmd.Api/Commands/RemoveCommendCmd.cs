using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRS.Core.Commands;

namespace Post.Cmd.Api.Commands
{
    public class RemoveCommendCmd : BaseCommand
    {
        public Guid CommentId {get; set;}
        public string Username {get; set;}
    }
}