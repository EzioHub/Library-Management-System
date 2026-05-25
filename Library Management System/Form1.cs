using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Library.BLL;
using Library.DAL.Models;

namespace Library_Management_System
{
    public partial class Form1 : Form
    {
        private int _selectedBookID = -1;

        public Form1()
        {
            InitializeComponent();
            SetupUI();
            LoadData();
        }

        private void btnNavDashboard_Click(object sender, EventArgs e)
        {
            pnlDashboard.Visible = true;
            pnlBooks.Visible = false;
            UpdateDashboard();
        }

        private void btnNavBooks_Click(object sender, EventArgs e)
        {
            pnlDashboard.Visible = false;
            pnlBooks.Visible = true;
        }

        private void SetupUI()
        {
            this.Text = "Library Management System";
            dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBooks.MultiSelect = false;
            dgvBooks.ReadOnly = true;
            dgvBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            
            // Disable update/delete until selection
            btn_Update.Enabled = false;
            btn_delete.Enabled = false;

          
            dgvBooks.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgvBooks.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvBooks.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgvBooks.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvBooks.BackgroundColor = Color.White;

            dgvBooks.EnableHeadersVisualStyles = false;
            dgvBooks.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvBooks.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgvBooks.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void LoadData()
        {
            try
            {
                var books = BookBLL.GetBooks();
                dgvBooks.DataSource = books;
                
                // Refresh stats 
                UpdateDashboard();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateDashboard()
        {
            try
            {
                var stats = BookBLL.GetStatistics();
                lblTotalBooks.Text = $"Total Unique Titles: {stats["TotalBooks"]}";
                lblTotalQuantity.Text = $"Total Books in Stock: {stats["TotalQuantity"]}";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating dashboard: " + ex.Message);
            }
        }

        private void ClearFields()
        {
            title_Textbox.Clear();
            Author_Textbox.Clear();
            Category_Textbox.Clear();
            Price_Textbox.Clear();
            Quantity_Textbox.Clear();
            _selectedBookID = -1;
            btn_Update.Enabled = false;
            btn_delete.Enabled = false;
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(title_Textbox.Text))
                {
                    MessageBox.Show("Please enter a Title", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (string.IsNullOrWhiteSpace(Author_Textbox.Text))
                {
                    MessageBox.Show("Please enter a Author", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (string.IsNullOrWhiteSpace(Category_Textbox.Text))
                {
                    MessageBox.Show("Please enter a Category", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (string.IsNullOrWhiteSpace(Category_Textbox.Text))
                {
                    MessageBox.Show("Please enter a Category", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                    
                    decimal price;
                if (!decimal.TryParse(Price_Textbox.Text, out price))
                {
                    MessageBox.Show("Please enter a valid price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int quantity;
                if (!int.TryParse(Quantity_Textbox.Text, out quantity))
                {
                    MessageBox.Show("Please enter a valid quantity.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool result = BookBLL.AddBook(
                    title_Textbox.Text,
                    Author_Textbox.Text,
                    Category_Textbox.Text,
                    price,
                    quantity
                );

                if (result)
                {
                    MessageBox.Show("Book added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Failed to add book.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (_selectedBookID == -1) return;

            var confirmResult = MessageBox.Show("Are you sure you want to delete this book?",
                                     "Confirm Delete",
                                     MessageBoxButtons.YesNo,
                                     MessageBoxIcon.Question);
            
            if (confirmResult == DialogResult.Yes)
            {
                if (BookBLL.DeleteBook(_selectedBookID))
                {
                    LoadData();
                    MessageBox.Show("Book deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Failed to delete book.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            if (_selectedBookID == -1) return;

            try
            {
                decimal price;
                if (!decimal.TryParse(Price_Textbox.Text, out price))
                {
                    MessageBox.Show("Please enter a valid price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int quantity;
                if (!int.TryParse(Quantity_Textbox.Text, out quantity))
                {
                    MessageBox.Show("Please enter a valid quantity.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool result = BookBLL.UpdateBook(
                    _selectedBookID,
                    title_Textbox.Text,
                    Author_Textbox.Text,
                    Category_Textbox.Text,
                    price,
                    quantity
                );

                if (result)
                {
                    MessageBox.Show("Book updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Failed to update book.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            try
            {
                var books = BookBLL.GetBooks();
                if (books == null || !books.Any())
                {
                    MessageBox.Show("No data to export.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                using(SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "CSV Files (*.CSV)|*.CSV";
                    sfd.FileName = "LibraryInventory.csv";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        var lines = new List<string>();
                        lines.Add("BookID,Title,Author,Category,Price,Quantity");
                        foreach (var book in books)
                        {
                            lines.Add($"{book.BookID},{book.Title},{book.Author},{book.Category},{book.Price},{book.Quantity}");
                        }
                        File.WriteAllLines(sfd.FileName, lines);
                        MessageBox.Show("Data exported successfully to " + sfd.FileName, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error exporting data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               throw;
            }


          
        }

        private void dgvBooks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvBooks.Rows[e.RowIndex];
                _selectedBookID = Convert.ToInt32(row.Cells["BookID"].Value);
                title_Textbox.Text = row.Cells["Title"].Value.ToString();
                Author_Textbox.Text = row.Cells["Author"].Value.ToString();
                Category_Textbox.Text = row.Cells["Category"].Value?.ToString();
                Price_Textbox.Text = row.Cells["Price"].Value.ToString();
                Quantity_Textbox.Text = row.Cells["Quantity"].Value.ToString();

                btn_Update.Enabled = true;
                btn_delete.Enabled = true;
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(text_Search.Text))
            {
                MessageBox.Show(
                    "Please enter search text",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }
            string search = text_Search.Text;
            dgvBooks.DataSource = BookBLL.SearchBooks(search);
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            text_Search.Clear();
            LoadData();
        }

        private void dgvBooks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            dgvBooks_CellClick(sender, e);
        }

        private void btn_Refresh_Click_1(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
