@echo off
setlocal
cls
powershell -Command "& { Start-Transcript %~dp0runbuild.txt; Import-Module %~dp0..\Tools\PSake\psake.psm1; Invoke-psake %~dp0..\Build\build.ps1 %*; Stop-Transcript; }"
endlocal
