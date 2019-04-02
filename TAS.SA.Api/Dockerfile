FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY TAS.SA.Api/TAS.SA.Api.csproj TAS.SA.Api/
COPY TAS.SA.Dominio/TAS.SA.Dominio.csproj TAS.SA.Dominio/
COPY TAS.SA.Infra.EntityFramework/TAS.SA.Infra.EntityFramework.csproj TAS.SA.Infra.EntityFramework/
RUN dotnet restore TAS.SA.Api/TAS.SA.Api.csproj
COPY . .
WORKDIR /src/TAS.SA.Api
RUN dotnet build TAS.SA.Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish TAS.SA.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TAS.SA.Api.dll"]
