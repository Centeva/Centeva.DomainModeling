# Docker configuration for running tests

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /

COPY . ./

# Install the wait tool
ADD https://github.com/ufoscout/docker-compose-wait/releases/download/2.9.0/wait /wait
RUN /bin/bash -c 'ls -la /wait; chmod +x /wait; ls -la /wait'

# Install the report generator tool
RUN dotnet tool install dotnet-reportgenerator-globaltool --version 5.1.12 --tool-path /tools

CMD /wait \
    && dotnet test -f net6.0 /test/Centeva.DomainModeling.UnitTests/Centeva.DomainModeling.UnitTests.csproj --logger trx --collect:'XPlat Code Coverage' \
    && mv /test/Centeva.DomainModeling.UnitTests/TestResults/*/coverage.cobertura.xml /var/tmp/coverage.unit.cobertura.xml \
    && dotnet test -f net6.0 /test/Centeva.DomainModeling.IntegrationTests/Centeva.DomainModeling.IntegrationTests.csproj --logger trx --collect:'XPlat Code Coverage' \
    && mv /test/Centeva.DomainModeling.IntegrationTests/TestResults/*/coverage.cobertura.xml /var/tmp/coverage.efcoreintegration.cobertura.xml \
    && tools/reportgenerator -reports:/var/tmp/coverage.*.cobertura.xml -targetdir:/var/tmp/coverage -reporttypes:'TeamCitySummary'
