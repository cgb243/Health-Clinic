using System;
using API.Health.Clinic.Controllers;
using Library.Clinic.Models;


namespace API.Health.Clinic.Database;

static public class FakeDatabase
{
    public static IEnumerable<Patient> Patients {
        get
        {
            return new List<Patient>
            {  
                new Patient {Id = 1, Name = "John Doe"}
                , new Patient{Id = 2, Name = "Jane Doe"}
            };
        }
    }
}
