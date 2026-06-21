package Week9;

import com.fs.bookdemo.model.*;
import com.fs.bookdemo.service.*;
import org.junit.jupiter.api.*;
import org.springframework.beans.factory.annotation.*;
import org.springframework.boot.test.context.*;
import org.springframework.boot.webmvc.test.autoconfigure.*;
import org.springframework.http.MediaType;
import org.springframework.test.web.servlet.*;
import tools.jackson.databind.*;

import static org.mockito.ArgumentMatchers.*;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.*;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.*;

@SpringBootTest
@AutoConfigureMockMvc
public class BookControllerIntegrationTest {
    @Autowired
    private MockMvc mockMvc;

    @Autowired
    private BookService bookService;

    @Autowired
    private ObjectMapper objectMapper;


    @Test
    public void addBook_thenGetAllBooks() throws Exception{
        Book b1 = new Book(1L,"Kitap-1","Yazar-1");
        mockMvc.perform(
                post("/api/books")
                        .contentType(MediaType.APPLICATION_JSON)
                        .content(objectMapper.writeValueAsString(b1)
                        ))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$.title").value(b1.getTitle()))
                .andExpect(jsonPath("$.author").value(b1.getAuthor()))
        ;

        mockMvc.perform(
                get("/api/books"))
                .andExpect(jsonPath("$.size()").value(1))
                .andExpect(jsonPath("$[0].id").value(b1.getId()))
                .andExpect(jsonPath("$[0].title").value(b1.getTitle()))
                .andExpect(jsonPath("$[0].author").value(b1.getAuthor()));
    }


    @Test
    public void getBookById_Found() throws  Exception{
        Book b1 = new Book(1L,"Kitap-1","Yazar-1");
        mockMvc.perform(post("/api/books").contentType(MediaType.APPLICATION_JSON).content(objectMapper.writeValueAsString(b1)));

        mockMvc.perform(get("/api/books/"+b1.getId())).andExpect(status().is(200)).andExpect(jsonPath("$.id").value(b1.getId())).andExpect(jsonPath("$.title").value(b1.getTitle())).andExpect(jsonPath("$.author").value(b1.getAuthor()));
    }

    @Test
    public void getBookById_NotFound() throws  Exception{
        mockMvc.perform(get("/api/books/"+anyLong()))
                .andExpect(status().isNotFound())
                .andExpect(content().string(""));
    }


    @Test
    public void deleteBookById_Found() throws  Exception{
        Book b1 = new Book(1L,"Kitap-1","Yazar-1");
        bookService.addBook(b1);
        mockMvc.perform(delete("/api/books/"+b1.getId())).andExpect(status().is(200)).andExpect(content().string("Book Deleted Successfully"));
    }

    @Test
    public void deleteBookById_NotFound()  throws  Exception{
        mockMvc.perform(delete("/api/books/"+anyLong())).andExpect(content().string("Book Not Found"));
    }

    @Test
    public void addBook_InvalidId_ShouldReturnNullBody() throws Exception{
        // BookService.addBook(-1L) null döndürüyor, controller da body olarak null dönecek
        Book b1 = new Book(-5L,"Kitap-1","Yazar-1");
        mockMvc.perform(
                post("/api/books").contentType(MediaType.APPLICATION_JSON).content(objectMapper.writeValueAsString(b1))).andExpect(content().string(""));
    }
}
