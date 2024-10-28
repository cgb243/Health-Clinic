using Library.Clinic.Models;
using Library.Clinic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;


namespace App.Clinic.ViewModels
{
    public class PatientViewModel
    {
        public Patient? Model { get; set; }
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
            get => Model?.Name ?? string.Empty;
            set
            {
                if(Model != null)
                {
                    Model.Name = value;
                }
            }
        }

        public ObservableCollection<Insurance> Insurances => new ObservableCollection<Insurance>(InsuranceServiceProxy.Current.Insurances);


        public Insurance? SelectedInsurance
        {
            get => Model?.InsurancePlan;
            set
            {
                if (Model != null)
                {
                    Model.InsurancePlan = value;
                    Model.InsurancePlan.insuranceId = value?.insuranceId ?? 0;
                }
            }
        }

        public string InsurancePlan
        {
            get => Model?.InsurancePlan?.Title ?? string.Empty;
        }

        public void SetupCommands()
        {
            DeleteCommand = new Command(DoDelete);
            EditCommand = new Command((p) => DoEdit(p as PatientViewModel));
        }

        private void DoDelete()
        {
            if (Id > 0)
            {
                PatientServiceProxy.Current.DeletePatient(Id);
                Shell.Current.GoToAsync("//Patients");
            }
        }

        private void DoEdit(PatientViewModel? pvm)
        {
            if (pvm == null)
            {
                return;
            }
            var selectedPatientId = pvm?.Id ?? 0;
            Shell.Current.GoToAsync($"//PatientDetails?patientId={selectedPatientId}");
        }

        public PatientViewModel()
        {
            Model = new Patient();
            SetupCommands();
        }

        public PatientViewModel(Patient? _model)
        {
            Model = _model;
            SetupCommands();
        }

        public /*async*/ void ExecuteAdd()
        {
            if (Model != null)
            {
                /*await*/ PatientServiceProxy
                .Current
                .AddOrUpdatePatient(Model);
            }

            /*await*/ Shell.Current.GoToAsync("//Patients");
        }
    }
}