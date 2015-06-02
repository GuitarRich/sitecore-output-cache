@echo Off
set config=%1
if "%config%" == "" (
   set config=Debug
)
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild src\Sitecore.SharedSource.OutputCache.sln /p:Configuration="%config%" /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false
