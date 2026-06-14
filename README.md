# Proyecto Software - Sistema de Reserva de Eventos

Este proyecto corresponde a un sistema de reserva de asientos para eventos. Permite visualizar eventos disponibles, consultar sectores y butacas, reservar asientos por un tiempo limitado y confirmar el pago de una reserva.

El sistema está desarrollado con una API en ASP.NET Core y un frontend separado en HTML, CSS y JavaScript.

---

## Estructura del proyecto


Proyecto-Software/
│
├── Application/
│   ├── DTOs/
│   ├── Interfaces/
│   └── UseCases/
│
├── Domain/
│   └── Entities/
│
├── Infraestructure/
│   ├── BackgroundJobs/
│   ├── Migrations/
│   ├── Persistence/
│   └── Repositories/
│
├── Proyecto Software/
│   ├── Controllers/
│   ├── Properties/
│   ├── Program.cs
│   └── appsettings.json
│
├── Frontend/
│   ├── assets/
│   │   ├── css/
│   │   ├── img/
│   │   └── js/
│   ├── index.html
│   └── screenView.html
│
└── README.md


---

## Arquitectura utilizada

El proyecto está organizado siguiendo una separación por capas:

### Domain

Contiene las entidades principales del negocio:

* EVENT
* SECTOR
* SEAT
* RESERVATION
* USER
* AUDIT_LOG

### Application

Contiene la lógica de aplicación, DTOs, interfaces, comandos, queries y handlers.

Esta capa no depende directamente de Entity Framework Core. Para manejar transacciones se utiliza una abstracción propia mediante `IAppTransaction`.

### Infraestructure

Contiene la implementación de persistencia con Entity Framework Core:

* AppDbContext
* Repositorios
* Migraciones
* Background service para liberar reservas expiradas

### Proyecto Software

Es la API principal desarrollada en ASP.NET Core. Expone los endpoints REST y configura Swagger, CORS, inyección de dependencias y conexión a base de datos.

### Frontend

El frontend está separado del backend, como proyecto independiente. Se encuentra en la carpeta `/Frontend` y se ejecuta aparte mediante Live Server o un servidor local.

---

## Tecnologías utilizadas

* ASP.NET Core
* Entity Framework Core
* SQL Server LocalDB
* Swagger / OpenAPI
* HTML
* CSS
* JavaScript
* Live Server para ejecutar el frontend

---

## Configuración de base de datos

La conexión a base de datos se encuentra en:


Proyecto Software/appsettings.Development.json


Cadena de conexión utilizada:

json
"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=EventReservationDB;Trusted_Connection=True;MultipleActiveResultSets=true"


Para crear o actualizar la base de datos se puede ejecutar desde la consola del Administrador de paquetes NuGet:

powershell
Update-Database -Project Infraestructure -StartupProject "Proyecto Software"


---

## Cómo ejecutar el backend

1. Abrir la solución `Proyecto Software.sln` en Visual Studio.
2. Establecer como proyecto de inicio el proyecto `Proyecto Software`.
3. Ejecutar con el perfil `https`.
4. Swagger debería abrirse en:


https://localhost:7210/swagger


La API queda disponible en:


https://localhost:7210


---

## Cómo ejecutar el frontend

El frontend se encuentra en la carpeta:


Frontend/


Para ejecutarlo:

1. Abrir la carpeta `Frontend` con Visual Studio Code.
2. Instalar la extensión Live Server.
3. Abrir `index.html`.
4. Presionar `Go Live`.

El frontend debería abrirse en una URL similar a:


http://127.0.0.1:5500/index.html


El archivo `api.js` apunta al backend mediante:


const API_BASE_URL = "https://localhost:7210";


---

## CORS

El backend permite solicitudes desde los siguientes orígenes:


http://localhost:3000
http://127.0.0.1:5500
http://localhost:5500


Esto permite levantar el frontend separado del backend.

---

## Endpoints principales

