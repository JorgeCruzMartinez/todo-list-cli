# 📝 Task Manager CLI - .NET Console Application

<p align="left">
  <strong>Español</strong> | <a href="./README.en.md">English</a>
</p>

Una aplicación de consola simple y eficiente pero moderna, interactiva y robusta para gestionar tus tareas diarias, diseñada para ejecutarse directamente desde la terminal, desarrollada en **.NET 8** (o tu versión). Este proyecto demuestra el uso de interfaces de línea de comandos (CLI) dinámicas, persistencia de datos local y buenas prácticas de arquitectura en C# siendo este proyecto parte de mi portafolio personal.
---

## 🚀 Características Clave

   **Interfaz de Usuario Enriquecida:** Menús interactivos y tablas dinámicas controladas con el teclado gracias a "Spectre.Console".
   **Persistencia Local:** Guardado y carga automática de tareas en formato "JSON" mediante "System.Text.Json".
   **Manejo de Colecciones con LINQ:** Filtrado, búsquedas eficientes y actualizaciones de estado seguras en memoria.
   **Código Limpio:** Separación estricta de responsabilidades (Modelos, Lógica de Negocio y Presentación).

---

## 🛠️ Tecnologías y Librerías Utilizadas

   **Lenguaje:** C# 12
   **Framework:** .NET 8.0 SDK
   **Librerías de Terceros:**
     *   [Spectre.Console] (https://spectreconsole.net) - Para el diseño visual, tablas y prompts interactivos en la terminal.

---

## 📈 Evolución del Proyecto: Documentación STAR

Esta sección documenta la evolución técnica de la aplicación mediante la metodología STAR (Situación, Tarea, Acción y Resultado), destacando los problemas abordados, las decisiones de implementación y los resultados obtenidos durante su desarrollo.

### 1. Persistencia automática de tareas mediante JSON

**Commit:** `8b03d08` – `autoSave implementado`

**Fecha:** 3 de septiembre de 2026

**S – Situación**

Inicialmente, la aplicación almacenaba las tareas únicamente en memoria, por lo que la información se perdía al cerrar la aplicación. Era necesario incorporar un mecanismo que permitiera conservar las tareas entre diferentes ejecuciones.

**T – Tarea**

Implementar un mecanismo de persistencia que permitiera guardar automáticamente las tareas en un archivo JSON y recuperar su información al iniciar nuevamente la aplicación.

**A – Acción**

- Implementé la serialización y deserialización de objetos utilizando `System.Text.Json`.
- Incorporé operaciones de lectura y escritura mediante `File.ReadAllText()` y `File.WriteAllText()`.
- Implementé los métodos `SaveTasksToFile()` y `LoadTasksFromFile()` en `TaskManager`.
- Configuré el guardado automático después de agregar tareas o modificar su estado.
- Implementé el cálculo del siguiente identificador a partir del ID máximo almacenado, evitando duplicidades.
- Incorporé manejo de excepciones en las operaciones de persistencia.

**R – Resultado**

Conseguí que las tareas se conservaran en `tasks.json` y pudieran recuperarse al reiniciar la aplicación. Verifiqué que los identificadores, títulos y estados de las tareas permanecieran correctamente después de cerrar y volver a ejecutar el programa.

**Tecnologías y conceptos:** C#, .NET, `System.Text.Json`, manejo de archivos, serialización, deserialización y manejo de excepciones.

---

## 📦 Instalación y Ejecución

## Prerrequisitos
* Tener instalado [Idioma/Entorno, ej: Node.js / Python / .NET]

Sigue estos pasos para clonar y ejecutar el proyecto localmente:

1. **Clonar el repositorio:**
   """bash
   git clone https://github.com
   cd TU_REPOSITORIO
   """

2. **Restaurar dependencias e instalar Spectre.Console:**
   """bash
   dotnet restore
   """

3. **Ejecutar la aplicación:**
   """bash
   dotnet run
   """

---

## 📂 Estructura del Código

*   "Program.cs": Controla el flujo principal de la aplicación y renderiza la interfaz visual interactiva.
*   "TaskManager.cs"": Contiene toda la lógica de negocio, manipulación de listas mediante LINQ y la serialización/deserialización del archivo JSON.
*   "TaskItem.cs"": Entidad pura de datos que define las propiedades de una tarea (Id, Título, Estado).

---

## 📈 Próximas Mejoras (Roadmap)

- [ ] Agregar funcionalidad para eliminar tareas con confirmación del usuario.
- [ ] Agregar funcionalidad para renombrar tareas con confirmación del usuario.
- [ ] Refactorización aplicando principios SOLID de forma síncrona
- [ ] Agregar funcionalidad para persistencia asíncrona.
- [ ] Implementar pruebas unitarias utilizando xUnit.
- [ ] Migrar el almacenamiento local de JSON a una base de datos con SQL Server y EF Core.