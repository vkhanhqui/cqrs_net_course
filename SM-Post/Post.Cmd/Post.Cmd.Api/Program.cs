using Confluent.Kafka;
using CQRS.Core.Domain;
using CQRS.Core.Handlers;
using CQRS.Core.Infrastructure;
using CQRS.Core.Producers;
using Post.Cmd.Api.Commands;
using Post.Cmd.Domain.Aggregates;
using Post.Cmd.Infrastructure.Config;
using Post.Cmd.Infrastructure.Dispatchers;
using Post.Cmd.Infrastructure.Handlers;
using Post.Cmd.Infrastructure.Producers;
using Post.Cmd.Infrastructure.Repositories;
using Post.Cmd.Infrastructure.Stores;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<MongoDbConfig>( // Read configs from appsettings.Development.json
    builder.Configuration.GetSection(nameof(MongoDbConfig)));
builder.Services.Configure<ProducerConfig>( // Read configs from appsettings.Development.json
    builder.Configuration.GetSection(nameof(ProducerConfig)));
// Have to be in order because they're dependence
// Scoped: create a new instance for each unique HTTP request
// Singleton: create a single instance for the entire application
// Transient: create a new instance everywhere we use it
builder.Services.AddScoped<IEventStoreRepository, EventStoreRepository>();
builder.Services.AddScoped<IEventProducer, EventProducer>();
builder.Services.AddScoped<IEventStore, EventStore>();
builder.Services.AddScoped<IEventSourcingHandler<PostAggregate>, EventSourcingHandler>();
builder.Services.AddScoped<ICommandHandler, CommandHandler>();

// Register command handler methods
var commandHandler = builder.Services.BuildServiceProvider().GetRequiredService<ICommandHandler>();
var dispatcher = new CommandDispatcher();
dispatcher.RegisterHandler<NewPostCmd>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<EditMessageCmd>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<LikePostCmd>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<AddCommentCmd>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<EditCommentCmd>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<RemoveCommentCmd>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<DeletePostCmd>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<RestoreReadDbCmd>(commandHandler.HandleAsync);
builder.Services.AddSingleton<ICommandDispatcher>(_ => dispatcher);

// Default
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
