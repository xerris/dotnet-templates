namespace Xerris.WebApi1
{
    public class TodoItem : TodoItemProperties
    {
        public int Id { get; set; }
    }

    public class TodoItemProperties
    {
        public string Name { get; set; }

        public bool IsComplete { get; set; }
    }
}
