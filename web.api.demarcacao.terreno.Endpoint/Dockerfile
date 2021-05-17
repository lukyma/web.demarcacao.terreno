#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ./ ./
RUN dotnet restore "web.api.demarcacao.terreno.Endpoint/web.api.demarcacao.terreno.Endpoint.csproj"

RUN dotnet build "web.api.demarcacao.terreno.Endpoint/web.api.demarcacao.terreno.Endpoint.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "web.api.demarcacao.terreno.Endpoint/web.api.demarcacao.terreno.Endpoint.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

#ENV ASPNETCORE_URLS=http://+:$PORT

CMD ASPNETCORE_URLS=http://*:$PORT dotnet web.api.demarcacao.terreno.Endpoint.dll

#ENTRYPOINT ["dotnet", "web.api.demarcacao.terreno.Endpoint.dll"]