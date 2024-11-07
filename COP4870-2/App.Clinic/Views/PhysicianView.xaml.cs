using App.Clinic.ViewModels;
using Library.Clinic.Models;
using Library.Clinic.Services;

namespace App.Clinic.Views;

[QueryProperty(nameof(PhysicianId), "physicianId")]
public partial class PhysicianView : ContentPage
{
    private int physicianId;
    public int PhysicianId
    {
        get => physicianId;
        set
        {
            physicianId = value;
            LoadPhysician();
        }
    }

    public PhysicianView()
    {
        InitializeComponent();
    }

    private void LoadPhysician()
    {
        if (PhysicianId > 0)
        {
            var model = PhysicianServiceProxy.Current
                .Physicians.FirstOrDefault(p => p.Id == PhysicianId);
            if (model != null)
            {
                BindingContext = new PhysicianViewModel(model);
            }
            else
            {
                BindingContext = new PhysicianViewModel();
            }
        }
        else
        {
            BindingContext = new PhysicianViewModel();
        }
    }

    private async void CancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Physicians");
    }

    private void AddClicked(object sender, EventArgs e)
    {
        (BindingContext as PhysicianViewModel)?.ExecuteAdd();
    }

}
