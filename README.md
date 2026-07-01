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

Here are some sinppets of the Employee Management System.
<img width="1342" height="631" alt="Screenshot 2026-07-01 125920" src="https://github.com/user-attachments/assets/14944106-9675-485b-8cb2-0f5528fe6adc" />
<img width="1352" height="557" alt="Screenshot 2026-07-01 125942" src="https://github.com/user-attachments/assets/b0108b5f-8ec2-4ce8-b43b-36df53f11001" />
<img width="488" height="489" alt="Screenshot 2026-07-01 130006" src="https://github.com/user-attachments/assets/da1d1ac7-71b4-4b9e-be78-2281b5da4f82" />
<img width="1353" height="632" alt="Screenshot 2026-07-01 130027" src="https://github.com/user-attachments/assets/070bfb55-9da4-4d14-9932-4ea9a020e4d7" />
<img width="1365" height="720" alt="Screenshot 2026-07-01 130048" src="https://github.com/user-attachments/assets/ba792189-947d-464a-92a0-5495c9dbfcce" />
<img width="482" height="519" alt="Screenshot 2026-07-01 130148" src="https://github.com/user-attachments/assets/af0a8219-0968-4a6b-93bf-be68b3c7e59e" />
<img width="351" height="351" alt="Screenshot 2026-07-01 130214" src="https://github.com/user-attachments/assets/e3eb5f5e-9664-496c-a05e-7f4674b24d27" />






