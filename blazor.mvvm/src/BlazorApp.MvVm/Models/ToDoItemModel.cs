using System;

namespace BlazorApp.MvVm.Models
{
    public class ToDoItemModel
    {
        public Guid Id { get; set; }
        public bool Done { get; set; }
        public DateTime? Date { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
    }
}
