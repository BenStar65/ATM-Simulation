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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string accountNumberInput = textBox2.Text.Trim();
            string pinInput = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(accountNumberInput) || string.IsNullOrEmpty(pinInput))
            {
                MessageBox.Show("Please enter both Account Number and PIN.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (pinInput.Length != 4 || !int.TryParse(pinInput, out _))
            {
                MessageBox.Show("PIN must be exactly 4 digits.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string connectionString = "server=localhost;port=3363;user=root;password=#Samsunset7;database=ATM;SslMode=Required;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string sql = "SELECT * FROM Accounts WHERE AccountNumber = @accountNumber AND PIN = @pin";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@accountNumber", accountNumberInput);
                    cmd.Parameters.AddWithValue("@pin", pinInput);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read(); // 👈 Important!

                            long accountNumber = reader.GetInt64("AccountNumber");
                            string name = reader.GetString("Namess"); // or "Namess" based on your table
                            string gender = reader.GetString("Gender");
                            string surname = reader.GetString("Surname");
                            double balance = reader.GetDouble("Money");

                            BankAccount account = new BankAccount(accountNumber, name, gender, surname, balance);

                            MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            Form1 T = new Form1(account);
                            this.Hide();
                            T.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Invalid Account Number or PIN. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Signup s = new Signup();
            this.Hide();
            s.ShowDialog();
            this.Close();
        }
    }
}
