namespace Books.Services.Interfaces;

public interface IBookService
{
    Task<IEnumerable<Book.Models.Book>> GetAllBooks();
    Task<Book.Models.Book> GetById(int id);
    Task<IEnumerable<Book.Models.Book>>GetByAuthorId(int id);
    Task<Book.Models.Book> CreateBook(Book.Models.Book book);
    Task<Book.Models.Book> UpdateBook(Book.Models.Book book);
    Task<bool> DeleteBook(int id);
}