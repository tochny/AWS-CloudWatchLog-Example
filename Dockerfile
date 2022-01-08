# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env

WORKDIR /app

# Copy everything and build
COPY *.* ./
RUN dotnet publish -c Release -o ./out -r linux-arm64 '-p:AssemblyName=run'

# Build runtime image
FROM mcr.microsoft.com/dotnet/runtime:6.0.1-bullseye-slim-arm64v8
WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "run.dll"]