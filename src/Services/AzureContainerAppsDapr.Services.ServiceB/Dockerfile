FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /app

COPY src/. .

RUN dotnet restore Services/AzureContainerAppsDapr.Services.ServiceB
RUN dotnet publish Services/AzureContainerAppsDapr.Services.ServiceB -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

COPY --from=build /app/out .

ENV ASPNETCORE_URLS http://*:80
ENV ASPNETCORE_ENVIRONMENT Docker

ENTRYPOINT ["dotnet", "AzureContainerAppsDapr.Services.ServiceB.dll"]
