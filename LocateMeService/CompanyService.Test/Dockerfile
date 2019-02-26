FROM microsoft/dotnet:2.2-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY CompanyService.Test/CompanyService.Test.csproj CompanyService.Test/
RUN dotnet restore CompanyService.Test/CompanyService.Test.csproj
COPY . .
WORKDIR /src/CompanyService.Test
RUN dotnet build CompanyService.Test.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish CompanyService.Test.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CompanyService.Test.dll"]
