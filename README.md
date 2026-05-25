# 📚 Library Management System

A Windows Forms desktop application built with **C#**, **ADO.NET**, and **SQL Server** following a **3-Layer Architecture (UI / BLL / DAL)**.

This project demonstrates CRUD operations, search functionality, clean architecture principles, and database interaction using ADO.NET.

## 🎬 Demo

## 🎬 Demo

![Application Demo](Library%20Management%20System/Images/Animation.gif)

---

## Architecture Overview

![Architecture](Library%20Management%20System/Images/Data_Flow.png)

## ✨ Features

✔ Add new books

✔ Update existing books

✔ Delete books

✔ Search by:

* Title
* Author
* Category

✔ Refresh DataGridView

✔ Input Validation

✔ SQL Injection Prevention using Parameterized Queries

✔ Layered Architecture (UI / BLL / DAL)

✔ Exception Handling

✔ Data binding using DataGridView

---

## 🏗 Project Architecture

This project follows a layered architecture:

### 1. Presentation Layer (Windows Forms)

Responsible for:

* User interaction
* Handling events
* Displaying data
* Showing validation messages

Example:

```csharp
btnAdd_Click()
```

---

### 2. Business Logic Layer (BLL)

Responsible for:

* Validation
* Business rules
* Processing application logic

Example:

```csharp
BookBLL.AddBook()
```

---

### 3. Data Access Layer (DAL)

Responsible for:

* Database communication
* SQL queries
* Data retrieval and updates

Example:

```csharp
BookDAL.AddBook()
```

---

## 🔄 Application Flow

```text
User
↓
Windows Form (UI)
↓
Business Logic Layer (BLL)
↓
Data Access Layer (DAL)
↓
SQL Server
↓
BLL
↓
UI
↓
DataGridView
```

---

## 🛠 Technologies Used

* C#
* ADO.NET
* SQL Server
* Windows Forms
* DataGridView
* Visual Studio 2022

---

## 🚀 Getting Started

### Prerequisites

Install:

* Visual Studio 2022
* SQL Server Express / LocalDB
* SQL Server Management Studio

---

### Installation

1. Clone repository

```bash
git clone https://github.com/YOUR_USERNAME/LibraryManagementSystem.git
```

2. Open solution in Visual Studio

3. Execute:

```text
SQLQuery1.sql
```

inside SQL Server Management Studio

4. Update your connection string in:

```text
App.config
```

Example:

```xml
<connectionStrings>
<add name="ConnectionString"
connectionString="Data Source=.;Initial Catalog=LibraryDB;Integrated Security=True"/>
</connectionStrings>
```

5. Build and run project

---

## 📂 Project Structure

```text
Library-Management-System
│
├── README.md
│
├── Images
│      ├── Animation.gif
│      └── Data_Flow.png
│
├── LibraryManagementSystem
├── Library.BLL
├── Library.DAL
```

---

## 🔐 Security

This project uses parameterized SQL queries:

```csharp
command.Parameters.Add(
"@Search",
SqlDbType.NVarChar
).Value = "%" + searchText + "%";
```

This prevents SQL Injection attacks.

---

## 📈 Future Improvements

* Export PDF
* Export Excel
* Dashboard statistics
* Login system
* Category management
* Dark mode UI
* Pagination

---

## 👨‍💻 Author

Ezio

GitHub:
https://github.com/EzioHub

