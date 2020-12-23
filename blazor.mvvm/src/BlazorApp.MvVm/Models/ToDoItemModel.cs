using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.MvVm.Models
{
    public class ToDoItemModel
    {
        public Guid Id { get; set; }
        public bool Done { get; set; }
        public DateTime? Date { get; set; }
        [Required(ErrorMessage ="Esto es obligatorio capullo")]
        public string Title { get; set; }
        public string Notes { get; set; }
    }
}
