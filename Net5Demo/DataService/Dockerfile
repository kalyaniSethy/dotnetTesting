FROM mcr.microsoft.com/dotnet/sdk:5.0 AS base
WORKDIR /src
COPY ./DataService.csproj .
COPY . .
FROM base as toolchain
RUN dotnet nuget add source -n "Instana-Feed" https://instananugetserver.azurewebsites.net/nuget

FROM toolchain as build
RUN dotnet add DataService.csproj package Instana.Tracing.Core.Rewriter.Linux --version 1.195.5
RUN dotnet add DataService.csproj package Instana.Tracing.Core --version 1.195.5
RUN dotnet publish -c Release -o /app
COPY ./CoreProfiler.so /app/instana_tracing/CoreProfiler.so
FROM build as run
WORKDIR /app

ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV ASPNETCORE_ENVIRONMENT=Production

ENV CORECLR_ENABLE_PROFILING=1
ENV CORECLR_PROFILER={cf0d821e-299b-5307-a3d8-b283c03916dd}
ENV CORECLR_PROFILER_PATH=/app/instana_tracing/CoreProfiler.so
ENV DOTNET_STARTUP_HOOKS=/app/Instana.Tracing.Core.dll
ENV INSTANA_AGENT_HOST=172.17.0.1
ENV INSTANA_AGENT_PORT=42699
ENTRYPOINT ["dotnet", "DataService.dll"]


