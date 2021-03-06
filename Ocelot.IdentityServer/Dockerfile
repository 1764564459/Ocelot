#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-nanoserver-1809 AS build
WORKDIR /src
COPY ["Ocelot.IdentityServer/Ocelot.IdentityServer.csproj", "Ocelot.IdentityServer/"]
RUN dotnet restore "Ocelot.IdentityServer/Ocelot.IdentityServer.csproj"
COPY . .
WORKDIR "/src/Ocelot.IdentityServer"
RUN dotnet build "Ocelot.IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ocelot.IdentityServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ocelot.IdentityServer.dll"]