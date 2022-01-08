# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/runtime:6.0.1-bullseye-slim-arm64v8 as runtime


# Copy csproj and restore as distinct layers
WORKDIR /App
COPY app/ ./

ENTRYPOINT ["dotnet", "run.dll"]