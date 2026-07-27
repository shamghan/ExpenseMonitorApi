FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["src/ExpenseMonitor.API/ExpenseMonitor.API.csproj", "src/ExpenseMonitor.API/"]
COPY ["src/ExpenseMonitor.Application/ExpenseMonitor.Application.csproj", "src/ExpenseMonitor.Application/"]
COPY ["src/ExpenseMonitor.Infrastructure/ExpenseMonitor.Infrastructure.csproj", "src/ExpenseMonitor.Infrastructure/"]
COPY ["src/ExpenseMonitor.Domain/ExpenseMonitor.Domain.csproj", "src/ExpenseMonitor.Domain/"]
RUN dotnet restore "src/ExpenseMonitor.API/ExpenseMonitor.API.csproj"

COPY . .
WORKDIR /src/src/ExpenseMonitor.API
RUN dotnet publish "ExpenseMonitor.API.csproj" -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "ExpenseMonitor.API.dll"]
