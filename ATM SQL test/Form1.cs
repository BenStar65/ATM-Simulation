using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace ATM_SQL_test
{
    public partial class Form1 : Form
    {
        
        private BankAccount _account;
        private string _connectionString = "server=localhost;port=3363;user=root;password=#Samsunset7;database=ATM;SslMode=Required;";
         
        public Form1(BankAccount account)
        {
            InitializeComponent();
            _account = account;

            label1.Text = $"Good Day {_account.Gender} {_account.Surname}";
            textBox1.Text = $"Balance: R{_account.Balance:F2}";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void LogTransaction(string type, double amount)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string sql = @"INSERT INTO Transactions (AccountNumber, Type, Amount, BalanceAfter, Timestamp) 
                       VALUES (@acctNo, @type, @amount, @balanceAfter, @timestamp)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@acctNo", _account.AccountNumber);
                    cmd.Parameters.AddWithValue("@type", type);
                    cmd.Parameters.AddWithValue("@amount", amount);
                    cmd.Parameters.AddWithValue("@balanceAfter", _account.Balance);
                    cmd.Parameters.AddWithValue("@timestamp", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            
                if (!double.TryParse(textBox2.Text, out double amount) || amount <= 0)
                {
                    textBox1.Text = "Enter a valid positive amount to deposit.";
                    return;
                }

                // Update in-memory
                _account.Deposit(amount);

                // Persist to database
                UpdateBalanceInDatabase();
               LogTransaction("Deposit", amount);


            // Feedback to user
            textBox1.Text = $"Deposit successful!";
            textBox2.Clear();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(textBox2.Text, out double amount) || amount <= 0)
            {
                textBox1.Text = "Enter a valid positive amount to withdraw.";
                return;
            }

            // Try withdraw
            if (!_account.Withdraw(amount))
            {
                textBox1.Text = "Insufficient funds.";
                    
                return;
            }

            // Persist to database
            UpdateBalanceInDatabase();
            LogTransaction("Withdrew", amount);


            // Feedback & refresh
            MessageBox.Show(
                $"Withdrawal successful!");
                

            textBox1.Text = $"Balance: R{_account.Balance:F2}";
            textBox2.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = $"Balance: R{_account.Balance:F2}";
            
        }

        /// <summary>
        /// Updates the Money column in MySQL to match the in-memory Balance.
        /// </summary>
        private void UpdateBalanceInDatabase()
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "UPDATE Accounts SET Money = @money WHERE AccountNumber = @acctNo";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@money", _account.Balance);
                    cmd.Parameters.AddWithValue("@acctNo", _account.AccountNumber);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var historyForm = new Transaction(_account, _connectionString);
            historyForm.ShowDialog();
            this.Hide();
        }
    }
}
