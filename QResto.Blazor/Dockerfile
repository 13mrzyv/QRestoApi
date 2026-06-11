# 1-ci Mərhələ: Layihəni SDK daxilində build etmək
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["QResto.Blazor/QResto.Blazor.csproj", "QResto.Blazor/"]
COPY ["QResto.Shared/QResto.Shared.csproj", "QResto.Shared/"]

RUN dotnet restore "QResto.Blazor/QResto.Blazor.csproj"

COPY . .
WORKDIR "/src/QResto.Blazor"
RUN dotnet publish "QResto.Blazor.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# 2-ci Mərhələ: Statik faylları Nginx serveri ilə ayağa qaldırmaq
FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html

COPY --from=build /app/publish/wwwroot .

# Kənar fayl olmadan birbaşa konfiqurasiya yazırıq
RUN echo 'server { listen 80; location / { root /usr/share/nginx/html; try_files $uri $uri/ /index.html =404; } }' > /etc/nginx/conf.d/default.conf

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]