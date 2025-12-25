@echo off
echo Starting Smart Bazaar Full Stack Application...
echo.

REM Start Backend API
echo [1/2] Starting Backend API on port 5000...
start "Smart Bazaar API" cmd /k "cd /d %~dp0SmartBazaar.API && dotnet run --urls http://localhost:5000"

REM Wait for backend to start
timeout /t 5 /nobreak > nul

REM Start Frontend
echo [2/2] Starting Frontend on port 3000...
start "Smart Bazaar Frontend" cmd /k "cd /d %~dp0SmartBazaar.API && npm run dev"

echo.
echo ========================================
echo Smart Bazaar is starting!
echo.
echo Backend API: http://localhost:5000
echo Frontend App: http://localhost:3000
echo.
echo Both servers will open in separate windows.
echo Press any key to close this window...
echo ========================================
pause > nul
