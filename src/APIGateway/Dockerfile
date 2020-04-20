# specifies that we want to build on a Docker image made by Microsoft that contains the .NET core 3.1 SDK. We give this image the name build-env.
# In that image we want to create a working directory called /app ,so all commands following this one execute under this context.

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build-env
WORKDIR /app
 
# These instructions copy over the .csproj files into the build-env container's /app and restore the dependencies

COPY *.csproj ./
RUN dotnet restore
 
# Copies all project source files into the container.
# We then tell .NET to publish our app to a directory called out. The full path is /app/out since our working directory is /app.

COPY . ./
RUN dotnet publish -c Release -o out
 
# Build the runtime image
# We have written the dockerfile to perform a multi-stage build.
# When the dockerfile is executed, it will use the temporary container, build-env, with the .NET core 3.1 SDK to build the sample app
# and then copy the outputted binaries into another container containing only the .NET core 3.1 runtime so that we minimized the size of the final container.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out ./

ENTRYPOINT ["dotnet", "APIGateway.dll"]