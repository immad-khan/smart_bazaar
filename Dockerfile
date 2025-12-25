# 1. Use .NET 10 SDK to build
FROM mcr.microsoft.com/dotnet/nightly/sdk:10.0 AS build
WORKDIR /src
COPY . .
WORKDIR "/src/SmartBazaar.API"
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# 2. Use .NET 10 Runtime + Chrome for Selenium
FROM mcr.microsoft.com/dotnet/nightly/aspnet:10.0
WORKDIR /app

# Essential: Install Chrome for your Scrapers
RUN apt-get update && apt-get install -y \
    wget \
    gnupg \
    && wget -q -O - https://dl-ssl.google.com/linux/linux_signing_key.pub | apt-key add - \
    && sh -c 'echo "deb [arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main" >> /etc/apt/sources.list.d/google.list' \
    && apt-get update \
    && apt-get install -y google-chrome-stable \
    && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish .

# Azure-friendly port
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# IMPORTANT: Make sure this name matches your project's .dll file
ENTRYPOINT ["dotnet", "SmartBazaar.API.dll"]