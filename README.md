# 🏥 TenderCare - Pediatric Clinic Management System

**TenderCare** is a web-based Healthcare Management System built using **ASP.NET Core MVC**. The platform is designed to streamline pediatric appointment scheduling and provide parents with a secure, centralized dashboard to monitor their child's medical history.

---

## 🌟 Key Features

* **User Authentication**: Secure login and account management for guardians.
* **Intelligent Booking**: A smart appointment form that automatically links records to the user's logged-in email.
* **Medical History Dashboard**: A personalized view for tracking past checkups, vaccinations, and immunizations.
* **Crash-Proof Logic**: 
    * **New Account Handling**: Implemented safety checks to prevent `NullReferenceException` errors for users with no existing records.
    * **Empty States**: Professional "Welcome" UI for new users to guide them toward their first booking.
* **Refined Notifications**: Uses isolated `TempData` keys to ensure success messages (like "Successfully Logged In") do not conflict with appointment confirmations.

---

## 💻 Technical Stack

* **Framework**: ASP.NET Core MVC (8.0)
* **Language**: C#
* **Database**: MySQL / MariaDB
* **ORM**: Entity Framework Core
* **Frontend**: Razor Pages, CSS3, JavaScript, SweetAlert2

---

## 🗄️ Database Architecture

The system utilizes a relational database structure to manage patient data and clinical schedules:
* **Patients Table**: Stores core profile data (Email, Name, Guardian details).
* **Appointments Table**: Stores visit types, dates, and medical notes (Linked via `PatientID`).



---

## 🛠️ Implementation Details

During development, the following technical challenges were resolved:
1.  **Notification Logic**: Segregated `TempData["Success"]` and `TempData["AppointmentBooked"]` so that the SweetAlert popup only triggers upon form submission, not during standard navigation or login.
2.  **Model Safety**: Updated the `HomeController` to pass a "Dummy Model" if a patient record is not yet found, ensuring the "My History" page remains stable for new users.
3.  **UI Consistency**: Used conditional Razor logic to switch between a "No Records" welcome screen and the "Medical History" table based on data availability.

---

## 🚀 Getting Started

1.  **Clone the Repository**
    ```bash
    git clone [https://github.com/your-username/TenderCare.git](https://github.com/your-username/TenderCare.git)
    ```
2.  **Configure Database**
    Update your connection string in `appsettings.json`:
    ```json
    "DefaultConnection": "Server=localhost;Database=tendercaredb;User=root;Password=yourpassword;"
    ```
3.  **Apply Migrations**
    ```bash
    dotnet ef database update
    ```
4.  **Launch**
    ```bash
    dotnet run
    ```

---

© 2026 TenderCare Pediatric Clinic
