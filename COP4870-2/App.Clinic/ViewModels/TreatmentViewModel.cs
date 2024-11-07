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


        public TreatmentViewModel()
        {
            treatment = new Treatment();
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
        }

  

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
        set
        {
            if (treatment != null && treatment.Title != value)
            {
                treatment.Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
    }

    public decimal UninsuredPrice
    {
        get => treatment?.uninsuredPrice ?? 0m;
        set
        {
            if (treatment != null && treatment.uninsuredPrice != value)
            {
                treatment.uninsuredPrice = value;
                OnPropertyChanged(nameof(UninsuredPrice));
            }
        }
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
                decimal coveragePercentage = patient.InsurancePlan.CoveragePercentage;

                InsuranceCoverage = UninsuredPrice * coveragePercentage;

                InsuredPrice = UninsuredPrice - InsuranceCoverage;
            }
            else
            {
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

        public async void ExecuteAdd()
        {
            if (treatment != null)
            {
                TreatmentServiceProxy.Current.AddOrUpdateTreatment(treatment);
            }

            await Shell.Current.GoToAsync("//Treatments");
        }

    }
}
