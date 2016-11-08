@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)

set version=
if not "%PackageVersion%" == "" (
   set version=-Version %PackageVersion%
)

REM Build
"%programfiles(x86)%\MSBuild\14.0\Bin\MSBuild.exe" Zanganeh.RobotsTxtHandler.sln /p:Configuration="%config%" /p:Platform="Any CPU" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false

REM Package
mkdir Build
call ".nuget\nuget.exe" pack "Zanganeh.RobotsTxtHandler\Zanganeh.RobotsTxtHandler.csproj" -IncludeReferencedProjects -o Build -p Configuration=%config% %version%