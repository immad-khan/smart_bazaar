# Start Smart Bazaar Full Stack Application

Write-Host "Starting Smart Bazaar Full Stack Application..." -ForegroundColor Cyan
Write-Host ""

# Start Backend API
Write-Host "[1/2] Starting Backend API on port 5000..." -ForegroundColor Green
$backendPath = Join-Path $PSScriptRoot "SmartBazaar.API"
Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd '$backendPath'; Write-Host 'Backend API Server' -ForegroundColor Yellow; dotnet run --urls 'http://localhost:5000'"

# Wait for backend to start
Start-Sleep -Seconds 5

# Start Frontend
Write-Host "[2/2] Starting Frontend on port 3000..." -ForegroundColor Green
Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd '$backendPath'; Write-Host 'Frontend Dev Server' -ForegroundColor Yellow; npm run dev"

Write-Host ""
Write-Host "=======================================" -ForegroundColor Cyan
Write-Host "Smart Bazaar is starting!" -ForegroundColor Green
Write-Host ""
Write-Host "Backend API: " -NoNewline
Write-Host "http://localhost:5000" -ForegroundColor Yellow
Write-Host "Frontend App: " -NoNewline
Write-Host "http://localhost:3000" -ForegroundColor Yellow
Write-Host ""
Write-Host "Both servers opened in separate windows." -ForegroundColor White
Write-Host "=======================================" -ForegroundColor Cyan
