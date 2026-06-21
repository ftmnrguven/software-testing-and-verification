package Week9;

import com.fs.bookdemo.controller.*;
import com.fs.bookdemo.model.*;
import com.fs.bookdemo.service.*;
import org.junit.jupiter.api.*;
import org.springframework.beans.factory.annotation.*;
import org.springframework.boot.webmvc.test.autoconfigure.*;
import org.springframework.http.MediaType;
import org.springframework.test.context.bean.override.mockito.*;
import org.springframework.test.web.servlet.*;
import org.springframework.test.web.servlet.request.*;
import tools.jackson.databind.*;
import java.util.*;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.*;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.*;
import static org.mockito.Mockito.*;



//Test Ettiğimiz Sınıfı Yazıyoruz
@WebMvcTest(BookController.class)
public class BookControllerTest {

    @Autowired
    private MockMvc mockMvc;

    @MockitoBean
    private BookService bookService;

    @Autowired
    private ObjectMapper objectMapper;

    @Test
    public void testGetAllBooks() throws Exception{
        when(bookService.getAllBooks()).thenReturn(Arrays.asList(
                new Book(1L,"Kitap-1","Yazar-1"),
                new Book(2L,"Kitap-2","Yazar-2")
        ));


        mockMvc.perform(get("/api/books"))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$.size()").value(2));
    }

    @Test
    public void testAddBook() throws Exception{
        Book b1 = new Book(11L,"Kitap-1","Yazar-1");
        when(bookService.addBook(any(Book.class))).thenReturn(b1);

        mockMvc.perform(post("/api/books")
                .contentType(MediaType.APPLICATION_JSON)
                .content(objectMapper.writeValueAsString(b1)))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$.title").value(b1.getTitle()))
                .andExpect(jsonPath("$.author").value(b1.getAuthor()))
        ;
    }

    @Test
    public void testGetBookById() throws Exception{
        long id = 5L;
        when(bookService.getBookById(id)).thenReturn(new Book(1L,"Kitap-1","Yazar-1"));
        mockMvc.perform(get("/api/books/"+id)).andExpect(status().isOk()).andExpect(jsonPath("$.title").value("Kitap-1"));
    }


    @Test
    public void testDeleteBook()  throws Exception{
        when(bookService.deleteBookById(anyLong())).thenReturn(true);
        mockMvc.perform(delete("/api/books/"+anyLong())).andExpect(content().string("Book Deleted Successfully")).andExpect(status().isOk());
    }

    @Test
    public void testDeleteBook_NotFound()  throws Exception{
        when(bookService.deleteBookById(anyLong())).thenReturn(false);
        mockMvc.perform(delete("/api/books/"+anyLong())).andExpect(content().string("Book Not Found"));
    }



}
