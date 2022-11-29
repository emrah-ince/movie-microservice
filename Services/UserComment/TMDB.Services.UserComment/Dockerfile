FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/TMDB.Shared/*.csproj Shared/TMDB.Shared/
COPY Services/UserComment/TMDB.Services.UserComment/*.csproj Services/UserComment/TMDB.Services.UserComment/
RUN dotnet restore Services/UserComment/TMDB.Services.UserComment/*.csproj
COPY . .
RUN dotnet publish Services/UserComment/TMDB.Services.UserComment/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","TMDB.Services.UserComment.dll" ]
