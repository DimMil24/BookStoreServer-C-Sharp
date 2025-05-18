using System.ComponentModel.DataAnnotations;

namespace BookStoreServerNet.Models.Requests;

public class NewBookRequest
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Must be a number greater than 0")]
    public long Isbn13 { get; set; }
    public string? Isbn10 { get; set; }
    [Required]
    public string Title { get; set; }
    public string? Subtitle { get; set; }
    [Required]
    public string? Authors { get; set; }
    public string? Categories { get; set; }
    public string? Thumbnail { get; set; }
    public string? Description { get; set; }
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Must be a number greater than 0")]
    public int? Year { get; set; }
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Must be a number greater than 0")]
    public double? AverageRating { get; set; }
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Must be a number greater than 0")]
    public int? NumberOfPages { get; set; }
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Must be a number greater than 0")]
    public int? RatingsCount { get; set; }
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Must be a number greater than 0")]
    public int? Price { get; set; }
}