using System.ComponentModel;
using Library.Clinic.Models;
using Library.Clinic.Services;

namespace App.Clinic.Views;

[QueryProperty(nameof(PatientId), "patientId")]
public partial class PatientView : ContentPage
{
	public PatientView()
	{
		InitializeComponent();
		//BindingContext=new Patient();
	}

	public int PatientId {get; set; }

	private void CancelClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//Patients");
	}

	private void AddClicked(object sender, EventArgs e)
	{
		var PatientToAdd = BindingContext as Patient;
		if (PatientToAdd != null)
		{
			PatientServiceProxy.Current.AddOrUpdatePatient(PatientToAdd);
		}
		Shell.Current.GoToAsync("//Patients");
	}

	private void PatientView_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		//TODO: This neeeds to be in a view model
		if(PatientId > 0)
		{
			BindingContext=PatientServiceProxy.Current.Patients.FirstOrDefault(p => p.Id == PatientId);
		} else
		{
			BindingContext = new Patient();
		}

		
	}
}