namespace BookStoreServerNet.Models.Responses;

public class BookResponse
{
    //This should normally also be a DTO.
    public List<Book>? content { get; set; }
    public int totalPages { get; set; }
}