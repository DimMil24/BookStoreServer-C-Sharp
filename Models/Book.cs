using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace BookStoreServerNet.Models;

public class Book
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Column("isbn13")]
    public long Isbn13 { get; set; }
    [Column("isbn10")]
    public string? Isbn10 { get; set; }
    [Column("title")]
    public string Title { get; set; }
    [Column("subtitle")]
    public string? Subtitle { get; set; }
    [Column("authors")]
    public string? Authors { get; set; }
    [Column("categories")]
    public string? Categories { get; set; }
    [Column("thumbnail")]
    public string? Thumbnail { get; set; }
    [Column("description")]
    public string? Description { get; set; }
    [Column("published_year")]
    public int? Year { get; set; }
    [Column("average_rating")]
    [JsonPropertyName("average_rating")]
    public double? AverageRating { get; set; }
    [Column("num_pages")]
    [JsonPropertyName("num_pages")]
    public int? NumberOfPages { get; set; }
    [Column("ratings_count")]
    [JsonPropertyName("ratings_count")]
    public int? RatingsCount { get; set; }
    [Column("price")]
    public int? Price { get; set; }
}