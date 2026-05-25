using System;
using System.Collections.Generic;
using Library.DAL;
using Library.DAL.Models;

namespace Library.BLL
{
    public class BookBLL
    {
        public static List<Book> GetBooks()
        {
            try
            {
                return BookDAL.GetAllBooks();
            }
            catch (Exception ex)
            {
                // Here you could log the error
                throw new Exception("Error retrieving books from database.", ex);
            }
        }

        public static bool AddBook(string title, string author, string category, decimal price, int quantity)
        {
            // Validation logic
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title is required.");
            if (string.IsNullOrWhiteSpace(author)) throw new ArgumentException("Author is required.");
            if (price < 0) throw new ArgumentException("Price cannot be negative.");
            if (quantity < 0) throw new ArgumentException("Quantity cannot be negative.");

            Book newBook = new Book
            {
                Title = title,
                Author = author,
                Category = category,
                Price = price,
                Quantity = quantity
            };

            return BookDAL.AddBook(newBook);
        }

        public static bool UpdateBook(int bookID, string title, string author, string category, decimal price, int quantity)
        {
            if (bookID <= 0) throw new ArgumentException("Invalid Book ID.");
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title is required.");
            if (string.IsNullOrWhiteSpace(author)) throw new ArgumentException("Author is required.");
            if (price < 0) throw new ArgumentException("Price cannot be negative.");
            if (quantity < 0) throw new ArgumentException("Quantity cannot be negative.");

            Book bookToUpdate = new Book
            {
                BookID = bookID,
                Title = title,
                Author = author,
                Category = category,
                Price = price,
                Quantity = quantity
            };

            return BookDAL.UpdateBook(bookToUpdate);
        }

        public static bool DeleteBook(int bookID)
        {
            if (bookID <= 0) return false;
            return BookDAL.DeleteBook(bookID);
        }

        public static List<Book> SearchBooks(string searchText)
        {
           
            return BookDAL.SearchBooks(searchText);
        }

        public static Dictionary<string, int> GetStatistics()
        {
            List<Book> books = GetBooks();
            var stats = new Dictionary<string, int>
            {
                { "TotalBooks", books.Count },
                { "TotalQuantity", 0 }
            };

            foreach (var book in books)
            {
                stats["TotalQuantity"] += book.Quantity;
            }

            return stats;
        }
    }
}
