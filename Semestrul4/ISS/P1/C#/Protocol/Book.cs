using System;

namespace Protocol
{
    public class Book
    {
        public Book()
        {
        }

        public Book(string title, string author, string isbn, int count)
        {
            if (title != null)
                if (author != null)
                    if (isbn != null)
                        if (title.Length > 0)
                            if (count >= 0)
                                if (isbn.Length >= 10)
                                {
                                    Title = title;
                                    Author = author;
                                    Isbn = isbn;
                                    Count = count;
                                }
                                else
                                    throw new ArgumentException("The ISBN must be at least 10 characters long.");
                            else
                                throw new ArgumentException("The book count cannot be negative!");
                        else
                            throw new ArgumentException("The title length must be at least 1");
                    else
                        throw new ArgumentNullException("The provided value for isbn cannot be null!");
                else
                    throw new ArgumentNullException("The provided value for author cannot be null!");
            else
                throw new ArgumentNullException("The provided value for title cannot be null!");
        }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Isbn { get; set; }

        public int Count { get; set; }
    }
}
