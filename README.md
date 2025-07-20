## 🔍 Overview

This project allows users to:

- Create new entries in a database
- View and list existing records
- Update and edit record details
- Delete unwanted entries
- Use the app across all screen sizes (fully responsive)

It is ideal as a base template for admin panels, dashboards, or any data-driven application.

---

## 🚀 Tech Stack

| Technology         | Description                          |
|--------------------|--------------------------------------|
| ASP.NET Core       | Backend framework                    |
| Entity Framework   | ORM for database interaction     |
| SQL Server         | Relational database engine           |
| Bootstrap          | Frontend responsive styling          |
| HTML/CSS           | UI structure and customization       |

---

## 📦 Getting Started

Follow these steps to run the project locally:

### 1. Clone the Repository

```bash
git clone https://github.com/Elwan6896/CrudApp.git

### 2. Configure the Database

Update your database connection string inside `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=CrudAppDb;Trusted_Connection=True;"
}
```

### 3. Apply Migrations & Run

```bash
dotnet ef database update
dotnet run
```

Open your browser and navigate to `https://localhost:5001` (or the port in your terminal).

## 📬 Contact

* **Author**: Ahmed Elwan
* **Email**: [aelwan6896@gmail.com]
* **GitHub**: [Elwan6896](https://github.com/Elwan6896)

