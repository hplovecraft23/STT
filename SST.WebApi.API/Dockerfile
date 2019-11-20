#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-nanoserver-1809 AS build
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