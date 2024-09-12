using System;


namespace Library.Clinic.Models
{
   public class Patient
   {
       public override string ToString()
       {
           return $"[{Id}] {Name}";
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


       public Patient()
       {
           Name = string.Empty;
           Address = string.Empty;
           Birthday = DateTime.MinValue;
           SSN = string.Empty;
           Diagnoses = string.Empty;
           Prescriptions = string.Empty;
       } 
    }
}
