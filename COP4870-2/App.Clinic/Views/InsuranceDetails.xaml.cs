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
			// No need to retrieve data here since it's done in ApplyQueryAttributes
		}


        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            // Ensure the query contains the "appointmentId" key
            if (query.ContainsKey("appointmentId"))
            {
                // Retrieve and parse the appointment ID
                _appointmentId = Convert.ToInt32(query["appointmentId"]);

                // Proceed to retrieve the appointment and patient
                LoadAppointmentData();
            }
            else
            {
                // Handle the case where the appointmentId is missing
                DisplayAlert("Error", "No appointment ID provided.", "OK");
            }
        }

        private void LoadAppointmentData()
        {
            // Retrieve the appointment using the _appointmentId
            var appointment = AppointmentServiceProxy.Current.GetAppointmentById(_appointmentId);

            if (appointment == null)
            {
                DisplayAlert("Error", "Appointment not found.", "OK");
                return;
            }

            // Get the patient from the appointment
            var patient = appointment.patient ?? PatientServiceProxy.Current.GetPatientById(appointment.PatientId);

            if (patient == null)
            {
                DisplayAlert("Error", "Patient not found for this appointment.", "OK");
                return;
            }

            // Initialize the ViewModel with the patient
            viewModel = new TreatmentManagementViewModel(patient);

            // Set the BindingContext of the page to the ViewModel
            this.BindingContext = viewModel;
        }

        public void BackClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Appointments");
        }
    }
}
