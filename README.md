# 🏢 Employee Management System (EMS) - Enterprise Edition

A modern, responsive, and secure web application built with **Blazor Server** for centralizing employee records, credential provisioning, and departmental configuration. 

## ✨ Features

* **Modern Dark UI:** A premium, dark-themed user interface featuring glass-morphism effects, custom gradients, and soft shadows for an optimal enterprise user experience.
* **Robust Form Validation:** Client and server-side validation powered by C# DataAnnotations, ensuring high data integrity before database insertion.
* **Geolocation Integration:** Built-in JavaScript interop (JS Runtime) to fetch and track real-time spatial coordinates for field employees.
* **Department Mapping:** Dynamic dropdowns linking employees to corporate departments via relational IDs.
* **State Management:** Asynchronous UI updates, loading spinners, and modal success popups to prevent double-submissions and improve UX.

## 🛠️ Tech Stack

* **Frontend:** Blazor (Interactive Server Render Mode), HTML5, CSS3, Bootstrap 5
* **Backend:** ASP.NET Core, C#
* **Architecture:** N-Tier structured with separated Shared DTOs (`EMS.Shared.DTOs`) and localized Services (`EMS.Blazor.Services`).
* **Data Binding:** Two-way data binding with `<EditForm>` and custom Blazor input components.

## 📁 Project Structure

EMS.Solution/
├── EMS.Blazor/                 # Main web application (Frontend & UI Logic)
│   ├── Components/             # Reusable UI components (e.g., Modals)
│   ├── Pages/                  # Routable pages (e.g., AddEmployee.razor, EmployeeList.razor)
│   ├── Models/                 # Client-side specific view models
│   └── Services/               # API/Database communication (EmployeeService.cs)
├── EMS.Shared/                 # Shared logic between client and API
│   └── DTOs/                   # Data Transfer Objects for clean data contracts
└── EMS.Api/                    # (Optional) Backend REST API if separated from Server App