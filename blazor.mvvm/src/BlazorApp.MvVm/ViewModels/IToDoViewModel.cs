using BlazorApp.MvVm.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace BlazorApp.MvVm.ViewModels
{
    public interface IToDoViewModel 
    {
        public bool IsBusy { get; set; }
        public List<ToDoItemModel> ToDoItemList { get; }
        public ToDoItemModel ToDoItem { get; set; }
        public int ItemsToDo { get; }
                
        event PropertyChangedEventHandler PropertyChanged;
        public void SaveToDoItem(ToDoItemModel toDoItem);
    }
}
