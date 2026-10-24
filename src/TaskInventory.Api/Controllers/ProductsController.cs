using Microsoft.AspNetCore.Mvc;
using TaskInventory.Api.Models;

namespace TaskInventory.Api.Controllers;

[ApiController]
[Route("api/products")]
[Produces("application/json")]
public class ProductsController : ControllerBase
{
    private static readonly List<ProductItem> Products = new()
    {
        new ProductItem { Id = 1, Name = "Dell Latitude 5440 Notebook", Sku = "NB-DELL-5440", Category = "Hardware", UnitPrice = 4250.00m, StockQuantity = 18 },
        new ProductItem { Id = 2, Name = "Logitech MX Master 3S Mouse", Sku = "PER-LOG-MX3S", Category = "Peripherals", UnitPrice = 549.90m, StockQuantity = 42 },
        new ProductItem { Id = 3, Name = "LG UltraWide 29\" IPS Monitor", Sku = "MON-LG-29UW", Category = "Monitors", UnitPrice = 1190.00m, StockQuantity = 11 }
    };

    [HttpGet]
    public ActionResult<IEnumerable<ProductItem>> GetAll([FromQuery] string? category = null)
    {
        if (!string.IsNullOrWhiteSpace(category))
        {
            return Ok(Products.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase)));
        }
        return Ok(Products);
    }

    [HttpGet("{id:int}")]
    public ActionResult<ProductItem> GetById(int id)
    {
        var item = Products.FirstOrDefault(p => p.Id == id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public ActionResult<ProductItem> Create([FromBody] CreateProductDto dto)
    {
        var nextId = Products.Count > 0 ? Products.Max(p => p.Id) + 1 : 1;
        var item = new ProductItem
        {
            Id = nextId,
            Name = dto.Name,
            Sku = dto.Sku,
            Category = dto.Category,
            UnitPrice = dto.UnitPrice,
            StockQuantity = dto.StockQuantity,
            UpdatedAt = DateTime.UtcNow
        };
        Products.Add(item);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public ActionResult<ProductItem> UpdateStock(int id, [FromBody] CreateProductDto dto)
    {
        var item = Products.FirstOrDefault(p => p.Id == id);
        if (item is null) return NotFound();

        item.Name = dto.Name;
        item.Sku = dto.Sku;
        item.Category = dto.Category;
        item.UnitPrice = dto.UnitPrice;
        item.StockQuantity = dto.StockQuantity;
        item.UpdatedAt = DateTime.UtcNow;

        return Ok(item);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var item = Products.FirstOrDefault(p => p.Id == id);
        if (item is null) return NotFound();
        Products.Remove(item);
        return NoContent();
    }
}
