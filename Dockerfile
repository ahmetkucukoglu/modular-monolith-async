FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Bootstrapper/Ecommerce.Bootstrapper/Ecommerce.Bootstrapper.csproj", "src/Bootstrapper/Ecommerce.Bootstrapper/"]
COPY ["src/Modules/Inventory/Ecommerce.Inventory/Ecommerce.Inventory.csproj", "src/Modules/Inventory/Ecommerce.Inventory/"]
COPY ["src/Shared/Ecommerce.Shared/Ecommerce.Shared.csproj", "src/Shared/Ecommerce.Shared/"]
COPY ["src/Shared/Ecommerce.Shared.Abstractions/Ecommerce.Shared.Abstractions.csproj", "src/Shared/Ecommerce.Shared.Abstractions/"]
COPY ["src/Modules/Inventory/Ecommerce.Inventory.Core/Ecommerce.Inventory.Core.csproj", "src/Modules/Inventory/Ecommerce.Inventory.Core/"]
COPY ["src/Modules/Inventory/Ecommerce.Inventory.Shared/Ecommerce.Inventory.Shared.csproj", "src/Modules/Inventory/Ecommerce.Inventory.Shared/"]
COPY ["src/Modules/Inventory/Ecommerce.Inventory.Infrastructure/Ecommerce.Inventory.Infrastructure.csproj", "src/Modules/Inventory/Ecommerce.Inventory.Infrastructure/"]
COPY ["src/Modules/Order/Ecommerce.Order/Ecommerce.Order.csproj", "src/Modules/Order/Ecommerce.Order/"]
COPY ["src/Modules/Order/Ecommerce.Order.Core/Ecommerce.Order.Core.csproj", "src/Modules/Order/Ecommerce.Order.Core/"]
COPY ["src/Modules/Payment/Ecommerce.Payment.Shared/Ecommerce.Payment.Shared.csproj", "src/Modules/Payment/Ecommerce.Payment.Shared/"]
COPY ["src/Modules/Order/Ecommerce.Order.Infrastructure/Ecommerce.Order.Infrastructure.csproj", "src/Modules/Order/Ecommerce.Order.Infrastructure/"]
COPY ["src/Modules/Payment/Ecommerce.Payment/Ecommerce.Payment.csproj", "src/Modules/Payment/Ecommerce.Payment/"]
COPY ["src/Modules/Payment/Ecommerce.Payment.Core/Ecommerce.Payment.Core.csproj", "src/Modules/Payment/Ecommerce.Payment.Core/"]
COPY ["src/Modules/Payment/Ecommerce.Payment.Infrastructure/Ecommerce.Payment.Infrastructure.csproj", "src/Modules/Payment/Ecommerce.Payment.Infrastructure/"]
COPY ["src/Modules/OrderSaga/Ecommerce.OrderSaga/Ecommerce.OrderSaga.csproj", "src/Modules/OrderSaga/Ecommerce.OrderSaga/"]
RUN dotnet restore "src/Bootstrapper/Ecommerce.Bootstrapper/Ecommerce.Bootstrapper.csproj"
COPY . .
WORKDIR "/src/src/Bootstrapper/Ecommerce.Bootstrapper"
RUN dotnet build "Ecommerce.Bootstrapper.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ecommerce.Bootstrapper.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ecommerce.Bootstrapper.dll"]
