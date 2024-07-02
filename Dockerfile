# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

## Copy all projects to WORKDIR to restore all dependencies first
# COPY ./NuGet.Config /app/NuGet.Config
COPY ./DynamicSearch.Sample/src/Core.Api/*.csproj         /app/DynamicSearch.Sample/src/Core.Api/
COPY ./DynamicSearch.Sample/src/Core.Application/*.csproj /app/DynamicSearch.Sample/src/Core.Application/
COPY ./DynamicSearch.Sample/src/Core.Domain/*.csproj      /app/DynamicSearch.Sample/src/Core.Domain/
COPY ./DynamicSearch.Sample/src/Core.Persistence/*.csproj /app/DynamicSearch.Sample/src/Core.Persistence/
COPY ./DynamicSearch.Lib/*.csproj                         /app/DynamicSearch.Lib/

RUN dotnet restore /app/DynamicSearch.Sample/src/Core.Api/*.csproj /property:Configuration=Release -nowarn:msb3202,nu1503

# Copy the rest to the WORKDIR
COPY ./DynamicSearch.Sample/src/ /app/DynamicSearch.Sample/src/
COPY ./DynamicSearch.Lib/        /app/DynamicSearch.Lib/

RUN dotnet publish /app/DynamicSearch.Sample/src/Core.Api/*.csproj --no-restore -c Release -o /app/out/

# Stage 2: Serve the built application with Kestrel
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as final

ENV ASPNETCORE_URLS http://+:80

WORKDIR /app

COPY --from=build /app/out /app/

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

ENTRYPOINT ["dotnet", "Core.Api.dll"]

# Debug
# CMD ["tail", "-f", "/dev/null"]