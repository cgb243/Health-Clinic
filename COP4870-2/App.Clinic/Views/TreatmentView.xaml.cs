using Library.Clinic.Services;
using Library.Clinic.ViewModels;
using Microsoft.Maui.Controls;
using System;
using System.Linq;

namespace App.Clinic.Views
{
    [QueryProperty(nameof(TreatmentId), "treatmentId")]
    public partial class TreatmentView : ContentPage
    {
        public int TreatmentId { get; set; }

        public TreatmentView()
        {
            InitializeComponent();
        }

        private void TreatmentView_NavigatedTo(object sender, NavigatedToEventArgs e)
        {
            if (TreatmentId > 0)
            {
                var model = TreatmentServiceProxy.Current
                    .Treatments.FirstOrDefault(p => p.Id == TreatmentId);
                if (model != null)
                {
                    BindingContext = new TreatmentViewModel(model);
                }
                else
                {
                    BindingContext = new TreatmentViewModel();
                }
            }
            else
            {
                BindingContext = new TreatmentViewModel();
            }
        }

        private async void AddClicked(object sender, EventArgs e)
        {
            (BindingContext as TreatmentViewModel)?.ExecuteAdd();
            await Shell.Current.GoToAsync("//Treatments");
        }

        private async void CancelClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//Treatments");
        }
    }
}
