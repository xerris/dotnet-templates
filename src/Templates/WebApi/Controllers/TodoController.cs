using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Xerris.WebApi1.Infrastructure;

namespace Xerris.WebApi1.Controllers
{
    [ApiController]
    [Route("todos")]
    public class TodoController : ControllerBase
    {
        private static readonly IEnumerable<TodoItem> TodoItems = new[]
        {
            new TodoItem { Id = 1, Name = "Water cats", IsComplete = false },
            new TodoItem { Id = 2, Name = "Feed lawn", IsComplete = false },
            new TodoItem { Id = 3, Name = "Mow plants", IsComplete = true},
        };

        [HttpPost]
        [ApiConventionMethod(typeof(AuthorizedApiConventions), nameof(AuthorizedApiConventions.Post))]
        [Description("Create a new all todo item.")]
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

        [HttpGet]
        [ApiConventionMethod(typeof(AuthorizedApiConventions), nameof(AuthorizedApiConventions.GetAll))]
        [Description("Get all todo items.")]
        public ActionResult<IEnumerable<TodoItem>> Get()
        {
            return Ok(TodoItems);
        }

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
        
        [HttpPut("{id:int}")]
        [ApiConventionMethod(typeof(AuthorizedApiConventions), nameof(AuthorizedApiConventions.Put))]
        [Description("Update a todo item.")]
        public ActionResult<TodoItem> Put(int id, TodoItemProperties props)
        {
            var item = TodoItems.FirstOrDefault(x => x.Id == id);

            if (item == null)
                return NotFound();

            item.Name = props.Name;
            item.IsComplete = props.IsComplete;

            return Ok(item);
        }

        [HttpDelete("{id:int}")]
        [ApiConventionMethod(typeof(AuthorizedApiConventions), nameof(AuthorizedApiConventions.Delete))]
        [Description("Delete a todo item.")]
        public ActionResult<TodoItem> Delete(int id)
        {
            var item = TodoItems.FirstOrDefault(x => x.Id == id);

            if (item == null)
                return NotFound();

            return NoContent();
        }
    }
}
