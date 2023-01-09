namespace Book.Models;

public class Book
{
    public int Id { get; set; }
    public Author Author { get; set; }
    public string Title { get; set; }
    
    public Book(){}

    public Book(int id, Author author, string title)
    {
        Id = id;
        Author = author;
        Title = title;
    }
}