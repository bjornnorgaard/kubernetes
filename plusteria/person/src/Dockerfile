FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS base
WORKDIR /app

COPY *.sln ./
COPY */*.csproj ./

RUN for file in $(ls *.csproj); do mkdir -p ${file%.*}/ && mv $file ${file%.*}/; done
RUN dotnet restore

FROM base AS build
COPY . .
RUN dotnet build

# FROM build AS test
# RUN dotnet test

FROM build AS publish
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS runtime
RUN apk add icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
COPY --from=publish /app/out .
ENTRYPOINT [ "dotnet", "Api.Web.dll" ]



