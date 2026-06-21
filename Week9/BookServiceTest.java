package Week9;

import com.fs.bookdemo.model.Book;
import com.fs.bookdemo.service.BookService;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import java.util.*;

import static org.junit.jupiter.api.Assertions.*;
public class BookServiceTest {

    private BookService bookService;

    @BeforeEach
    public void setUp() {
        bookService = new BookService();
    }

    @Test
    public void testGetAllBooks_EmptyResult(){
        List<Book> result = bookService.getAllBooks();
        assertNull(result);
    }
    @Test
    public void testAddBook(){
        Book b1 = new Book(1L,"Kitap-1","Yazar-1");
        Book addedBook = bookService.addBook(b1);
        assertNotNull(addedBook);
        assertEquals(addedBook.getTitle(),b1.getTitle());
        assertEquals(addedBook.getAuthor(),b1.getAuthor());
        assertEquals(addedBook.getId(),b1.getId());
    }

    @Test
    public void testNullAddBook(){
        Book b1 = new Book(-1L,"Kitap-1","Yazar-1");
        Book addedBook = bookService.addBook(b1);
        assertNull(addedBook);
    }

    @Test
    public void testGetAllBooks(){
        Book b1 = new Book(1L,"Kitap-1","Yazar-1");
        Book b2 = new Book(2L,"Kitap-2","Yazar-2");
        Book b3 = new Book(3L,"Kitap-3","Yazar-3");
        Book b4 = new Book(4L,"Kitap-4","Yazar-4");
        Book b5 = new Book(5L,"Kitap-5","Yazar-5");
        bookService.addBook(b1);
        bookService.addBook(b2);
        bookService.addBook(b3);
        bookService.addBook(b4);
        bookService.addBook(b5);
        List<Book> books = bookService.getAllBooks();
        assertNotNull(books);
        assertEquals(5,books.size());

    }

    @Test
    public void testGetBookById(){
        Book b1 = new Book(1L,"Kitap-1","Yazar-1");
        Book b2 = new Book(2L,"Kitap-2","Yazar-2");
        Book b3 = new Book(3L,"Kitap-3","Yazar-3");
        Book b4 = new Book(4L,"Kitap-4","Yazar-4");
        Book b5 = new Book(5L,"Kitap-5","Yazar-5");
        bookService.addBook(b1);
        bookService.addBook(b2);
        bookService.addBook(b3);
        bookService.addBook(b4);
        bookService.addBook(b5);
        Book book = bookService.getBookById(b2.getId());
        assertNotNull(book);
        assertEquals(book.getId(),b2.getId());
        assertEquals(book,b2);


        Book result2 = bookService.getBookById(13L);
        assertNull(result2);

    }

    @Test
    public void testDeleteBookById(){
        Book b1 = new Book(1L,"Kitap-1","Yazar-1");
        Book b2 = new Book(2L,"Kitap-2","Yazar-2");
        Book b3 = new Book(3L,"Kitap-3","Yazar-3");
        Book b4 = new Book(4L,"Kitap-4","Yazar-4");
        Book b5 = new Book(5L,"Kitap-5","Yazar-5");
        bookService.addBook(b1);
        bookService.addBook(b2);
        bookService.addBook(b3);
        bookService.addBook(b4);
        bookService.addBook(b5);

        boolean result = bookService.deleteBookById(3L);
        boolean result2 = bookService.deleteBookById(13L);
        assertTrue(result);
        assertFalse(result2);
    }
}
