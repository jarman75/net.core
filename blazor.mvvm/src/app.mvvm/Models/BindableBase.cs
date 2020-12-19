using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace app.mvvm.Models
{
    public abstract class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null) 
        {            
            if (EqualityComparer<T>.Default.Equals(field,value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}