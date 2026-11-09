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

### 2. Eliminación de tareas con confirmación y persistencia

**Commit:** `cfee4d3` – `deleteTask implementado`

**Fecha:** 7 de septiembre de 2026

**S – Situación**

La aplicación permitía agregar y administrar tareas, pero todavía no ofrecía una opción para eliminar aquellas que habían dejado de ser relevantes. Era necesario incorporar esta funcionalidad sin comprometer la información de las demás tareas.

**T – Tarea**

Implementar un mecanismo que permitiera seleccionar y eliminar tareas de manera interactiva, solicitando confirmación antes de ejecutar la operación y conservando los cambios en el archivo JSON.

**A – Acción**

- Incorporé la opción `Eliminar Tarea` al menú principal de la aplicación.
- Implementé el método `PromptDeleteTask()` en `Program.cs` para mostrar las tareas disponibles mediante `SelectionPrompt<TaskItem>`.
- Agregué una validación para evitar mostrar el selector cuando no existieran tareas registradas.
- Implementé una confirmación antes de realizar la eliminación, permitiendo cancelar la operación.
- Desarrollé el método `DeleteTask(int id)` en `TaskManager`, utilizando `FirstOrDefault()` para localizar la tarea por su identificador.
- Incorporé la eliminación del elemento mediante `Remove()` y el guardado inmediato de los cambios con `SaveTasksToFile()`.

**R – Resultado**

Conseguí incorporar la eliminación de tareas con confirmación previa y actualización automática del archivo `tasks.json`. Verifiqué que las tareas eliminadas no reaparecieran después de reiniciar la aplicación y que las tareas restantes conservaran sus identificadores. También comprobé que las nuevas tareas recibieran identificadores sin duplicidades.

**Tecnologías y conceptos:** C#, .NET, LINQ, `FirstOrDefault()`, manipulación de colecciones, Spectre.Console, confirmación de operaciones y persistencia mediante JSON.

### 3. Actualización del título de tareas conservando su identificador

**Commit:** `1fbbbf8` – `feat(todo): añadir funcionalidad para renombrar tareas con valor predeterminado`

**Fecha:** 7 de septiembre de 2026

**S – Situación**

La aplicación permitía agregar, consultar y eliminar tareas, pero todavía no ofrecía una opción para modificar el título de una tarea existente. Cuando el usuario cometía un error al registrarla o necesitaba actualizar su descripción, tenía que eliminarla y crearla nuevamente.

Esta alternativa podía resultar inconveniente en escenarios donde otros registros dependieran del identificador de la tarea, ya que eliminarla y crear otra podría afectar la integridad de las relaciones entre los datos.

**T – Tarea**

Implementar una funcionalidad que permitiera actualizar el título de una tarea existente, conservando su identificador y evitando operaciones innecesarias cuando el usuario decidiera mantener el nombre original.

**A – Acción**

- Incorporé la opción `Renombrar Tarea` al menú principal de la aplicación.
- Implementé el método `PromptRenameTask()` en `Program.cs` para mostrar las tareas disponibles mediante `SelectionPrompt<TaskItem>`.
- Agregué una validación para evitar mostrar el selector cuando no existieran tareas registradas.
- Utilicé `TextPrompt<string>` con `DefaultValue()` para presentar el título actual como valor inicial y facilitar su edición.
- Incorporé una condición para cancelar la operación cuando el usuario conservara el título original.
- Desarrollé el método `UpdateTaskTitle(int id, string newTitle)` en `TaskManager`, utilizando `FirstOrDefault()` para localizar la tarea mediante su identificador único.
- Actualicé exclusivamente la propiedad `Title`, conservando el identificador de la tarea.
- Incorporé `SaveTasksToFile()` para guardar automáticamente los cambios en el archivo JSON.

**R – Resultado**

Conseguí implementar la actualización de títulos sin necesidad de eliminar y volver a crear tareas. La solución conserva el identificador original de cada registro y evita modificaciones innecesarias cuando el usuario no cambia el título.

Verifiqué que el nuevo título permaneciera guardado en `tasks.json` después de cerrar y reiniciar la aplicación, y que presionar Enter sin modificarlo cancelara la operación.

**Tecnologías y conceptos:** C#, .NET, LINQ, `FirstOrDefault()`, actualización de objetos, identificadores únicos, Spectre.Console y persistencia mediante JSON.

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

- [x] Agregar funcionalidad para eliminar tareas con confirmación del usuario.
- [x] Agregar funcionalidad para renombrar tareas con confirmación del usuario.
- [ ] Refactorización aplicando principios SOLID de forma síncrona
- [ ] Agregar funcionalidad para persistencia asíncrona.
- [ ] Implementar pruebas unitarias utilizando xUnit.
- [ ] Migrar el almacenamiento local de JSON a una base de datos con SQL Server y EF Core.