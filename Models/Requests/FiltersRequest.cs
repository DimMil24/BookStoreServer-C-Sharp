using Microsoft.AspNetCore.Mvc;

namespace BookStoreServerNet.Models.Requests;

public class FiltersRequest
{
    [ModelBinder(Name = "y2")]
    public int YearLow { get; set; } = 1970;
    [ModelBinder(Name = "y1")]
    public int YearHigh { get; set; } = 2023;
    [ModelBinder(Name = "r1")]
    public double RatingLow { get; set; } = 0.0;
    [ModelBinder(Name = "r2")]
    public double RatingHigh { get; set; } = 5.0;
    [ModelBinder(Name = "p1")]
    public int PriceLow { get; set; } = 1;
    [ModelBinder(Name = "p2")]
    public int PriceHigh { get; set; } = 150;
    [ModelBinder(Name = "c")]
    public string? Category { get; set; }
    [ModelBinder(Name = "search")]
    public string? SearchTerm { get; set; }
    [ModelBinder(Name = "ps")]
    public int PageSize { get; set; } = 6;
    [ModelBinder(Name = "order_by")]
    public string OrderBy { get; set; } = "Isbn13";
    [ModelBinder(Name = "desc")]
    public bool OrderByDescending { get; set; } = false;
}