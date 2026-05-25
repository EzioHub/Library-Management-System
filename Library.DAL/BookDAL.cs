using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Library.DAL.Models;
using System.Windows;
using System.Windows.Forms;
namespace Library.DAL
{
    public static class DataAccessSettings
    {
       
        public static string ConnectionString = "Server=.;Database=LibraryDB;Integrated Security=True";
    }

    public class BookDAL
    {
        public static List<Book> GetAllBooks()
        {
            List<Book> books = new List<Book>();

            try
            {
                using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
                {
                    string query = "SELECT BookID, Title, Author, Category, Price, Quantity FROM Books";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                books.Add(new Book
                                {
                                    BookID = Convert.ToInt32(reader["BookID"]),
                                    Title = reader["Title"].ToString(),
                                    Author = reader["Author"].ToString(),
                                    Category = reader["Category"].ToString(),
                                    Price = Convert.ToDecimal(reader["Price"]),
                                    Quantity = Convert.ToInt32(reader["Quantity"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               
                Console.WriteLine("Error in GetAllBooks: " + ex.Message);
                throw; 
            }

            return books;
        }

        public static bool AddBook(Book book)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
                {
                    string query = @"INSERT INTO Books (Title, Author, Category, Price, Quantity) 
                                    VALUES (@Title, @Author, @Category, @Price, @Quantity)";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = book.Title;
                        command.Parameters.Add("@Author", SqlDbType.NVarChar).Value = book.Author;
                        command.Parameters.Add("@Category", SqlDbType.NVarChar).Value = book.Category;
                        command.Parameters.Add("@Price", SqlDbType.Decimal).Value = book.Price;
                        command.Parameters.Add("@Quantity", SqlDbType.Int).Value = book.Quantity;

                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in AddBook: " + ex.Message);
                return false;
            }

            return rowsAffected > 0;
        }

        public static bool UpdateBook(Book book)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
                {
                    string query = @"UPDATE Books 
                                    SET Title = @Title, Author = @Author, Category = @Category, 
                                        Price = @Price, Quantity = @Quantity 
                                    WHERE BookID = @BookID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@BookID", SqlDbType.Int).Value = book.BookID;
                        command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = book.Title;
                        command.Parameters.Add("@Author", SqlDbType.NVarChar).Value = book.Author;
                        command.Parameters.Add("@Category", SqlDbType.NVarChar).Value = book.Category;
                        command.Parameters.Add("@Price", SqlDbType.Decimal).Value = book.Price;
                        command.Parameters.Add("@Quantity", SqlDbType.Int).Value = book.Quantity;

                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in UpdateBook: " + ex.Message);
                return false;
            }

            return rowsAffected > 0;
        }

        public static bool DeleteBook(int bookID)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
                {
                    string query = "DELETE FROM Books WHERE BookID = @BookID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@BookID", SqlDbType.Int).Value = bookID;
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DeleteBook: " + ex.Message);
                return false;
            }

            return rowsAffected > 0;
        }

        public static List<Book> SearchBooks(string searchText)
        {
            List<Book> books = new List<Book>();
            try
            {
                using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
                {
                    string query = @"SELECT * FROM Books 
                                    WHERE Title LIKE @Search 
                                    OR Author LIKE @Search 
                                    OR Category LIKE @Search";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@Search", SqlDbType.NVarChar).Value = "%" + searchText + "%";
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                books.Add(new Book
                                {
                                    BookID = Convert.ToInt32(reader["BookID"]),
                                    Title = reader["Title"].ToString(),
                                    Author = reader["Author"].ToString(),
                                    Category = reader["Category"].ToString(),
                                    Price = Convert.ToDecimal(reader["Price"]),
                                    Quantity = Convert.ToInt32(reader["Quantity"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                   ex.Message,
                   "Error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
            }

            return books;
        }
    }
}
