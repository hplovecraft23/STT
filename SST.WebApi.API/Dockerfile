FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["SST.WebApi.API/SST.WebApi.API.csproj", "SST.WebApi.API/"]
COPY ["STT.WebApi.Contract/STT.WebApi.Contract.csproj", "STT.WebApi.Contract/"]
COPY ["STT.WebApi.APIClient/STT.WebApi.APIClient.csproj", "STT.WebApi.APIClient/"]
COPY ["STT.WebApi.Data/STT.WebApi.Data.csproj", "STT.WebApi.Data/"]
RUN dotnet restore "SST.WebApi.API/SST.WebApi.API.csproj"
COPY . .
WORKDIR "/src/SST.WebApi.API"
RUN dotnet build "SST.WebApi.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SST.WebApi.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SST.WebApi.API.dll"]
