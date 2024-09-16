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

namespace App.Clinic.ViewModels;

public class PatientManagementViewModel : INotifyPropertyChanged
{

	public event PropertyChangedEventHandler? PropertyChanged;

	public ObservableCollection<Patient> Patients
	{
		get
		{
			return new ObservableCollection<Patient>(PatientServiceProxy.Current.Patients);
		}
	}

	public Patient? SelectedPatient { get; set; }
	private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	public void Delete()
	{
		if (SelectedPatient == null)
		{
			return;
		}
		PatientServiceProxy.Current.DeletePatient(SelectedPatient.Id);

		Refresh();
	}

	public void Refresh()
	{
		NotifyPropertyChanged("Patients");
	}
	
}