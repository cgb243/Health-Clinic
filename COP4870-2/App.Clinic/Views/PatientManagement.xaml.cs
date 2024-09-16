using System.ComponentModel;
using System.Runtime.CompilerServices;
using App.Clinic.ViewModels;
using Library.Clinic.Models;
using Library.Clinic.Services;

namespace App.Clinic.Views;

public partial class PatientManagement : ContentPage, INotifyPropertyChanged
{
	public PatientManagement()
	{
		InitializeComponent();
		BindingContext = new PatientManagementViewModel();
	}

	private void CancelClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//MainPage");
	}

	private void AddClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//PatientDetails");
	}

	private void DeleteClicked(object sender, EventArgs e)
	{
		//PatientServiceProxy.Current.DeletePatient((BindingContext as PatientManagementViewModel)?.SelectedPatient.Id ?? 0);
		(BindingContext as PatientManagementViewModel)?.Delete();

		
	}

	
	private void PatientManagement_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		(BindingContext as PatientManagementViewModel)?.Refresh();
	}
}