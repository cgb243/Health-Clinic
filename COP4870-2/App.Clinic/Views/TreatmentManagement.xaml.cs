using App.Clinic.ViewModels;
using Library.Clinic.ViewModels;
using Microsoft.Maui.Controls;
using System;

namespace App.Clinic.Views
{
    public partial class TreatmentManagement : ContentPage
    {
        public TreatmentManagement()
        {
            InitializeComponent();
            BindingContext = new TreatmentManagementViewModel();
        }

        private void CancelClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//MainPage");
        }

        private async void AddClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//TreatmentDetails?treatmentId=0");
        }

        private async void EditClicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as TreatmentManagementViewModel;
            var selectedTreatmentId = viewModel?.SelectedTreatment?.Id ?? 0;
            await Shell.Current.GoToAsync($"//TreatmentDetails?treatmentId={selectedTreatmentId}");
        }

        private void DeleteClicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as TreatmentManagementViewModel;
            if (viewModel?.SelectedTreatment != null)
            {
                viewModel.DeleteCommand.Execute(viewModel.SelectedTreatment);
            }
        }

        private void TreatmentManagement_NavigatedTo(object sender, NavigatedToEventArgs e)
        {
            (BindingContext as TreatmentManagementViewModel)?.Refresh();
        }

        private void RefreshClicked(object sender, EventArgs e)
        {
            (BindingContext as TreatmentManagementViewModel)?.Refresh();
        }
    }
}
