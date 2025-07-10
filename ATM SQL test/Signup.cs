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
    public partial class Signup : Form
    {
        
        public Signup()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text.Trim();
            string surname = textBox2.Text.Trim();
            string pin = textBox3.Text.Trim();
            string confirmPin = textBox4.Text.Trim();

            // Step 0: Get selected Gender
            string gender = "";
            if (radioButton1.Checked) // Assume radioButton1 = "MR"
            {
                gender = "MR";
            }
            else if (radioButton2.Checked) // Assume radioButton2 = "MS"
            {
                gender = "MS";
            }
            else if (radioButton3.Checked) // Assume radioButton3 = "MRS"
            {
                gender = "MRS";
            }
            else
            {
                MessageBox.Show("Please select a gender.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Stop if no gender selected
            }

            // 1. Check if PINs match
            if (pin != confirmPin)
            {
                MessageBox.Show("PINs do not match! Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Check if PIN is 4 digits
            if (pin.Length != 4 || !int.TryParse(pin, out _))
            {
                MessageBox.Show("PIN must be exactly 4 digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. Insert into database
            string connectionString = "server=localhost;port=3363;user=root;password=#Samsunset7;database=ATM;SslMode=Required;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Notice Gender is added now
                    string sql = "INSERT INTO Accounts (Namess, Surname, PIN, Gender, Money) VALUES (@name, @surname, @pin, @gender, @Money)";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@surname", surname);
                    cmd.Parameters.AddWithValue("@pin", pin);
                    cmd.Parameters.AddWithValue("@gender", gender);
                    cmd.Parameters.AddWithValue("@Money", 10); // Start with 10 bucks

                    int rowsInserted = cmd.ExecuteNonQuery();

                    if (rowsInserted > 0)
                    {
                        // Get auto-generated Account Number
                        long accountNumber = cmd.LastInsertedId;

                        MessageBox.Show($"User registered successfully!\nYour Account Number is: {accountNumber}",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Clear textboxes and radio buttons
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                        textBox4.Clear();
                        radioButton1.Checked = false;
                        radioButton2.Checked = false;
                        radioButton3.Checked = false;
                    }
                    else
                    {
                        MessageBox.Show("Registration failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            Login L = new Login();
            this.Hide();
            L.ShowDialog();
            this.Close();
        }
    }
}
