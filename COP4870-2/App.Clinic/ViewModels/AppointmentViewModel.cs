using Library.Clinic.Models;
using Library.Clinic.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace App.Clinic.ViewModels
{
    public class AppointmentViewModel
    {
        private TimeSpan _startTime;
        private DateTime _startDate;
        private TimeOption? _selectedStartTime;
        private DurationOption? _selectedDurationOption;

        public Appointment? Model { get; set; }

        public ICommand? DeleteCommand { get; set; }
        public ICommand? EditCommand { get; set; }

        public int Id
        {
            get => Model?.Id ?? -1;
            set
            {
                if (Model != null && Model.Id != value)
                {
                    Model.Id = value;
                }
            }
        }

        public string Name
        {
            get => Model?.Title ?? string.Empty;
            set
            {
                if (Model != null)
                {
                    Model.Title = value;
                }
            }
        }

        public string PatientName
        {
            get
            {
                if (Model != null && Model.PatientId > 0)
                {
                    if (Model.patient == null)
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
                if (Model != null && Model.PhysicianId > 0)
                {
                    if (Model.physician == null)
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

        public string StartDateDisplay => Model?.StartTime?.ToString("d") ?? string.Empty;
        public string StartTimeDisplay => Model?.StartTime?.ToString("t") ?? string.Empty;

        public string DurationDisplay
        {
            get
            {
                if (Model?.EndTime != null && Model?.StartTime != null)
                {
                    var duration = Model.EndTime.Value - Model.StartTime.Value;
                    return $"{duration.TotalMinutes} minutes";
                }
                return string.Empty;
            }
        }

        public Patient? SelectedPatient
        {
            get => Model?.patient;
            set
            {
                if (Model != null)
                {
                    Model.patient = value;
                    Model.PatientId = value?.Id ?? 0;
                }
            }
        }

        public Physician? SelectedPhysician
        {
            get => Model?.physician;
            set
            {
                if (Model != null)
                {
                    Model.physician = value;
                    Model.PhysicianId = value?.Id ?? 0;
                }
            }
        }

        public DateTime MinStartDate => DateTime.Today;

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    RefreshTime();
                }
            }
        }

        
        public TimeSpan StartTime
        {
            get => _startTime;
            set
            {
                if (_startTime != value)
                {
                    _startTime = value;
                    _selectedStartTime = StartTimes.FirstOrDefault(option => option.Time == _startTime);
                    RefreshTime();
                }
            }
        }

        public TimeOption? SelectedStartTime
        {
            get => _selectedStartTime;
            set
            {
                if (_selectedStartTime != value)
                {
                    _selectedStartTime = value;
                    if (_selectedStartTime != null)
                    {
                        if (_startTime != _selectedStartTime.Time)
                        {
                            StartTime = _selectedStartTime.Time;
                        }
                        RefreshTime();
                    }
                }
            }
        }

        public DurationOption? SelectedDurationOption
        {
            get => _selectedDurationOption;
            set
            {
                if (_selectedDurationOption != value)
                {
                    _selectedDurationOption = value;
                    RefreshTime();
                }
            }
        }

        public ObservableCollection<Patient> Patients => new ObservableCollection<Patient>(PatientServiceProxy.Current.Patients);
        public ObservableCollection<Physician> Physicians => new ObservableCollection<Physician>(PhysicianServiceProxy.Current.Physicians);

        public List<DurationOption> DurationOptions { get; } = new List<DurationOption>
        {
            new DurationOption { Value = 30 },
            new DurationOption { Value = 60 },
            new DurationOption { Value = 90 },
            new DurationOption { Value = 120 },
        };

        public List<TimeOption> StartTimes { get; } = GenerateStartTimes();

        public class DurationOption
        {
            public int Value { get; set; }
            public string DisplayText => $"{Value} minutes";
        }

        public class TimeOption
        {
            public TimeSpan Time { get; set; }
            public string DisplayText => DateTime.Today.Add(Time).ToString("h:mm tt");
        }

        public AppointmentViewModel()
        {
            Model = new Appointment();
            InitializePropertiesFromModel();
            SetupCommands();
        }

        public AppointmentViewModel(Appointment? _model)
        {
            Model = _model ?? new Appointment();
            InitializePropertiesFromModel();
            SetupCommands();
        }

        private void InitializePropertiesFromModel()
        {
            _startDate = Model.StartTime?.Date ?? DateTime.Today;

            _startTime = Model.StartTime?.TimeOfDay ?? new TimeSpan(9, 0, 0);
            _selectedStartTime = StartTimes.FirstOrDefault(option => option.Time == _startTime);
            if (_selectedStartTime == null)
            {
                _selectedStartTime = new TimeOption { Time = _startTime };
                StartTimes.Add(_selectedStartTime);
            }

            int durationMinutes = 30; 
            if (Model.StartTime.HasValue && Model.EndTime.HasValue)
            {
                durationMinutes = (int)(Model.EndTime.Value - Model.StartTime.Value).TotalMinutes;
            }
            _selectedDurationOption = DurationOptions.FirstOrDefault(d => d.Value == durationMinutes);

            if (_selectedDurationOption == null)
            {
                _selectedDurationOption = new DurationOption { Value = durationMinutes };
                DurationOptions.Add(_selectedDurationOption);
            }

            SelectedPatient = Model.patient ?? PatientServiceProxy.Current.Patients.FirstOrDefault(p => p.Id == Model.PatientId);
            SelectedPhysician = Model.physician ?? PhysicianServiceProxy.Current.Physicians.FirstOrDefault(p => p.Id == Model.PhysicianId);

            RefreshTime();
        }

       
        public void RefreshTime()
        {
            if (Model != null)
            {
                var date = StartDate.Date.Add(StartTime);
                Model.StartTime = date;

                if (SelectedDurationOption != null)
                {
                    Model.EndTime = date.AddMinutes(SelectedDurationOption.Value);
                }
                else
                {
                    Model.EndTime = null;
                }
            }
        }

        
        private static List<TimeOption> GenerateStartTimes()
        {
            var times = new List<TimeOption>();
            var startTime = new TimeSpan(9, 0, 0); 
            var endTime = new TimeSpan(17, 0, 0);  

            while (startTime <= endTime)
            {
                times.Add(new TimeOption { Time = startTime });
                startTime = startTime.Add(new TimeSpan(0, 15, 0)); 
            }

            return times;
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
            var selectedAppointmentId = pvm.Id;
            Shell.Current.GoToAsync($"//AppointmentDetails?appointmentId={selectedAppointmentId}");
        }

        public async Task ExecuteAddAsync()
        {
            if (Model != null)
            {
                RefreshTime();
                bool conflict = await AppointmentServiceProxy
                    .Current
                    .AddOrUpdateAppointment(Model);

                if (!conflict)
                {
                    await Shell.Current.DisplayAlert("Conflict", "The appointment you want to schedule either has conflicts or is outside of Business Hours.", "OK");
                }
                else
                {
                    await Shell.Current.GoToAsync("//Appointments");
                }
            }
            else
            {
                await Shell.Current.GoToAsync("//Appointments");
            }
        }
    }
}
