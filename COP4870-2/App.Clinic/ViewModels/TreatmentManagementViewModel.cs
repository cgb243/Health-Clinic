using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Library.Clinic.Models;
using Library.Clinic.Services;

namespace Library.Clinic.ViewModels
{
    public class TreatmentManagementViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<TreatmentViewModel> treatments;
        private Patient patient;
        private string? query;

        public TreatmentManagementViewModel(Patient patient)
        {
            this.patient = patient;
            treatments = new ObservableCollection<TreatmentViewModel>();
            LoadTreatments();

            EditCommand = new Command<TreatmentViewModel>(DoEdit);
            DeleteCommand = new Command<TreatmentViewModel>(DoDelete);
        }

        public TreatmentManagementViewModel()
        {
            treatments = new ObservableCollection<TreatmentViewModel>();
            LoadTreatments();

            EditCommand = new Command<TreatmentViewModel>(DoEdit);
            DeleteCommand = new Command<TreatmentViewModel>(DoDelete);
        }

        public ICommand EditCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        public ObservableCollection<TreatmentViewModel> Treatments
        {
            get => treatments;
            set
            {
                if (treatments != value)
                {
                    treatments = value;
                    OnPropertyChanged(nameof(Treatments));
                }
            }
        }

        public string? Query
        {
            get => query;
            set
            {
                if (query != value)
                {
                    query = value;
                    OnPropertyChanged();
                }
            }
        }

        public TreatmentViewModel? SelectedTreatment { get; set; }

        private void LoadTreatments()
        {
            Treatments.Clear();
            var treatmentModels = TreatmentServiceProxy.Current.Treatments;

            foreach (var treatment in treatmentModels)
            {
                Treatments.Add(new TreatmentViewModel(treatment, patient));
            }
        }

        public void Refresh()
        {
            Treatments.Clear();
            var treatmentModels = TreatmentServiceProxy.Current.Treatments;

            if (!string.IsNullOrEmpty(Query))
            {
                treatmentModels = treatmentModels
                    .Where(t => t.Title.Contains(Query, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            foreach (var treatment in treatmentModels)
            {
                Treatments.Add(new TreatmentViewModel(treatment, patient));
            }
        }

        private void DoEdit(TreatmentViewModel treatmentViewModel)
        {
            if (treatmentViewModel == null) return;
            var selectedTreatmentId = treatmentViewModel.Id;
            Shell.Current.GoToAsync($"//TreatmentDetails?treatmentId={selectedTreatmentId}");
        }

        private void DoDelete(TreatmentViewModel treatmentViewModel)
        {
            if (treatmentViewModel == null) return;
            TreatmentServiceProxy.Current.DeleteTreatment(treatmentViewModel.Id);
            Treatments.Remove(treatmentViewModel);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void EditTreatment(TreatmentViewModel treatmentViewModel)
        {
            if (treatmentViewModel == null) return;
            await Shell.Current.GoToAsync($"TreatmentView?treatmentId={treatmentViewModel.Id}");
        }

    }
}
