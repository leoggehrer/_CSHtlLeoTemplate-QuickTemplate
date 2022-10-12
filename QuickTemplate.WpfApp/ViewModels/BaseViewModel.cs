﻿//@CodeCopy
//MdStart
using System.ComponentModel;

namespace QuickTemplate.WpfApp.ViewModels
{
    public abstract partial class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
//MdEnd
