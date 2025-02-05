using Microsoft.AspNetCore.Mvc;

namespace BookStoreServerNet.Models.Requests;

public class FiltersRequest
{
    public int YearLow { get; set; } = 1970;
    public int YearHigh { get; set; } = 2023;
    public double RatingLow { get; set; } = 0.0;
    public double RatingHigh { get; set; } = 5.0;
    public int PriceLow { get; set; } = 1;
    public int PriceHigh { get; set; } = 150;
    public string? Category { get; set; }
    public string? Search { get; set; }
    public int PageSize { get; set; } = 6;
    [ModelBinder(Name = "order_by")]
    public string OrderBy { get; set; } = "Isbn13";
    [ModelBinder(Name = "desc")]
	public bool OrderByDescending { get; set; } = false;
}