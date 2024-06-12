@echo off
cd Sources
cd WebAPI\bin\Debug\net8.0
start %1WebAPI.exe
cd ..\..\..\..\
cd WebApp\bin\Debug\net8.0
start %1WebApp.exe
exit