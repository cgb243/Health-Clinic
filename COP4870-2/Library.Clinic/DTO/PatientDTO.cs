using System;
using Library.Clinic.Models;


namespace Library.Clinic.DTO;

public class PatientDTO
{
    public override string ToString()
    {
        return Display;
    }

    public string Display//remove and put on a view model
    {
        get
        {
            return $"[{Id}] {Name}";
        }
    }
    public int Id {get; set;}
    private string? name;
    public string Name {get; set;}
    public DateTime Birthday { get; set; }
    public string? Address { get; set; }
    public string? SSN { get; set; }
    public string Diagnoses {get; set;}
    public string Prescriptions {get; set;}
    public Insurance InsurancePlan{get; set;}
}
