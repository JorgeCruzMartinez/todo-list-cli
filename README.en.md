# 📝 Task Manager CLI - .NET Console Application

<p align="left">
  <strong>Spanish</strong> | <a href="./README.en.md">English</a>
</p>

A simple yet efficient, modern, interactive and robust console application for managing your daily tasks, designed to run directly from the terminal, developed in **.NET 8** (or your version). This project demonstrates the use of dynamic command-line interfaces (CLI), local data persistence and architectural best practices in C#; it forms part of my personal portfolio.
---

## 🚀 Key Features

   **Enriched User Interface:** Interactive menus and dynamic tables controlled via the keyboard using "Spectre.Console".
   **Local Persistence:** Automatic saving and loading of tasks in "JSON" format using "System.Text.Json".
   **Collection Handling with LINQ:** Filtering, efficient searches and safe in-memory state updates.
   **Clean Code:** Strict separation of concerns (Models, Business Logic and Presentation).

---

## 🛠️ Technologies and Libraries Used

*   **Language:** C# 12
*   **Framework:** .NET 8.0 SDK
*   **Third-party libraries:**
    *   [Spectre.Console] (https://spectreconsole.net) – For visual design, tables and interactive prompts in the terminal.

---

## 📦 Installation and Execution

## Prerequisites
* Have [Language/Environment, e.g. Node.js / Python / .NET] installed



Follow these steps to clone and run the project locally:

1. **Clone the repository:**
   """bash
   git clone https://github.com
   cd YOUR_REPOSITORY
   """

2. **Restore dependencies and install Spectre.Console:**
   """bash
   dotnet restore
   """

3. **Run the application:**
   """bash
   dotnet run
   """

---

## 📂 Code Structure

*   ‘Program.cs’: Controls the main flow of the application and renders the interactive visual interface.
*   ‘TaskManager.cs’: Contains all the business logic, list manipulation using LINQ, and the serialisation/deserialisation of the JSON file.
*   ‘TaskItem.cs’: A pure data entity that defines the properties of a task (Id, Title, Status).

---

## 📈 Upcoming Improvements (Roadmap)

- [ ] Add functionality to delete tasks with user confirmation.
- [ ] Add functionality to rename tasks with user confirmation.
- [ ] Implement unit tests using xUnit.
- [ ] Migrate local storage from JSON to a lightweight database using SQLite and EF Core.