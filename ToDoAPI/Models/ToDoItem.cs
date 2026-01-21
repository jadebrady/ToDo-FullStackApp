using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }


}