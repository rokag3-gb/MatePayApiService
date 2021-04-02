FROM mcr.microsoft.com/dotnet/sdk:5.0-windowsservercore-ltsc2019 AS vsbuildtoools

# Restore the default Windows shell for correct batch processing.
SHELL ["cmd", "/S", "/C"]

# Download the Build Tools bootstrapper.
ADD https://aka.ms/vs/16/release/vs_buildtools.exe "C:\TEMP\vs_buildtools.exe"

# Install Build Tools with the Microsoft.VisualStudio.Workload.AzureBuildTools workload, excluding workloads and components with known issues.
RUN C:\TEMP\vs_buildtools.exe --quiet --wait --norestart --nocache \
    --installPath C:\BuildTools \
    --add Microsoft.VisualStudio.Workload.AzureBuildTools \
    --add Microsoft.VisualStudio.Workload.MSBuildTools \
    --add Microsoft.VisualStudio.Workload.NetCoreBuildTools;includeRecommended \
    --add Microsoft.VisualStudio.Workload.WebBuildTools;includeRecommended \
    --remove Microsoft.VisualStudio.Component.Windows10SDK.10240 \
    --remove Microsoft.VisualStudio.Component.Windows10SDK.10586 \
    --remove Microsoft.VisualStudio.Component.Windows10SDK.14393 \
    --remove Microsoft.VisualStudio.Component.Windows81SDK \
 || IF "%ERRORLEVEL%"=="3010" EXIT 0

# Define the entry point for the docker container.
# This entry point starts the developer command prompt and launches the PowerShell shell.
SHELL ["C:\\BuildTools\\Common7\\Tools\\VsDevCmd.bat", "&&", "powershell.exe", "-NoLogo", "-ExecutionPolicy", "Bypass"]

FROM vsbuildtoools as build

WORKDIR /build
COPY ./ /build

RUN Start-Process -Wait regsvr32 -ArgumentList "C:/build/payment_com_libs/ep_cli_com.dll", "/s";

RUN msbuild \
    -t:restore \
    -t:build \
    -t:publish \
    /p:DeployOnBuild=true \
    /p:PublishProfile=FolderProfile \
    /p:Configuration=Release 

FROM mcr.microsoft.com/windows/servercore:20H2 AS runtime

SHELL ["powershell.exe", "-Command"]

WORKDIR /app
COPY --from=build C:/build/MatePayApiService/bin/Release/net5.0/publish/ .
COPY payment_com_libs/ep_cli_com.dll .
RUN Start-Process -Wait regsvr32 -ArgumentList "ep_cli_com.dll", "/s";

EXPOSE 5000
EXPOSE 5001

ENTRYPOINT [ "MatePayApiService.exe" ]
