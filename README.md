# Cari Tracking Management System

A full-stack web application developed to manage customer accounts (Cari), financial transactions, and account balances. The project was built as part of a software engineering internship using a layered architecture.

---

## Features

### Authentication

- User registration
- Secure login with JWT Authentication
- Profile page
- Profile update

### Customer (Cari) Management

- Create customer
- Update customer
- Delete customer
- List all customers
- Customer details
- Credit limit management
- Tax Number / National ID
- Phone number
- Email information

### Customer Transactions

- Add transaction
- Update transaction
- Delete transaction
- Debit / Credit operations
- Transaction source types
  - Sales Invoice
  - Purchase Invoice
  - Collection
  - Payment
  - Manual Transaction

### Balance Management

- Automatic customer balance calculation
- Current balance display

### Form Validation

- Required field validation
- Duplicate Tax Number control
- Negative credit limit prevention
- Email validation
- Phone number formatting

---

## Technologies

### Backend

- ASP.NET Core Web API
- C#
- Entity Framework Core
- SQLite
- JWT Authentication
- Dependency Injection
- Repository Pattern
- Service Layer

### Frontend

- React
- React Router
- JavaScript (ES6)
- CSS
- Fetch API

---

## Project Architecture

```
CariTakip.API
│
├── Controllers
├── Services
│
CariTakip.Business
│
├── DTOs
├── Services
│
CariTakip.DataAccess
│
├── DbContext
├── Repositories
│
CariTakip.Entities
│
├── Models
├── Enums
│
CariTakip.Frontend
│
├── Pages
├── Components
├── Services
```

---

## Database

Main entities:

- User
- Cari
- CariHareket

Relationship:

```
User
 │
 └── Cari
        │
        └── CariHareket
```

---

## Installation

### Backend

```bash
git clone https://github.com/your-username/CariTakip.git

cd CariTakip.API

dotnet restore

dotnet ef database update

dotnet run
```

### Frontend

```bash
cd CariTakip.Frontend

npm install

npm run dev
```

---

## Future Improvements

- Dashboard page
- Search and filtering
- Excel/PDF reporting
- Role-based authorization
- Activity logs
- Statistics dashboard


---

## Developer

**Gaye**

Software Engineering Student