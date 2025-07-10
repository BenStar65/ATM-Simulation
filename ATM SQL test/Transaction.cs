using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ATM_SQL_test
{
    public partial class Transaction : Form
    {
        private string _connectionString;
        private BankAccount _account;

        public Transaction(BankAccount account, string connectionString)
        {
            InitializeComponent();
            _account = account;
            _connectionString = connectionString;
            LoadTransactions();
        }

        private void LoadTransactions()
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "SELECT Type, Amount, BalanceAfter, Timestamp FROM Transactions WHERE AccountNumber = @acctNo ORDER BY Timestamp DESC";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@acctNo", _account.AccountNumber.ToString());
                    using (var reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        dataGridView1.DataSource = dt;
                    }
                }
            }
        }

        private void Transaction_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 mainForm = new Form1(_account); // Reuse the same account object
            mainForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrintPage += (s, ev) =>
            {
                int y = 100;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    string line = $"{row.Cells["Type"].Value} - R{row.Cells["Amount"].Value} - Balance: R{row.Cells["BalanceAfter"].Value} - {row.Cells["Timestamp"].Value}";
                    ev.Graphics.DrawString(line, new Font("Arial", 10), Brushes.Black, new PointF(100, y));
                    y += 30;
                }
            };

            PrintPreviewDialog preview = new PrintPreviewDialog();
            preview.Document = printDoc;
            preview.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
