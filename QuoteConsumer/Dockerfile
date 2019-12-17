FROM mcr.microsoft.com/dotnet/core/runtime:3.1-nanoserver-1903 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-1903 AS build
WORKDIR /src
COPY ["QuoteConsumer/QuoteConsumer.csproj", "QuoteConsumer/"]
RUN dotnet restore "QuoteConsumer/QuoteConsumer.csproj"
COPY . .
WORKDIR "/src/QuoteConsumer"
RUN dotnet build "QuoteConsumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QuoteConsumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QuoteConsumer.dll"]