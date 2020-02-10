FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY WaitForRabbitMQ.Receiver/WaitForRabbitMQ.Receiver.csproj WaitForRabbitMQ.Receiver/
RUN dotnet restore "WaitForRabbitMQ.Receiver/WaitForRabbitMQ.Receiver.csproj"
COPY WaitForRabbitMQ.Receiver/ WaitForRabbitMQ.Receiver/
WORKDIR /src/WaitForRabbitMQ.Receiver
RUN dotnet build "WaitForRabbitMQ.Receiver.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WaitForRabbitMQ.Receiver.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WaitForRabbitMQ.Receiver.dll"]
