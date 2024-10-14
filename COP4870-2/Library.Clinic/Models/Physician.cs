using System;


namespace Library.Clinic.Models
{
   public class Physician
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

