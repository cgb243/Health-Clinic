using System;


namespace Library.Clinic.Models
{
   public class Physician
   {
       public int Id {get; set;}
      
       public string Name {get; set;}
       public DateTime graduationDate { get; set; }
       public string licenseNumber { get; set; }
       public string Specializations { get; set; }

       public Physician ()
       {
            Name = string.Empty;
            licenseNumber = string.Empty;
            graduationDate = DateTime.MinValue;
            Specializations = string.Empty;
       }
   }
}

