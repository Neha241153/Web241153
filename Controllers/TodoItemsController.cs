using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationDemo.Model;

namespace WebApplicationDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private static readonly List<TodoItem> _todoItems = new()
        {
            new TodoItem {
                Id=1,
                Name="Learn .NET Core",
                IsComplete = true,
                Secret="Shh"
            },
            new TodoItem
            {
                Id=2,
                Name="Build Web API",
                IsComplete = false,
                Secret="Secret-2"
            }
        };
        private static long _nextId=3;
        [HttpGet]
        public ActionResult<List<TodoItem>> GetTodoItems()
        {
            return _todoItems;
        }

        [HttpGet("{id}")]
        public ActionResult<TodoItem>GetTodoItem(long id)
        {
            var todoItem = _todoItems.FirstOrDefault(t => t.Id == id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return todoItem;
        }

        [HttpPost]
        public void PostTodoItem(TodoItem todoItem)
        {
            todoItem.Id = _nextId;
            _todoItems.Add(todoItem);
        }
        [HttpDelete("{id}")]
        public void DeleteTodoItem(long id)
        {
            var todoItem = _todoItems.FirstOrDefault(t => t.Id == id);
         
            _todoItems.Remove(todoItem);
        }

        [HttpPut("{id}")]
        public IActionResult PutTodoItem (long id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }
            var previous = _todoItems.FirstOrDefault(t => t.Id == id);
            if (previous == null)
            {
                return NotFound();
            }
            previous.Name = todoItem.Name;
            previous.IsComplete = todoItem.IsComplete;
            return NoContent();
        }
    }
}
