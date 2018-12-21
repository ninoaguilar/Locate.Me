FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY CompanyService/CompanyService.csproj CompanyService/
RUN dotnet restore CompanyService/CompanyService.csproj
COPY . .
WORKDIR /src/CompanyService
RUN dotnet build CompanyService.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish CompanyService.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CompanyService.dll"]
