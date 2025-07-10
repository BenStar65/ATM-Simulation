# 🏦 ATM System (Windows Forms + MySQL)

This is a desktop **ATM simulation application** developed in **C# using Windows Forms**, designed for managing personal bank account activities such as **deposits**, **withdrawals**, **balance checking**, and **transaction history** logging. 

It uses a **MySQL** database to store user account data and transaction records.

---

## 🚀 Features

- ✅ User greeting with gender and surname
- ✅ Deposit money to account
- ✅ Withdraw funds with validation
- ✅ Real-time balance inquiry
- ✅ Logs every transaction (Deposit, Withdrawal, Balance Check)
- ✅ View full transaction history in a separate form
- ✅ Export/Print transaction history (Print Preview included)
- ✅ Proper error handling and user feedback
- ✅ Back button navigation to safely return to main form

---

## 🗃️ Database Structure (MySQL)

### 🔹 `Accounts` Table

| Column Name     | Type         | Description                          |
|-----------------|--------------|--------------------------------------|
| `AccountNumber` | VARCHAR(20)  | Primary Key, unique account number   |
| `Namess`        | VARCHAR(100) | First name                           |
| `Surname`       | VARCHAR(100) | Last name                            |
| `PIN`           | VARCHAR(10)  | Account security PIN                 |
| `Money`         | DOUBLE       | Current account balance              |
| `Gender`        | VARCHAR(10)  | Gender for greeting message          |

---

### 🔹 `Transactions` Table

| Column Name     | Type         | Description                              |
|-----------------|--------------|------------------------------------------|
| `Id`            | INT          | Primary Key (auto-increment)             |
| `AccountNumber` | VARCHAR(20)  | Foreign Key linked to `Accounts` table   |
| `Type`          | VARCHAR(50)  | Transaction type (Deposit, Withdraw, etc)|
| `Amount`        | DOUBLE       | Amount involved in the transaction       |
| `BalanceAfter`  | DOUBLE       | Account balance after the transaction    |
| `Timestamp`     | DATETIME     | Time when the transaction occurred       |

---

## 🛠️ Technologies Used

- 💻 C# (.NET Framework 4.8.1)
- 🧩 Windows Forms (WinForms)
- 🛢️ MySQL (via `MySql.Data` connector)
- 🎨 Visual Studio for development

---

## 🔒 Security Notes

- Data access uses parameterized SQL queries to avoid SQL Injection.
- PINs are currently stored as plaintext (for learning purposes); in real-world scenarios, use hashing (e.g., SHA256).

---

## 📦 How to Run

1. Clone this repository
2. Import the `.sql` file into your MySQL server to create the required tables
3. Open the project in **Visual Studio**
4. Ensure the `MySql.Data` NuGet package is installed
5. Update the connection string in `Form1.cs` to match your MySQL setup
6. Build and run the solution

---

## 👤 Author

**Siyabonga Shembe**  
Student Developer passionate about software development, security, and IT infrastructure.

---

## 🤝 Contributing

Pull requests and feedback are welcome. Feel free to fork the project and suggest improvements!

---

## 📄 License

This project is for **educational purposes** and does not include a specific license.

