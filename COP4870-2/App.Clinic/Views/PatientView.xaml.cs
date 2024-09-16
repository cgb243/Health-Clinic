using System.ComponentModel;
using Library.Clinic.Models;
using Library.Clinic.Services;

namespace App.Clinic.Views;

public partial class PatientView : ContentPage
{
	public PatientView()
	{
		//InitializeComponent();
		BindingContext=new Patient();
	}

	private void CancelClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//Patients");
	}

	private void AddClicked(object sender, EventArgs e)
	{
		var PatientToAdd = BindingContext as Patient;
		if (PatientToAdd != null)
		{
			PatientServiceProxy.Current.AddPatient(PatientToAdd);
		}
		Shell.Current.GoToAsync("//Patients");
	}

	private void PatientView_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		BindingContext=new Patient();
	}
}