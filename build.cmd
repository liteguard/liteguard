:: options
@echo Off
cd %~dp0
setlocal

:: set tool versions
set NUGET_VERSION=4.3.0
set DOTNET_SCRIPT_VERSION=0.15.0

:: determine nuget cache dir
set NUGET_CACHE_DIR=%LocalAppData%\.nuget\v%NUGET_VERSION%
set NUGET_LOCAL_DIR=.nuget\v%NUGET_VERSION%

:: download nuget to cache dir
set NUGET_URL=https://dist.nuget.org/win-x86-commandline/v%NUGET_VERSION%/NuGet.exe
if not exist %NUGET_CACHE_DIR%\NuGet.exe (
  if not exist %NUGET_CACHE_DIR% mkdir %NUGET_CACHE_DIR%
  echo Downloading '%NUGET_URL%'' to '%NUGET_CACHE_DIR%\NuGet.exe'...
  @powershell -NoProfile -ExecutionPolicy unrestricted -Command "$ProgressPreference = 'SilentlyContinue'; Invoke-WebRequest '%NUGET_URL%' -OutFile '%NUGET_CACHE_DIR%\NuGet.exe'"
)

:: copy nuget locally
if not exist %NUGET_LOCAL_DIR%\NuGet.exe (
  if not exist %NUGET_LOCAL_DIR% mkdir %NUGET_LOCAL_DIR%
  copy %NUGET_CACHE_DIR%\NuGet.exe %NUGET_LOCAL_DIR%\NuGet.exe > nul
)

:: restore packages for build script
echo Restoring NuGet packages for build script...
%NUGET_LOCAL_DIR%\NuGet.exe restore .\packages.choco.config -PackagesDirectory ./packages.choco -NoCache -DirectDownload -Source https://chocolatey.org/api/v2/
%NUGET_LOCAL_DIR%\NuGet.exe restore .\packages.config -PackagesDirectory ./packages

:: run build script
echo Running build script...
.\packages.choco\dotnet.script.%DOTNET_SCRIPT_VERSION%\Dotnet.Script\dotnet-script.cmd .\build.csx %*
