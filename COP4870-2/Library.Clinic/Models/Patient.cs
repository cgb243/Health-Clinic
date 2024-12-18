using System;
using System.Security.Cryptography.X509Certificates;
using Library.Clinic.DTO;


namespace Library.Clinic.Models
{
   public class Patient
   {
       public override string ToString()
       {
           return Display;
       }

       public string Display
       {
        get
        {
            return $"[{Id}] {Name}";
        }
       }
       public int Id {get; set;}
       private string? name;
      
       public string Name {
           get {
               return name ?? string.Empty;
           }
           set {
               name = value;
           }
       }
       public DateTime Birthday { get; set; }
       public string Address { get; set; }
       public string SSN { get; set; }
       public string Diagnoses {get; set;}
       public string Prescriptions {get; set;}
       public Insurance InsurancePlan{get; set;}


       public Patient()
       {
           Name = string.Empty;
           Address = string.Empty;
           Birthday = DateTime.MinValue;
           SSN = string.Empty;
           Diagnoses = string.Empty;
           Prescriptions = string.Empty;
       } 

       public Patient(PatientDTO p)
       {
            Id = p.Id;
            Name=p.Name;
            Birthday=p.Birthday;
            Address=p.Address;
            SSN=p.SSN;
            Diagnoses=p.Diagnoses;
            Prescriptions=p.Prescriptions;
            InsurancePlan=p.InsurancePlan;
       }
    }
}
