using System;
using API.Health.Clinic.Database;
using Library.Clinic.Models;


namespace API.Health.Clinic.Enterprise;

public class PatientEC
{
    public PatientEC() {}

    public IEnumerable<Patient> Patients
    {
        get
        {
            return FakeDatabase.Patients;
        }
    }
}
