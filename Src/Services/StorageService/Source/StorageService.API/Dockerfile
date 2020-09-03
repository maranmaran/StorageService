FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR ./

COPY Shared/Common/ Shared/Common/

COPY Services/StorageService/Source/StorageService.API/ Services/StorageService/Source/StorageService.API/
COPY Services/StorageService/Source/StorageService.Business/ Services/StorageService/Source/StorageService.Business/
COPY Services/StorageService/Source/StorageService.Persistence/ Services/StorageService/Source/StorageService.Persistence/
COPY Services/StorageService/Source/StorageService.Domain/ Services/StorageService/Source/StorageService.Domain/

RUN dotnet restore Services/StorageService/Source/StorageService.API/StorageService.API.csproj 

COPY . .
WORKDIR /Services/StorageService/Source/StorageService.API
RUN dotnet publish --no-restore -c Release -o /app

FROM build as tests_business
WORKDIR /Services/StorageService/Tests/Tests.Business

FROM build as tests_functional
WORKDIR /Services/StorageService/Tests/Tests.FunctionalTests

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "StorageService.API.dll"]
