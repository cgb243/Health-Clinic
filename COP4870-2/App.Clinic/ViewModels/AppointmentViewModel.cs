using Library.Clinic.Models;
using Library.Clinic.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace App.Clinic.ViewModels
{
    public class AppointmentViewModel
    {
        public Appointment? Model { get; set; }
        public ICommand? DeleteCommand { get; set; }
        public ICommand? EditCommand { get; set; }
        public int Id
        {
            get
            {
                if(Model == null)
                {
                    return -1;
                }

                return Model.Id;
            }

            set
            {
                if(Model != null && Model.Id != value) {
                    Model.Id = value;
                }
            }
        }

        public string Name
        {
            get => Model?.Title ?? string.Empty;
            set
            {
                if(Model != null)
                {
                    Model.Title = value;
                }
            }
        }


        public string PatientName
        {
            get
            {
                if(Model != null && Model.PatientId > 0)
                {
                    if(Model.patient == null)
                    {
                        Model.patient = PatientServiceProxy
                        .Current
                        .Patients
                        .FirstOrDefault(p => p.Id == Model.PatientId);
                    }
                }
                return Model?.patient?.Name ?? string.Empty;


            }
        }

        public string PhysicianName
        {
            get
            {
                if(Model != null && Model.PhysicianId > 0)
                {
                    if(Model.physician == null)
                    {
                        Model.physician = PhysicianServiceProxy
                        .Current
                        .Physicians
                        .FirstOrDefault(p => p.Id == Model.PhysicianId);
                    }
                }
                return Model?.physician?.Name ?? string.Empty;


            }
        }



        public Patient? SelectedPatient {
            get
            {
                return Model?.patient;
            }
            set{
                var selectedPatient = value;
                if(Model!=null)
                {
                    Model.patient = selectedPatient;
                    Model.PatientId = selectedPatient?.Id ?? 0;
                }

            }
        }

        public Physician? SelectedPhysician {
            get
            {
                return Model?.physician;
            }
            set{
                var selectedPhysician = value;
                if(Model!=null)
                {
                    Model.physician = selectedPhysician;
                    Model.PhysicianId = selectedPhysician?.Id ?? 0;
                }

            }
        }



        public ObservableCollection<Patient> Patients {
            get{
                return new ObservableCollection<Patient>(PatientServiceProxy.Current.Patients);
            }
        }

        public ObservableCollection<Physician> Physicians {
            get{
                return new ObservableCollection<Physician>(PhysicianServiceProxy.Current.Physicians);
            }
        }

        public void SetupCommands()
        {
            DeleteCommand = new Command(DoDelete);
            EditCommand = new Command((p) => DoEdit(p as AppointmentViewModel));
        }

        private void DoDelete()
        {
            if (Id > 0)
            {
                AppointmentServiceProxy.Current.DeleteAppointment(Id);
                Shell.Current.GoToAsync("//Appointments");
            }
        }

        private void DoEdit(AppointmentViewModel? pvm)
        {
            if (pvm == null)
            {
                return;
            }
            var selectedAppointmentId = pvm?.Id ?? 0;
            Shell.Current.GoToAsync($"//AppointmentDetails?appointmentId={selectedAppointmentId}");
        }

        public AppointmentViewModel()
        {
            Model = new Appointment();
            SetupCommands();
        }

        public AppointmentViewModel(Appointment? _model)
        {
            Model = _model;
            SetupCommands();
        }

        public void ExecuteAdd()
        {
            if (Model != null)
            {
            
                AppointmentServiceProxy
                .Current
                .AddOrUpdateAppointment(Model);
            }

            Shell.Current.GoToAsync("//Appointments");
        }
    }
}