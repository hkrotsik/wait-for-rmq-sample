FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY WaitForRabbitMQ.Publisher/WaitForRabbitMQ.Publisher.csproj WaitForRabbitMQ.Publisher/
RUN dotnet restore "WaitForRabbitMQ.Publisher/WaitForRabbitMQ.Publisher.csproj"
COPY WaitForRabbitMQ.Publisher/ WaitForRabbitMQ.Publisher/
WORKDIR /src/WaitForRabbitMQ.Publisher
RUN dotnet build "WaitForRabbitMQ.Publisher.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WaitForRabbitMQ.Publisher.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WaitForRabbitMQ.Publisher.dll"]
