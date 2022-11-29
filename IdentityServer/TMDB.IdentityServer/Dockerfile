FROM mcr.microsoft.com/dotnet/sdk:3.1 as build
WORKDIR /app
EXPOSE 80
COPY Shared/TMDB.Shared/*.csproj Shared/TMDB.Shared/
COPY IdentityServer/TMDB.IdentityServer/*.csproj IdentityServer/TMDB.IdentityServer/ 
RUN dotnet restore  IdentityServer/TMDB.IdentityServer/*.csproj
COPY . .
RUN dotnet publish IdentityServer/TMDB.IdentityServer/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:3.1 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","TMDB.IdentityServer.dll" ]




