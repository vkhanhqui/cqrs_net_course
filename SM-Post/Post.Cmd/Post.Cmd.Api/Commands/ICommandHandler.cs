namespace Post.Cmd.Api.Commands
{
    public interface ICommandHandler
    {
        // Abstract Colleague: Mediator pattern
        Task HandleAsync(NewPostCmd command);
        Task HandleAsync(EditMessageCmd command);
        Task HandleAsync(LikePostCmd command);
        Task HandleAsync(AddCommentCmd command);
        Task HandleAsync(EditCommentCmd command);
        Task HandleAsync(RemoveCommentCmd command);
        Task HandleAsync(DeletePostCmd command);
    }
}