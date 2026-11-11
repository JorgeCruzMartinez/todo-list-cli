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

### Hito 1. Persistencia automática de tareas mediante JSON

**Commit:** `8b03d08` – `autoSave implementado`

**Fecha:** 3 de septiembre de 2026

**Situación (Situation)**

Inicialmente, la aplicación almacenaba las tareas únicamente en memoria, por lo que la información se perdía al cerrar la aplicación. Era necesario incorporar un mecanismo que permitiera conservar las tareas entre diferentes ejecuciones.

**Tarea (Task)**

Implementar un mecanismo de persistencia que permitiera guardar automáticamente las tareas en un archivo JSON y recuperar su información al iniciar nuevamente la aplicación.

**Acción (Action)**

- Implementé la serialización y deserialización de objetos utilizando `System.Text.Json`.
- Incorporé operaciones de lectura y escritura mediante `File.ReadAllText()` y `File.WriteAllText()`.
- Implementé los métodos `SaveTasksToFile()` y `LoadTasksFromFile()` en `TaskManager`.
- Configuré el guardado automático después de agregar tareas o modificar su estado.
- Implementé el cálculo del siguiente identificador a partir del ID máximo almacenado, evitando duplicidades.
- Incorporé manejo de excepciones en las operaciones de persistencia.

**Resultado (Result)**

Conseguí que las tareas se conservaran en `tasks.json` y pudieran recuperarse al reiniciar la aplicación. Verifiqué que los identificadores, títulos y estados de las tareas permanecieran correctamente después de cerrar y volver a ejecutar el programa.

**Tecnologías y conceptos:** C#, .NET, `System.Text.Json`, manejo de archivos, serialización, deserialización y manejo de excepciones.

### Hito 2. Eliminación de tareas con confirmación y persistencia

**Commit:** `cfee4d3` – `deleteTask implementado`

**Fecha:** 7 de septiembre de 2026

**Situación (Situation)**

La aplicación permitía agregar y administrar tareas, pero todavía no ofrecía una opción para eliminar aquellas que habían dejado de ser relevantes. Era necesario incorporar esta funcionalidad sin comprometer la información de las demás tareas.

**Tarea (Task)**

Implementar un mecanismo que permitiera seleccionar y eliminar tareas de manera interactiva, solicitando confirmación antes de ejecutar la operación y conservando los cambios en el archivo JSON.

**Acción (Action)**

- Incorporé la opción `Eliminar Tarea` al menú principal de la aplicación.
- Implementé el método `PromptDeleteTask()` en `Program.cs` para mostrar las tareas disponibles mediante `SelectionPrompt<TaskItem>`.
- Agregué una validación para evitar mostrar el selector cuando no existieran tareas registradas.
- Implementé una confirmación antes de realizar la eliminación, permitiendo cancelar la operación.
- Desarrollé el método `DeleteTask(int id)` en `TaskManager`, utilizando `FirstOrDefault()` para localizar la tarea por su identificador.
- Incorporé la eliminación del elemento mediante `Remove()` y el guardado inmediato de los cambios con `SaveTasksToFile()`.

**Resultado (Result)**

Conseguí incorporar la eliminación de tareas con confirmación previa y actualización automática del archivo `tasks.json`. Verifiqué que las tareas eliminadas no reaparecieran después de reiniciar la aplicación y que las tareas restantes conservaran sus identificadores. También comprobé que las nuevas tareas recibieran identificadores sin duplicidades.

**Tecnologías y conceptos:** C#, .NET, LINQ, `FirstOrDefault()`, manipulación de colecciones, Spectre.Console, confirmación de operaciones y persistencia mediante JSON.

### Hito 3. Actualización del título de tareas conservando su identificador

**Commit:** `1fbbbf8` – `feat(todo): añadir funcionalidad para renombrar tareas con valor predeterminado`

**Fecha:** 7 de septiembre de 2026

**Situación (Situation)**

La aplicación permitía agregar, consultar y eliminar tareas, pero todavía no ofrecía una opción para modificar el título de una tarea existente. Cuando el usuario cometía un error al registrarla o necesitaba actualizar su descripción, tenía que eliminarla y crearla nuevamente.

Esta alternativa podía resultar inconveniente en escenarios donde otros registros dependieran del identificador de la tarea, ya que eliminarla y crear otra podría afectar la integridad de las relaciones entre los datos.

**Tarea (Task)**

Implementar una funcionalidad que permitiera actualizar el título de una tarea existente, conservando su identificador y evitando operaciones innecesarias cuando el usuario decidiera mantener el nombre original.

**Acción (Action)**

