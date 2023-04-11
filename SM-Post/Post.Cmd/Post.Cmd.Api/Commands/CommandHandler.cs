using CQRS.Core.Handlers;
using Post.Cmd.Domain.Aggregates;

namespace Post.Cmd.Api.Commands
{
    public class CommandHandler : ICommandHandler
    {
        // Concrete Colleague: Mediator pattern
        private readonly IEventSourcingHandler<PostAggregate> _eventSourcingHandler;

        public CommandHandler(IEventSourcingHandler<PostAggregate> eventSourcingHandler)
        {
            _eventSourcingHandler = eventSourcingHandler;
        }

        public async Task HandleAsync(NewPostCmd command)
        {
            var aggregate = new PostAggregate(command.Id, command.Author, command.Message);

            await _eventSourcingHandler.SaveAsync(aggregate);
        }

        public async Task HandleAsync(EditMessageCmd command)
        {
            var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
            aggregate.EditMessage(command.Message);

            await _eventSourcingHandler.SaveAsync(aggregate);
        }

        public async Task HandleAsync(LikePostCmd command)
        {
            var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
            aggregate.LikePost();

            await _eventSourcingHandler.SaveAsync(aggregate);
        }

        public async Task HandleAsync(AddCommentCmd command)
        {
            var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
            aggregate.AddComment(command.Comment, command.Username);

            await _eventSourcingHandler.SaveAsync(aggregate);
        }

        public async Task HandleAsync(EditCommentCmd command)
        {
            var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
            aggregate.EditComment(command.CommentId, command.Comment, command.Username);

            await _eventSourcingHandler.SaveAsync(aggregate);
        }

        public async Task HandleAsync(RemoveCommentCmd command)
        {
            var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
            aggregate.RemoveComment(command.CommentId, command.Username);

            await _eventSourcingHandler.SaveAsync(aggregate);
        }

        public async Task HandleAsync(DeletePostCmd command)
        {
            var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
            aggregate.DeletePost(command.Username);

            await _eventSourcingHandler.SaveAsync(aggregate);
        }
    }
}