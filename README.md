# 🎟️ Proyecto Software - Sistema de Gestión de Eventos

Este proyecto es una API desarrollada en **.NET 8** para la gestión de eventos, sectores y reserva de asientos (butacas).  
Permite consultar información de eventos, visualizar disponibilidad, realizar reservas temporales, procesar pagos, liberar reservas vencidas automáticamente y auditar acciones críticas del sistema.

---

## 🧠 Arquitectura

El sistema está estructurado siguiendo principios de **Clean Architecture** junto con el patrón **CQRS (Command Query Responsibility Segregation)**.

### 📁 Application
Contiene la lógica de la aplicación:
- DTOs → objetos de respuesta al cliente
- Interfaces → contratos de repositorios
- UseCases:
  - Commands → acciones (ej: reservar asiento)
  - Queries → consultas
  - Handlers → lógica que ejecuta cada caso de uso

---

### 📁 Domain
Contiene el modelo del negocio:
- Entidades principales (Eventos, Sectores, Asientos, etc.)
- Reglas del sistema

---

### 📁 Infrastructure
Contiene la implementación técnica:
- Uso de AppDbContext para modelado de tablas y sus relaciones
- EventRepository para la comunicacion de BD a los Commands, los Handlers y Queries
- Entity Framework Core
- SQLite
- Background Services para procesamiento automático de reservas vencidas

---

## ⚡ Concurrencia

El sistema implementa control de concurrencia optimista mediante el uso de un campo `Version` en los asientos.

Esto evita que dos usuarios puedan reservar simultáneamente el mismo asiento.

En caso de conflicto, la API devuelve:

- HTTP 409 Conflict

---

## 🧾 Auditoría

Todas las acciones críticas del sistema son registradas en la tabla `AUDIT_LOG`.

Ejemplos:
- RESERVE_SUCCESS
- RESERVE_FAILED
- PAYMENT_SUCCESS
- RESERVATION_EXPIRED

Cada registro almacena:
- usuario;
- acción;
- entidad afectada;
- timestamp exacto.

---

## ⏳ Background Jobs

El sistema ejecuta tareas automáticas en segundo plano mediante `BackgroundService`.

Actualmente se utiliza para:
- detectar reservas vencidas;
- liberar asientos automáticamente;
- registrar auditoría.

---

## 🚀 Ejecución del proyecto

1. Abrir la solución en Visual Studio  
2. Ejecutar el proyecto (F5 o botón "Run")  
3. Acceder a las siguientes URLs  

---

## 🌐 Accesos

### 🔹 Swagger (prueba de API)
https://localhost:7210/swagger/index.html

Permite probar los endpoints:
- GET (consultas)
- POST (acciones)

---

### 🔹 Frontend
https://localhost:7210/index.html

Interfaz web que consume la API mediante JavaScript (fetch) para mostrar y manipular datos.

---

## 🔄 Flujo del sistema

1. El usuario interactúa con el frontend  
2. El frontend realiza requests a la API  
3. La API procesa:
   - Queries → consultas de datos  
   - Commands → acciones (ej: reservar)  
4. Se devuelve una respuesta  
5. El frontend renderiza la información  

---

## 📌 Funcionalidades principales

- Obtener eventos
- Consultar sectores por evento
- Visualizar estado de asientos
- Reservar asientos temporalmente
- Procesar pagos
- Liberación automática de reservas vencidas
- Auditoría de acciones
- Manejo de concurrencia

---

## 🛠️ Tecnologías utilizadas

- .NET 8  
- ASP.NET Core  
- Swagger  
- Arquitectura Clean + CQRS  
- Entity Framework Core
- SQLite

---

## 📈 Estado del proyecto

Backend funcional 🚀

Características implementadas:
- API REST
- Sistema de reservas
- Procesamiento de pagos
- Control de concurrencia
- Auditoría
- Expiración automática
- Background workers

Frontend en desarrollo 🚧

---

## 👨‍💻 Autor

Proyecto desarrollado como práctica para la materia **Proyecto de Software**.
- Juan Bautista Lechares
- Juan Cruz Merino