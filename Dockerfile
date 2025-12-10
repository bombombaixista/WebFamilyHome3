# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o csproj e restaura dependências
COPY WebFamilyHome/*.csproj ./WebFamilyHome/
RUN dotnet restore WebFamilyHome/WebFamilyHome.csproj

# Copia todo o código e compila
COPY . .
WORKDIR /src/WebFamilyHome
RUN dotnet publish WebFamilyHome.csproj -c Release -o /app/publish

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Railway define a porta via variável de ambiente PORT
ENV ASPNETCORE_URLS=http://+:${PORT}
ENTRYPOINT ["dotnet", "WebFamilyHome.dll"]
