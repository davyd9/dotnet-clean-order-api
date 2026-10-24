using Microsoft.AspNetCore.Mvc;
using TaskInventory.Api.Controllers;
using TaskInventory.Api.Models;

namespace TaskInventory.Tests;

public class ApiControllersTests
{
    [Fact]
    public void ProductsController_GetAll_ReturnsOkWithSeededProducts()
    {
        var controller = new ProductsController();
        var result = controller.GetAll();
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var items = Assert.IsAssignableFrom<IEnumerable<ProductItem>>(okResult.Value);
        Assert.NotEmpty(items);
    }

    [Fact]
    public void TasksController_Create_AddsNewTask()
    {
        var controller = new TasksController();
        var dto = new CreateTaskDto
        {
            Title = "Verify inventory export",
            Description = "Run monthly CSV check",
            Priority = "Low"
        };

        var result = controller.Create(dto);
        var created = Assert.IsType<CreatedAtActionResult>(result.Result);
        var task = Assert.IsType<TaskItem>(created.Value);
        Assert.Equal("Verify inventory export", task.Title);
        Assert.False(task.IsCompleted);
    }
}