- Incorporé la opción `Renombrar Tarea` al menú principal de la aplicación.
- Implementé el método `PromptRenameTask()` en `Program.cs` para mostrar las tareas disponibles mediante `SelectionPrompt<TaskItem>`.
- Agregué una validación para evitar mostrar el selector cuando no existieran tareas registradas.
- Utilicé `TextPrompt<string>` con `DefaultValue()` para presentar el título actual como valor inicial y facilitar su edición.
- Incorporé una condición para cancelar la operación cuando el usuario conservara el título original.
- Desarrollé el método `UpdateTaskTitle(int id, string newTitle)` en `TaskManager`, utilizando `FirstOrDefault()` para localizar la tarea mediante su identificador único.
- Actualicé exclusivamente la propiedad `Title`, conservando el identificador de la tarea.
- Incorporé `SaveTasksToFile()` para guardar automáticamente los cambios en el archivo JSON.

**Resultado (Result)**

Conseguí implementar la actualización de títulos sin necesidad de eliminar y volver a crear tareas. La solución conserva el identificador original de cada registro y evita modificaciones innecesarias cuando el usuario no cambia el título.

Verifiqué que el nuevo título permaneciera guardado en `tasks.json` después de cerrar y reiniciar la aplicación, y que presionar Enter sin modificarlo cancelara la operación.

**Tecnologías y conceptos:** C#, .NET, LINQ, `FirstOrDefault()`, actualización de objetos, identificadores únicos, Spectre.Console y persistencia mediante JSON.

### Hito 4. Refactorización para mejorar la legibilidad y el mantenimiento del código

**Commit:** `957aa82`
**Fecha:** 10 de septiembre de 2026


**Situación (Situation)**
El proyecto ya contaba con las operaciones básicas de un CRUD (crear, consultar, actualizar y eliminar tareas). Sin embargo, identifiqué oportunidades para simplificar la estructura del código y mejorar su legibilidad, facilitando su comprensión por parte de otros desarrolladores y su mantenimiento ante futuras mejoras.

**Tarea (Task)**
Refactorizar el código para simplificar su estructura y mejorar su legibilidad, manteniendo intactas las funcionalidades existentes y facilitando su mantenimiento y evolución futura.

**Acción (Action)**
Revisé los archivos `Program.cs`, `TaskItem.cs` y `TaskManager.cs`, identificando oportunidades para simplificar expresiones, reducir estructuras innecesarias y mejorar la legibilidad.

Realicé inicialmente los ajustes de forma manual y, al identificar patrones repetitivos, utilicé las herramientas de refactorización de Visual Studio para extenderlos de manera consistente.

Entre los cambios realizados estuvieron:
- Adopción de *file-scoped namespaces*.
- Uso de expresiones de colección (`[]`) para simplificar la inicialización de listas.
- Sustitución de `Any()` por `Count == 0` en comprobaciones de listas.
- Simplificación de estructuras y ajustes en la interacción con el usuario.

**Resultado (Result)**
Verifiqué que las operaciones principales de la aplicación continuaran funcionando correctamente después de la refactorización.

Además, la simplificación del código me permitió identificar nuevas oportunidades de mejora en su diseño, motivándome a explorar y aplicar los principios SOLID como siguiente etapa de evolución del proyecto.

---

## 📦 Instalación y ejecución

### Prerrequisitos

- .NET 8 SDK
- Git

### Clonar el repositorio

```bash
git clone https://github.com/JorgeCruzMartinez/todo-list-cli.git
cd todo-list-cli
```

### Restaurar dependencias

```bash
dotnet restore Todo-List.sln
```

### Ejecutar la aplicación

```bash
dotnet run --project Todo-List.csproj
```

### Ejecutar las pruebas unitarias

```bash
dotnet test Todo-List.sln
```

---

## 📂 Estructura del Código

- `Program.cs`: controla el flujo principal de la aplicación y presenta la interfaz interactiva.
- `TaskManager.cs`: contiene la lógica de negocio y la administración de tareas.
- `TaskItem.cs`: define la entidad que representa una tarea.
- `ITaskRepository.cs`: define el contrato para las operaciones de persistencia.
- `JsonTaskRepository.cs`: implementa la persistencia de tareas mediante JSON.
- `Data/`: contiene los elementos relacionados con el almacenamiento de datos.
- `Migrations/`: contiene las migraciones de Entity Framework Core.
- `Todo_List.Tests/`: contiene las pruebas unitarias del repositorio JSON.

---

## 📈 Próximas Mejoras (Roadmap)

- [x] Agregar funcionalidad para eliminar tareas con confirmación del usuario.
- [x] Agregar funcionalidad para renombrar tareas con confirmación del usuario.
- [ ] Refactorización aplicando principios SOLID de forma síncrona
- [ ] Agregar funcionalidad para persistencia asíncrona.
- [ ] Implementar pruebas unitarias utilizando xUnit.
- [ ] Migrar el almacenamiento local de JSON a una base de datos con SQL Server y EF Core.