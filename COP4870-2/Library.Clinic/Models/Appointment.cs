using System;


namespace Library.Clinic.Models
{
   public class Appointment
   {
       public int Id {get; set;}
       public int PatientId {get; set;}
        public int PhysicianId {get; set;}
       public string Title {get; set;}
       public DateTime? StartTime { get; set; }

       public DateTime? EndTime { get; set; }
       public string Description { get; set; }
       public string Location { get; set; }
       public Patient? patient { get; set;}
       public Physician? physician {get; set;}

        public List<Treatment> Treatments { get; set; }
        public Insurance InsurancePlan => patient?.InsurancePlan; // set up
        
        public decimal TotalUninsuredPrice
        {
            get
            {
                if (Treatments == null || !Treatments.Any())
                    return 0m;

                return Treatments.Sum(t => t.uninsuredPrice);
            }
        }

        public decimal TotalInsuredPrice
        {
            get
            {
                if (Treatments == null || !Treatments.Any())
                    return 0m;

                return Treatments.Sum(t => InsurancePlan != null ? InsurancePlan.GetInsuredPrice(t) : t.uninsuredPrice);
            }
        }

       public Appointment ()
       {
            Title = string.Empty;
            Location = string.Empty;
            Description = string.Empty;
            StartTime = DateTime.MinValue;
            EndTime = DateTime.MinValue;
            patient = new Patient();
            physician = new Physician();
            CalculateTreatments();
       }

        public void CalculateTreatments()
        {

            if (Treatments == null)
            {
                Treatments = new List<Treatment>();
            }
            
            // foreach (var treatment in Treatments)
            // {
            //     decimal uninsuredPrice = treatment.uninsuredPrice;
            //     decimal insuredPrice = InsurancePlan.GetInsuredPrice(treatment);

            //     totalUninsuredPrice += uninsuredPrice;
            //     totalInsuredPrice += insuredPrice;
            // }
        }
     


   }
}
