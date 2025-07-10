using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ATM_SQL_test
{
    public class BankAccount
    {
        public long AccountNumber { get; private set; }
        public string Name { get; private set; }
        public string Gender { get; private set; }
        public string Surname { get; private set; }
        public double Balance { get; private set; }

        public BankAccount(long accountNumber, string name, string gender, string surname, double balance)
        {
            AccountNumber = accountNumber;
            Name = name;
            Gender = gender;
            Surname = surname;
            Balance = balance;
        }

        // Deposit money into account
        public void Deposit(double amount)
        {

            if (amount <= 0)
            {
                throw new ArgumentException("Deposit amount must be greater than zero.");
            }

            Balance += amount;

        }

        // Withdraw money from account
        public bool Withdraw(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Withdrawal amount must be greater than zero.");
            }

            if (Balance >= amount)
            {
                Balance -= amount;
                return true;
            }
            else
            {
                return false; // Insufficient funds
            }
        }

        // Check current balance
        public double CheckBalance()
        {
            return Balance;
        }
    }
}
