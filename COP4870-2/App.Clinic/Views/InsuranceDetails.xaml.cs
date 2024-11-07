using App.Clinic.ViewModels;
using Library.Clinic.Models;
using Library.Clinic.Services;
using System.Collections.Generic;
using Library.Clinic.ViewModels;

namespace App.Clinic.Views
{
    public partial class InsuranceDetails : ContentPage, IQueryAttributable
    {
        private int _appointmentId;
        private TreatmentManagementViewModel viewModel;

        public InsuranceDetails()
        {
            InitializeComponent();
        }

		protected override void OnAppearing()
		{
			base.OnAppearing();
		}


        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("appointmentId"))
            {
                _appointmentId = Convert.ToInt32(query["appointmentId"]);

                LoadAppointmentData();
            }
            else
            {
                DisplayAlert("Error", "No appointment ID provided.", "OK");
            }
        }

        private void LoadAppointmentData()
        {
            var appointment = AppointmentServiceProxy.Current.GetAppointmentById(_appointmentId);

            if (appointment == null)
            {
                DisplayAlert("Error", "Appointment not found.", "OK");
                return;
            }

            var patient = appointment.patient ?? PatientServiceProxy.Current.GetPatientById(appointment.PatientId);

            if (patient == null)
            {
                DisplayAlert("Error", "Patient not found for this appointment.", "OK");
                return;
            }

            viewModel = new TreatmentManagementViewModel(patient);

            this.BindingContext = viewModel;
        }

        public void BackClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Appointments");
        }
    }
}
