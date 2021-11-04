using System.ComponentModel.DataAnnotations;

namespace Xerris.WebApi1
{
    public class TodoItem : TodoItemProperties
    {
        public int Id { get; init; }
    }

    public class TodoItemProperties
    {
        /// <summary>
        /// The name of the item.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Whether or not the item is complete
        /// </summary>
        [Required]
        public bool IsComplete { get; set; }
    }
}
