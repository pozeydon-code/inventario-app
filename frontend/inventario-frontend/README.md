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

### ▶️ Frontend – React

```bash
cd frontend/react-app
npm install
npm run dev
```

Tambien puedes usar bun para un mejor rendimiento (recomendado)

```bash
cd frontend/react-app
bun install
bun run dev
```

---

## 🔁 Configuración de URLs del Frontend

Las URL base para consumir los microservicios desde el frontend están centralizadas en:

```
frontend/react-app/src/api/url.ts
```

Desde ahí puedes cambiar fácilmente las rutas hacia los microservicios de productos o transacciones según el entorno en el que se despliegue.

```ts
// url.ts
export const BASE_PRODUCT_API = "http://localhost:5106/api";
export const BASE_TRANSACTION_API = "http://localhost:5107/api";
```

Esto facilita mantener y modificar las rutas sin tener que buscarlas en cada componente.

---
