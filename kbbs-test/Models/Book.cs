using System;

namespace kbbs_test.Models
{
    public class Book
    {
        public Guid Reference { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Genre { get; set; }
        public int YearPublished { get; set; }
        public int Pages { get; set; }
        public Book(string title, string author, string isbn, string genre, int yearPublished, int pages)
        {
            Reference = Guid.NewGuid();
            Title = title;
            Author = author;
            ISBN = isbn;
            Genre = genre;
            YearPublished = yearPublished;
            Pages = pages;
        }
    }
}
