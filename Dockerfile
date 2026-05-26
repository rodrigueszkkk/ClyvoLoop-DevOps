FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["PetHealthEcosystem.Api.csproj", "./"]
RUN dotnet restore "PetHealthEcosystem.Api.csproj"

COPY . .

RUN dotnet publish "PetHealthEcosystem.Api.csproj" \
    -c Release \
    -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

RUN groupadd --system appgroup && \
    useradd --system --no-create-home --gid appgroup appuser

COPY --from=build /app/publish .
RUN chown -R appuser:appgroup /app

USER appuser

EXPOSE 8080

ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://0.0.0.0:8080

ENTRYPOINT ["dotnet", "PetHealthEcosystem.Api.dll"]