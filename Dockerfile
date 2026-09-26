FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
<<<<<<< HEAD
COPY ["ATD_API/ATD_API.csproj","ATD_API/"] 
COPY ["Application/Application.csproj","Application/"] 
COPY ["Infrastructure/Infrastructure.csproj","Infrastructure/"] 
COPY ["Domain/Domain.csproj","Domain/"] 

=======
COPY ["ATD_API/ATD_API.csproj","ATD_API/"]
COPY ["Domain/Domain.csproj","Domain/"]
COPY ["Infrastructure/Infrastructure.csproj","Infrastructure/"]
COPY ["Application/Application.csproj","Application/"]
>>>>>>> b880c41b046599fa62f604e3b9b1facde32101ca
RUN dotnet restore "ATD_API/ATD_API.csproj"

COPY . .
RUN dotnet publish "ATD_API/ATD_API.csproj" \
   -c Release \
   -o /app/publish \
   /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_HTTP_PORTS=8080
COPY --from=build /app/publish .
ENTRYPOINT [ "dotnet","ATD_API.dll"]