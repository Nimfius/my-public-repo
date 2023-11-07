FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
#COPY ["Postre", "./Postre"]
#COPY ["Postre", "./Postre"]
COPY [".", "./Postre"]
RUN dotnet restore "Postre/Postre.csproj"
COPY . .
WORKDIR "/src/Postre"
RUN dotnet build "Postre.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Postre.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Postre.dll"]
