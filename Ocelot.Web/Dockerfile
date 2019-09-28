#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-nanoserver-1809 AS build
WORKDIR /src
COPY ["Ocelot.Web/Ocelot.Web.csproj", "Ocelot.Web/"]
RUN dotnet restore "Ocelot.Web/Ocelot.Web.csproj"
COPY . .
WORKDIR "/src/Ocelot.Web"
RUN dotnet build "Ocelot.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ocelot.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ocelot.Web.dll"]