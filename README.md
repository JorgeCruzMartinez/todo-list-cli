# ⏳ TODO List CLI - .NET Console Application

Una aplicación de consola moderna, interactiva y robusta para la gestión de tareas diarias, desarrollada en **.NET 8** (o tu versión). Este proyecto demuestra el uso de interfaces de línea de comandos (CLI) dinámicas, persistencia de datos local y buenas prácticas de arquitectura en C#.

---

## 🚀 Características Clave

*   **Interfaz de Usuario Enriquecida:** Menús interactivos y tablas dinámicas controladas con el teclado gracias a `Spectre.Console`.
*   **Persistencia Local:** Guardado y carga automática de tareas en formato `JSON` mediante `System.Text.Json`.
*   **Manejo de Colecciones con LINQ:** Filtrado, búsquedas eficientes y actualizaciones de estado seguras en memoria.
*   **Código Limpio:** Separación estricta de responsabilidades (Modelos, Lógica de Negocio y Presentación).

---

## 🛠️ Tecnologías y Librerías Utilizadas

*   **Lenguaje:** C# 12
*   **Framework:** .NET 8.0 SDK
*   **Librerías de Terceros:**
    *   [Spectre.Console] (https://spectreconsole.net) - Para el diseño visual, tablas y prompts interactivos en la terminal.

---

## 📦 Instalación y Ejecución

Sigue estos pasos para clonar y ejecutar el proyecto localmente:

1. **Clonar el repositorio:**
   ```bash
   git clone https://github.com
   cd TU_REPOSITORIO
   ```

2. **Restaurar dependencias e instalar Spectre.Console:**
   ```bash
   dotnet restore
   ```

3. **Ejecutar la aplicación:**
   ```bash
   dotnet run
   ```

---

## 📂 Estructura del Código

*   `Program.cs`: Controla el flujo principal de la aplicación y renderiza la interfaz visual interactiva.
*   `TaskManager.cs`: Contiene toda la lógica de negocio, manipulación de listas mediante LINQ y la serialización/deserialización del archivo JSON.
*   `TaskItem.cs`: Entidad pura de datos que define las propiedades de una tarea (Id, Título, Estado).

---

## 📈 Próximas Mejoras (Roadmap)

- [ ] Agregar funcionalidad para eliminar tareas con confirmación del usuario.
- [ ] Implementar pruebas unitarias utilizando xUnit.
- [ ] Migrar el almacenamiento local de JSON a una base de datos ligera con SQLite y EF Core.