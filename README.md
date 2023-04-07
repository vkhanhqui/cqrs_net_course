# cqrs_net_course
Follow this course: https://www.udemy.com/course/net-microservices-cqrs-event-sourcing-with-kafka/


# Download Client Tools – Robo 3T:
https://robomongo.org/download


# Start project from scratch

```bash
docker-compose up -d

# CQRS-ES
dotnet new classlib -o CQRS-ES/CQRS.Core

# SM-Post
dotnet new sln -o SM-Post

# SM-Post/Post.Cmd
dotnet new webapi -o SM-Post/Post.Cmd/Post.Cmd.Api
dotnet new classlib -o SM-Post/Post.Cmd/Post.Cmd.Domain
dotnet new classlib -o SM-Post/Post.Cmd/Post.Cmd.Infrastructure

# SM-Post/Post.Query
dotnet new webapi -o SM-Post/Post.Query/Post.Query.Api
dotnet new classlib -o SM-Post/Post.Query/Post.Query.Domain
dotnet new classlib -o SM-Post/Post.Query/Post.Query.Infrastructure

# Add projects SM-Post.sln
cd SM-Post/
dotnet sln add ../CQRS-ES/CQRS.Core/CQRS.Core.csproj
dotnet sln add Post.Cmd/Post.Cmd.Api/Post.Cmd.Api.csproj
dotnet sln add Post.Cmd/Post.Cmd.Domain/Post.Cmd.Domain.csproj
dotnet sln add Post.Cmd/Post.Cmd.Infrastructure/Post.Cmd.Infrastructure.csproj
dotnet sln add Post.Query/Post.Query.Api/Post.Query.Api.csproj
dotnet sln add Post.Query/Post.Query.Domain/Post.Query.Domain.csproj
dotnet sln add Post.Query/Post.Query.Infrastructure/Post.Query.Infrastructure.csproj

```