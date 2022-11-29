FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/TMDB.Shared/*.csproj Shared/TMDB.Shared/
COPY Services/Mail/TMDB.Services.Mail/*.csproj Services/Mail/TMDB.Services.Mail/
RUN dotnet restore Services/Mail/TMDB.Services.Mail/*.csproj
COPY . .
RUN dotnet publish Services/Mail/TMDB.Services.Mail/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","TMDB.Services.Mail.dll" ]