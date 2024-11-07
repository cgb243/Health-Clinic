using Library.Clinic.Models;
using Library.Clinic.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

public class TreatmentSelectionViewModel : INotifyPropertyChanged
{
    public Treatment Treatment { get; set; }

    private bool isSelected;
    public bool IsSelected
    {
        get => isSelected;
        set
        {
            if (isSelected != value)
            {
                isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }
    }

    public TreatmentSelectionViewModel(Treatment treatment)
    {
        Treatment = treatment;
        IsSelected = false;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
