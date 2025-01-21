namespace BookStoreServerNet.Models.Responses;

public class BookResponse
{
    public List<Book>? content { get; set; }
    public int totalPages { get; set; }
}