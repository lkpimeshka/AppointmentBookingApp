![image](https://github.com/user-attachments/assets/104563a5-d121-49e2-bcd8-37bb220153d4)# Smart Appointment Booking System

## Overview
The **Smart Appointment Booking System** is a web-based application built using **Blazor Server** and **ASP.NET Core Web API** following Clean Architecture principles. It allows users to schedule, modify, and manage appointments with professionals.

## Tech Stack
- **Frontend**: Blazor Server
- **Backend**: ASP.NET Core Web API (Clean Architecture)
- **Database**: SQL Server with EF Core
- **Authentication**: Identity with JWT Authentication
- **UI Components**: MudBlazor OR Syncfusion (if available)

---

## Setup Guide

### 1. Prerequisites
Ensure you have the following installed:
- [.NET SDK 8.0+](https://dotnet.microsoft.com/download/dotnet)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/downloads/) with ASP.NET and Blazor workload
- [Entity Framework Core CLI](https://docs.microsoft.com/en-us/ef/core/cli/dotnet) (Install using `dotnet tool install --global dotnet-ef`)

### 2. Clone the Repository
```sh
https://github.com/lkpimeshka/AppointmentBookingApp.git
cd AppointmentBookingApp
```

### 3. Configure Database
Update the **connection string** in `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=your_server;Database=SmartAppointments;User Id=your_user;Password=your_password;TrustServerCertificate=True;"
}
```

### 4. Apply Migrations & Update Database
Run the following commands in the **Backend API Project directory**:
```sh
# Add Migration using packagemanager console
add-migration initialMigration

# Update Database using packagemanager console
update-database
```

### 5. Run the Backend API & Blazor UI
1. Go to soluton properties and select multiple startup projects
2. Set Client and Server action as start and click Apply
3. Click Start on Visual Studio and it will run both backend and frontend together
4. Admin Account and roles will create automatically when you first run the project.
5. You can change those in Infastructre layer DbSeeder.cs 

Default admin login 
Username: admin
Password: Admin@123



*Important: Make sure in the blazor project program.cs file has the correct port number for Server. By default it is 7139. change it to your backend port number
(builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7139/") });)

## Features & Endpoints

### 1. Authentication (Identity + JWT)
- **Register**: `POST /api/auth/register`
- **Login**: `POST /api/auth/login`

### 2. Appointments API (Protected with JWT)
- **Get All Appointments**: `GET /api/appointments`
- **Get Appointment by ID**: `GET /api/appointments/{id}`
- **Book an Appointment**: `POST /api/appointments`
- **Modify Appointment**: `PUT /api/appointments/{id}`
- **Cancel Appointment**: `DELETE /api/appointments/{id}`

### 3. Professional Availability API (Protected)
- **Set Availability**: `POST /api/professionals/availability`
- **Get Available Slots**: `GET /api/professionals/availability`

---

## Blazor UI

1. Homepage

![image](https://github.com/user-attachments/assets/56793912-1d9a-4313-9acc-299769b6487c)

2.Loging and registration

You can register as Professional or General User According to your Role. 

![image](https://github.com/user-attachments/assets/09227e05-0d97-4cc2-a74b-07547a631a2e)
![image](https://github.com/user-attachments/assets/022db3a1-ae7e-48be-ba34-1735a5a7f9b1)

3. User List

Only admin can see this page

![image](https://github.com/user-attachments/assets/98eca81b-4d53-4571-b085-f192b4b578a8)

4.Available Timeslots & Add new Timeslot (for profession only)

![image](https://github.com/user-attachments/assets/68341dc2-9514-4556-824a-956793c18328)
![image](https://github.com/user-attachments/assets/30ef6940-fc6a-48e7-a554-86dc3cbc9ea5)

5.Appointment List

Admin can see all the appointments
Professionals and Users can see the appointments that only related to them
Users can Cancel or edit their appointment until due date
Professionals can accept or reject appointmnet

![image](https://github.com/user-attachments/assets/bc4ebfb9-1c31-4937-b5d6-08d2c5e4e27e)

6.Create new Appointment (for users only)

![image](https://github.com/user-attachments/assets/16f38b6a-fe77-448a-977d-d3006ebd8045)
![image](https://github.com/user-attachments/assets/29fff0a0-b897-48dc-91e9-ca61c5cd7715)








