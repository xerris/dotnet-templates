using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Xerris.WebApi1.Infrastructure;
using Xerris.WebApi1.Models;

namespace Xerris.WebApi1.Controllers;

[ApiController]
[Route("todos")]
public class TodosController : ControllerBase
{
    private static readonly IEnumerable<TodoItem> TodoItems = new[]
    {
        new TodoItem { Id = 1, Name = "Water cats", IsComplete = false },
        new TodoItem { Id = 2, Name = "Feed lawn", IsComplete = false },
        new TodoItem { Id = 3, Name = "Mow plants", IsComplete = true }
    };

    /// <summary>
    ///     Create a new todo item.
    /// </summary>
    /// <param name="props">The properties to create the item with.</param>
    [HttpPost]
    [ApiConventionMethod(typeof(AuthorizedApiConventions), nameof(AuthorizedApiConventions.Post))]
    public ActionResult<TodoItem> Post(TodoItemProperties props)
    {
        var item = new TodoItem
        {
            Id = 0,
            Name = props.Name,
            IsComplete = props.IsComplete
        };

        return CreatedAtAction(nameof(Get), item.Id, item);
    }

    /// <summary>
    ///     Fetch the list of todo items.
    /// </summary>
    /// <returns>The todo items.</returns>
    [HttpGet]
    [ApiConventionMethod(typeof(AuthorizedApiConventions), nameof(AuthorizedApiConventions.GetAll))]
    public ActionResult<IEnumerable<TodoItem>> Get()
    {
        return Ok(TodoItems);
    }

    /// <summary>
    ///     Fetch a todo item.
    /// </summary>
    /// <param name="id">The ID of the todo item.</param>
    /// <returns>The todo item, if found.</returns>
    [HttpGet("{id:int}")]
    [ApiConventionMethod(typeof(AuthorizedApiConventions), nameof(AuthorizedApiConventions.Get))]
    [Description("Get a todo item.")]
    public ActionResult<TodoItem> Get(int id)
    {
        var item = TodoItems.FirstOrDefault(x => x.Id == id);

        if (item == null)
            return NotFound();

        return item;
    }

    /// <summary>
    ///     Update a todo item.
    /// </summary>
    /// <param name="id">The ID of the todo item.</param>
    /// <param name="props">The properties to update the item with.</param>
    /// <returns>The updated item, if found.</returns>
    [HttpPut("{id:int}")]
    [ApiConventionMethod(typeof(AuthorizedApiConventions), nameof(AuthorizedApiConventions.Put))]
    public ActionResult<TodoItem> Put(int id, TodoItemProperties props)
    {
        var item = TodoItems.FirstOrDefault(x => x.Id == id);

        if (item == null)
            return NotFound();

        item.Name = props.Name;
        item.IsComplete = props.IsComplete;

        return Ok(item);
    }

    /// <summary>
    ///     Delete a todo item.
    /// </summary>
    /// <param name="id">The ID of the todo item to delete.</param>
    [HttpDelete("{id:int}")]
    [ApiConventionMethod(typeof(AuthorizedApiConventions), nameof(AuthorizedApiConventions.Delete))]
    public ActionResult<TodoItem> Delete(int id)
    {
        var item = TodoItems.FirstOrDefault(x => x.Id == id);

        if (item == null)
            return NotFound();

        return NoContent();
    }
}
