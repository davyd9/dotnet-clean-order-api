using Microsoft.AspNetCore.Mvc;
using TaskInventory.Api.Models;

namespace TaskInventory.Api.Controllers;

[ApiController]
[Route("api/categories")]
[Produces("application/json")]
public class CategoriesController : ControllerBase
{
    private static readonly List<CategoryItem> Categories = new()
    {
        new CategoryItem { Id = 1, Name = "Hardware", ActiveProducts = 18 },
        new CategoryItem { Id = 2, Name = "Peripherals", ActiveProducts = 42 },
        new CategoryItem { Id = 3, Name = "Monitors", ActiveProducts = 11 }
    };

    [HttpGet]
    public ActionResult<IEnumerable<CategoryItem>> GetAll() => Ok(Categories);
}