### Eventos

Obtener eventos paginados:

http
GET /api/v1/events?Page=1&PageSize=10


Obtener sectores de un evento:

http
GET /api/v1/events/{eventId}/sectors


Obtener butacas de un sector:

http
GET /api/v1/events/{eventId}/sectors/{sectorId}/seats


---

### Reservas

Crear una reserva:

http
POST /api/v1/reservations


Ejemplo de body:

json
{
  "userId": 2,
  "seatId": "00000000-0000-0000-0000-000000000001"
}

Respuesta esperada:

json
{
  "reservationId": "guid",
  "userId": 2,
  "seatId": "guid",
  "expiresAt": "2026-06-14T07:39:00Z"
}


La reserva queda en estado `Pending` y vence a los 5 minutos.

---

### Pagos

Procesar pago de una reserva:

http
POST /api/v1/reservations/{reservationId}/payments


Respuesta esperada:

json
{
  "reservationId": "guid",
  "reservationStatus": "Paid",
  "seatId": "guid",
  "seatStatus": "Sold",
  "paidAt": "2026-06-14T08:00:00Z"
}


---

## Códigos HTTP utilizados

El proyecto distingue distintos escenarios mediante códigos HTTP:


200 OK        → Consulta o pago procesado correctamente.
201 Created   → Reserva creada correctamente.
400 BadRequest → Error general o solicitud inválida.
404 NotFound  → Asiento o recurso no encontrado.
409 Conflict  → Asiento ocupado, reserva expirada o reserva no disponible para pago.


---

## Flujo principal del sistema

1. El usuario visualiza los eventos disponibles.
2. Selecciona un evento.
3. El sistema muestra los sectores del evento.
4. El usuario selecciona una o más butacas disponibles.
5. Se crea una reserva en estado `Pending`.
6. El backend devuelve `expiresAt`.
7. El frontend muestra un timer sincronizado con `expiresAt`.
8. Si el usuario paga antes del vencimiento:

   * La reserva pasa a `Paid`.
   * La butaca pasa a `Sold`.
9. Si la reserva vence:

   * No puede ser pagada.
   * El sistema libera la butaca mediante el servicio de limpieza de reservas expiradas.

---

## Correcciones realizadas para la reentrega

Se realizaron las siguientes correcciones principales:

* Separación del frontend y backend.
* Eliminación del uso de `wwwroot` para servir el frontend desde la API.
* Eliminación de `app.UseStaticFiles()` en `Program.cs`.
* Corrección de rutas REST.
* Cambio de rutas de sectores y asientos a una jerarquía basada en eventos.
* Cambio de pagos a recurso hijo de reservas.
* Corrección de paginación en eventos.
* Mejora de códigos HTTP.
* Agregado de `expiresAt` en la respuesta de reserva.
* Sincronización del timer del frontend con el vencimiento real de la reserva.
* Validación para impedir pagar reservas expiradas.
* Eliminación del campo interno `version` en la respuesta pública de butacas.
* Reemplazo de `DateTime.Now` por `DateTime.UtcNow`.
* Eliminación de dependencia directa de Entity Framework Core desde la capa `Application`.
* Mejora responsive del frontend y del carrito flotante.

---

## Notas de funcionamiento

El carrito del frontend se mantiene en memoria mientras la página está abierta. Si se recarga la página, el carrito visual se pierde, pero la reserva sigue existiendo en el backend hasta que sea pagada o expire.

El backend es responsable de mantener la consistencia de las reservas, estados de butacas y pagos.

---

## Estado actual

El proyecto permite:

* Consultar eventos.
* Consultar sectores.
* Consultar butacas por sector.
* Reservar butacas.
* Ver el vencimiento real de una reserva.
* Pagar reservas pendientes.
* Bloquear pagos de reservas vencidas.
* Bloquear pagos duplicados.
* Mostrar estados de butacas como disponibles, reservadas o vendidas.
