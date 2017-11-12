@echo Off
cd %~dp0
setlocal EnableDelayedExpansion

set DOTNET_SCRIPT_VERSION=0.15.0
set NUGET_VERSION=4.3.0

set SCRIPT=%~n0.csx
set DOTNET_SCRIPT_URI=https://github.com/filipw/dotnet-script/releases/download/%DOTNET_SCRIPT_VERSION%/dotnet-script.%DOTNET_SCRIPT_VERSION%.zip
set DOTNET_SCRIPT_CMD_DIR=.dotnet-script\v%DOTNET_SCRIPT_VERSION%\
set DOTNET_SCRIPT_CMD=%DOTNET_SCRIPT_CMD_DIR%dotnet-script\dotnet-script.cmd
set DOTNET_SCRIPT_CACHE_DIR=%LOCALAPPDATA%\%DOTNET_SCRIPT_CMD_DIR%
set DOTNET_SCRIPT_CACHE=%DOTNET_SCRIPT_CACHE_DIR%dotnet-script.%DOTNET_SCRIPT_VERSION%.zip
set NUGET_URI=https://dist.nuget.org/win-x86-commandline/v%NUGET_VERSION%/NuGet.exe
set NUGET_EXE_DIR=.nuget\v%NUGET_VERSION%\
set NUGET_EXE=%NUGET_EXE_DIR%NuGet.exe
set NUGET_CACHE_DIR=%LOCALAPPDATA%\%NUGET_EXE_DIR%
set NUGET_CACHE=%LOCALAPPDATA%\%NUGET_EXE%
set PACKAGES_CONFIG=packages.config
set PACKAGES_DIR=packages\

if not exist %NUGET_EXE% (
  if not exist %NUGET_CACHE% (
    if not exist %NUGET_CACHE_DIR% mkdir %NUGET_CACHE_DIR% || exit /b
    echo %~nx0: Downloading '%NUGET_URI%' to '%NUGET_CACHE%'...
    @powershell -NoProfile -ExecutionPolicy unrestricted -Command "Invoke-WebRequest '%NUGET_URI%' -OutFile '%NUGET_CACHE%'" || exit /b
  )

  echo %~nx0: Copying '%NUGET_CACHE%' to '%NUGET_EXE_DIR%'...
  xcopy %NUGET_CACHE% %NUGET_EXE_DIR% /q || exit /b
)

if not exist %DOTNET_SCRIPT_CMD% (
  if not exist %DOTNET_SCRIPT_CACHE% (
    if not exist %DOTNET_SCRIPT_CACHE_DIR% mkdir %DOTNET_SCRIPT_CACHE_DIR% || exit /b %ERRORLEVEL%
    echo %~nx0: Downloading '%DOTNET_SCRIPT_URI%' to '%DOTNET_SCRIPT_CACHE%'...
    @powershell -NoProfile -ExecutionPolicy unrestricted -Command "Invoke-WebRequest '%DOTNET_SCRIPT_URI%' -OutFile '%DOTNET_SCRIPT_CACHE%'" || exit /b %ERRORLEVEL%
  )

  if not exist %DOTNET_SCRIPT_CMD_DIR% mkdir %DOTNET_SCRIPT_CMD_DIR% || goto :error
  echo Expanding '%DOTNET_SCRIPT_CACHE%' into '%DOTNET_SCRIPT_CMD_DIR%'...
  @powershell -NoProfile -ExecutionPolicy unrestricted -Command "Microsoft.PowerShell.Archive\Expand-Archive %DOTNET_SCRIPT_CACHE% -DestinationPath %DOTNET_SCRIPT_CMD_DIR% -Force"
)

echo %~nx0: Restoring NuGet packages for '%SCRIPT%'...
%NUGET_EXE% restore %PACKAGES_CONFIG% -PackagesDirectory %PACKAGES_DIR% || exit /b

echo %~nx0: Running '%SCRIPT%'...
%DOTNET_SCRIPT_CMD% %SCRIPT% %*
