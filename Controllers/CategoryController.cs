using BookStoreServerNet.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreServerNet.Controllers;

[Route("api/")]
[ApiController]
public class CategoryController : ControllerBase
{
    
    private readonly CategoryService _service;

    public CategoryController(CategoryService service)
    {
        _service = service;
    }

    [HttpGet("category")]
    public async Task<IActionResult> GetCategories()
    {
        var data = await _service.GetAllCategoriesAsync();
        return Ok(data);
    }
}