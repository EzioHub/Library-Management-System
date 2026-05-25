using System;

namespace Library.DAL.Models
{
    public class Book
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Book() { }

        public Book(int bookID, string title, string author, string category, decimal price, int quantity)
        {
            BookID = bookID;
            Title = title;
            Author = author;
            Category = category;
            Price = price;
            Quantity = quantity;
        }
    }
}
