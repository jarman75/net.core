using BlazorApp.MvVm.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorApp.MvVm.ViewModels
{
    public class ToDoViewModel : BaseViewModel, IToDoViewModel
    {
        private List<ToDoItemModel> toDoItemList = new List<ToDoItemModel>();
        public List<ToDoItemModel> ToDoItemList { get => toDoItemList; private set => SetValue(ref toDoItemList, value); }
        
        private ToDoItemModel toDoItem = new ToDoItemModel();
        public ToDoItemModel ToDoItem { get => toDoItem; set => SetValue(ref toDoItem, value); }

        public int ItemsToDo => ToDoItemList.Where(i => i.Done.Equals(false)).Count();

        public void SaveToDoItem(ToDoItemModel toDoItem)
        {
            IsBusy = true;
            if (toDoItem.Id.Equals(Guid.Empty))
                toDoItem.Id = Guid.NewGuid();
            else
                toDoItemList.Remove(toDoItem);

            toDoItemList.Add(toDoItem);

            OnPropertyChanged(nameof(ToDoItemList));
            IsBusy = false;
        }
    }
}
