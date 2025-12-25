# Smart Bazaar - Full Stack Application
start krny k liay run kro
cd d:\Desktop\smart_bazaar
.\start.ps1
Start the backend (in one terminal):
cd d:\Desktop\smart_bazaar\SmartBazaar.API
dotnet run
Start the frontend (in another terminal):
cd d:\Desktop\smart_bazaar\SmartBazaar.API
npm install  # if not already done
npm run dev
## Project Structure
- **Backend**: .NET 10.0 API (Port 5000)
- **Frontend**: React + Vite (Port 3000)

## Getting Started

### 1. Install Frontend Dependencies
```bash
npm install
```

### 2. Start the Backend API
```bash
dotnet run --urls "http://localhost:5000"
```

### 3. Start the Frontend (in a new terminal)
```bash
npm run dev
```

### 4. Open Application
Navigate to: http://localhost:3000

## Available Scripts

### Frontend
- `npm run dev` - Start development server
- `npm run build` - Build for production
- `npm run preview` - Preview production build

### Backend
- `dotnet run` - Start API server
- `dotnet build` - Build the project
- `dotnet watch run` - Start with hot reload

## API Endpoints
- `GET /api/health` - Health check
- `GET /api/scraper/search?q=query` - Search products
- `GET /api/scraper/test-naheed?url=productUrl` - Scrape single product

## Features
- 🔍 Unified search across Pakistani markets
- 🎨 Beautiful 3D animated background
- 📱 Responsive design
- ⚡ Fast API with real-time scraping
- 🛒 Direct product links
 
 🚀 How to Run
cd SmartBazaar.API
npm install
npm run dev1