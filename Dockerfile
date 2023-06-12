ARG DOTNET_VERSION=7.0

FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS publish
WORKDIR /src
COPY . .
WORKDIR "/src/SecretStore.Web"
RUN dotnet publish "SecretStore.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SecretStore.Web.dll"]
