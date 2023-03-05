#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NexleInterviewTesting.Api/NexleInterviewTesting.Api.csproj", "NexleInterviewTesting.Api/"]
COPY ["NexleInterviewTesting.Application/NexleInterviewTesting.Application.csproj", "NexleInterviewTesting.Application/"]
COPY ["NexleInterviewTesting.Domain/NexleInterviewTesting.Domain.csproj", "NexleInterviewTesting.Domain/"]
COPY ["NexleInterviewTesting.Infrastructure/NexleInterviewTesting.Infrastructure.csproj", "NexleInterviewTesting.Infrastructure/"]

RUN dotnet restore "NexleInterviewTesting.Api/NexleInterviewTesting.Api.csproj"

COPY . .
COPY NexleInterviewTesting.Api/appsettings.docker.json NexleInterviewTesting.Api/appsettings.json

# RUN dotnet tool install --global dotnet-ef

# ENV PATH="$PATH:/root/.dotnet/tools"

# RUN dotnet ef database update --project "NexleInterviewTesting.Infrastructure/NexleInterviewTesting.Infrastructure.csproj" --startup-project "NexleInterviewTesting.Api/NexleInterviewTesting.Api.csproj"
WORKDIR "/src/NexleInterviewTesting.Api"
RUN dotnet build "NexleInterviewTesting.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NexleInterviewTesting.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NexleInterviewTesting.Api.dll"]
