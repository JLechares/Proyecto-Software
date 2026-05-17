# 🎟️ Proyecto Software - Sistema de Gestión de Eventos

Este proyecto es una API desarrollada en **.NET 10** para la gestión de eventos, sectores y reserva de asientos (butacas).  
Permite consultar información de eventos, visualizar disponibilidad y realizar reservas.

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

---

## 🚀 Ejecución del proyecto

## 🛠️ Configuración y Precarga de la Base de Datos (SQL Server)

El proyecto utiliza **SQL Server LocalDB** para el entorno de desarrollo y cuenta con un sistema de **Data Seeding** automático. Al aplicar las migraciones, la base de datos se creará y se rellenará automáticamente con los datos iniciales de prueba para el sistema de reservas.

### Datos Precargados Automáticamente:
* **Evento:** Concierto de María Becerra en el Estadio River Plate (Programado para el 15/08/2026).
* **Sectores:** * `VIP` (Id: 1, Precio: $25.000, Capacidad: 50 butacas).
  * `Preferencial` (Id: 2, Precio: $12.500, Capacidad: 50 butacas).
* **Butacas:** Se generan automáticamente las 100 butacas del estadio, mapeadas con GUIDs secuenciales fijos, distribuidas equitativamente en filas de 10 asientos por sector y configuradas inicialmente como `Available` (Disponibles).
* **Usuarios:** Dos usuarios precargados, que son los integrantes del proyecto
 ---

### 🚀 Pasos para levantar la Base de Datos en tu máquina

Para clonar el proyecto y tener todo funcionando con datos de prueba de entrada, seguí estos pasos:

1. **Requisito previo:** Asegurate de tener instalado **SQL Server LocalDB** (se instala de forma predeterminada al cargar el entorno de desarrollo de .NET / Visual Studio).
2. **Verificar Cadena de Conexión:** El archivo `appsettings.json` de la WebAPI ya viene configurado de forma estándar para apuntar a la instancia local común:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=EventReservationDB;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
3. Ejecutar la Migración y el Seeding: Abre una terminal en la raíz del proyecto (donde se encuentra el archivo de contexto AppDbContext.cs) y ejecuta el comando de Entity Framework:

 ```Bash
dotnet ef database update
 ```
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
- Reservar asientos  

---

## 🛠️ Tecnologías utilizadas

- .NET 10 
- ASP.NET Core  
- Swagger  
- Arquitectura Clean + CQRS  

---

## 📈 Estado del proyecto

En desarrollo 🚧  

---

## 👨‍💻 Autor

Proyecto desarrollado como práctica para la materia **Proyecto de Software**.
- Juan Bautista Lechares
- Juan Cruz Merino
