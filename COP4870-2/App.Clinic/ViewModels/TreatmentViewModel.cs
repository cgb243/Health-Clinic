using System.ComponentModel;
using System.Windows.Input;
using Library.Clinic.Models;
using Library.Clinic.Services;

namespace Library.Clinic.ViewModels
{
    public class TreatmentViewModel : INotifyPropertyChanged
    {
        private Treatment treatment;
        private Patient patient;
        private decimal insuranceCoverage;
        private decimal insuredPrice;

        //public ICommand SaveCommand { get; }
        //public ICommand CancelCommand { get; }

        public TreatmentViewModel()
        {
            treatment = new Treatment();
            //SaveCommand = new Command(ExecuteSave);
            //CancelCommand = new Command(ExecuteCancel);
        }

        public TreatmentViewModel(Treatment treatment)
        {
            this.treatment=treatment;

        }

        public TreatmentViewModel(int treatmentId)
        {
            if (treatmentId == 0)
            {
                treatment = new Treatment();
            }
            else
            {
                treatment = TreatmentServiceProxy.Current.Treatments
                    .FirstOrDefault(t => t.Id == treatmentId) ?? new Treatment();
            }

            //SaveCommand = new Command(ExecuteSave);
            //CancelCommand = new Command(ExecuteCancel);
        }

        // private async void ExecuteSave()
        // {
        //     TreatmentServiceProxy.Current.AddOrUpdateTreatment(treatment);
        //     await Shell.Current.GoToAsync("..");
        // }

        // private async void ExecuteCancel()
        // {
        //     await Shell.Current.GoToAsync("..");
        // }


        public TreatmentViewModel(Treatment treatment, Patient patient)
        {
            this.treatment = treatment;
            this.patient = patient;
            CalculateInsuranceDetails();
        }

        public int Id
        {
            get => treatment?.Id ?? -1;
            set
            {
                if (treatment != null && treatment.Id != value)
                {
                    treatment.Id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        public string Title
        {
            get => treatment?.Title ?? string.Empty;
            // Setter if needed...
        }

        public decimal UninsuredPrice
        {
            get => treatment?.uninsuredPrice ?? 0m;
            // Setter if needed...
        }

        public decimal InsuranceCoverage
        {
            get => insuranceCoverage;
            private set
            {
                if (insuranceCoverage != value)
                {
                    insuranceCoverage = value;
                    OnPropertyChanged(nameof(InsuranceCoverage));
                    CalculateInsuredPrice();
                }
            }
        }

        public decimal InsuredPrice
        {
            get => insuredPrice;
            private set
            {
                if (insuredPrice != value)
                {
                    insuredPrice = value;
                    OnPropertyChanged(nameof(InsuredPrice));
                }
            }
        }

        private void CalculateInsuranceDetails()
        {
            if (patient?.InsurancePlan != null)
            {
                // Assume CoveragePercentage is a decimal value like 0.80 for 80%
                decimal coveragePercentage = patient.InsurancePlan.CoveragePercentage;

                // Calculate the insurance coverage amount
                InsuranceCoverage = UninsuredPrice * coveragePercentage;

                // Calculate the insured price
                InsuredPrice = UninsuredPrice - InsuranceCoverage;
            }
            else
            {
                // No insurance plan, so coverage is zero
                InsuranceCoverage = 0m;
                InsuredPrice = UninsuredPrice;
            }
        }

        private void CalculateInsuredPrice()
        {
            InsuredPrice = UninsuredPrice - InsuranceCoverage;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public void ExecuteAdd()
        {
            if (treatment != null)
            {
                TreatmentServiceProxy
                .Current
                .AddOrUpdateTreatment(treatment);
            }

            Shell.Current.GoToAsync("//Treatments");
        }
    }
}
