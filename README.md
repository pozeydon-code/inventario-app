# Inventario App – Fullstack Microservicios

Este proyecto implementa una aplicación de gestión de productos y transacciones, desarrollado como evaluación técnica.

## 🧱 Arquitectura
- Backend con .NET 8 y Clean Architecture
  - Microservicio de Productos
  - Microservicio de Transacciones
- Frontend con React + Chakra UI + Vite
- Comunicación entre servicios vía HTTP REST
- Control de stock automatizado por microservicio de transacciones

---

## 🚀 Cómo correrlo localmente

### 🔧 Requisitos
- .NET 8 SDK
- Node.js (v18+)
- SQL Server (o SQLite para pruebas)
- Visual Studio / VS Code

---

### ▶️ 1. Clonar y restaurar dependencias
```bash
git clone https://github.com/pozeydon-code/inventario-app.git
cd inventario-app
```

### ▶️ 2. Backend – Productos

Se debe cambiar las cadenas de conexion de la base de datos `API/appsettings.json`

```bash
cd backend/ProductsService
dotnet restore
dotnet ef database update --project ProductsService.Infrastructure --startup-project ProductsService.API
dotnet run --project ProductsService.API
```

### ▶️ 3. Backend – Transacciones

Se debe cambiar las cadenas de conexion de la base de datos `API/appsettings.json`

Se debe cambiar la url del archivo Infrastructure/DependencyInjection.cs con la dirección del mmicroservicio de productos

```bash
cd backend/TransactionsService
dotnet restore
dotnet ef database update --project TransactionsService.Infrastructure --startup-project TransactionsService.API
dotnet run --project TransactionsService.API
```

### ▶️ 4. Frontend – React
```bash
cd frontend/react-app
npm install
npm run dev
```
Tambien puede usarse bun para un mejor rendimiento

```bash
cd frontend/react-app
bun install
bun run dev
```

---

## 📦 Estructura

```
inventario-app/
│
├── backend/
│   ├── ProductsService/
│   └── TransactionsService/
│
└── frontend/
    └── react-app/
```

---

## 📌 Notas
- El stock de los productos se actualiza automáticamente desde el microservicio de transacciones.
---

## ✨ Autor
Desarrollado por Francisco Herrera – Evaluación técnica
