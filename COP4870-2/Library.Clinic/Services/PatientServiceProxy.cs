using Library.Clinic.DTO;
using Library.Clinic.Models;
using Newtonsoft.Json;
using PP.Library.Utilities;
//using Library.Clinic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace Library.Clinic.Services
{
    public class PatientServiceProxy
    {
        private static object _lock = new object();
        public static PatientServiceProxy Current
        {
            get
            {
                lock(_lock)
                {
                    if (instance == null)
                    {
                        instance = new PatientServiceProxy();
                    }
                }
                return instance;
            }
        }

        private static PatientServiceProxy? instance;
        private PatientServiceProxy()
        {
            instance = null;

            //var patientsData = new WebRequestHandler().Get("/Patient").Result;

            //Patients = JsonConvert.DeserializeObject<List<PatientDTO>>(patientsData) ?? new List<PatientDTO>();
            //Uncomment when I work on API Service

            var insuranceService = InsuranceServiceProxy.Current;
            Patients = new List<Patient>
            {  
                new Patient
                {
                    Id = 1,
                    Name = "John Doe",
                    Birthday = new DateTime(1985, 5, 14),
                    Address = "123 Maple Street, Springfield",
                    SSN = "123-45-6789",
                    Diagnoses = "Hypertension, Allergies",
                    Prescriptions = "Lisinopril, Cetirizine",
                    InsurancePlan = insuranceService.GetInsuranceById(1)
                },
                new Patient
                {
                    Id = 2,
                    Name = "Jane Smith",
                    Birthday = new DateTime(1990, 7, 22),
                    Address = "456 Oak Avenue, Riverside",
                    SSN = "987-65-4321",
                    Diagnoses = "Asthma, Migraine",
                    Prescriptions = "Albuterol, Sumatriptan",
                    InsurancePlan = insuranceService.GetInsuranceById(2)
                },
                new Patient
                {
                    Id = 3,
                    Name = "Emily Johnson",
                    Birthday = new DateTime(2001, 3, 5),
                    Address = "789 Pine Road, Lakewood",
                    SSN = "111-22-3333",
                    Diagnoses = "Diabetes Type 2",
                    Prescriptions = "Metformin, Insulin",
                    InsurancePlan = insuranceService.GetInsuranceById(3)
                },
                new Patient
                {
                    Id = 4,
                    Name = "Michael Brown",
                    Birthday = new DateTime(1978, 11, 19),
                    Address = "101 Cedar Lane, Brookville",
                    SSN = "444-55-6666",
                    Diagnoses = "Cholesterol, Arthritis",
                    Prescriptions = "Atorvastatin, Ibuprofen",
                    InsurancePlan = insuranceService.GetInsuranceById(4)
                },
                new Patient
                {
                    Id = 5,
                    Name = "Sarah Davis",
                    Birthday = new DateTime(1963, 2, 28),
                    Address = "321 Elm Court, Greenfield",
                    SSN = "777-88-9999",
                    Diagnoses = "Osteoporosis, Anxiety",
                    Prescriptions = "Alendronate, Sertraline",
                    InsurancePlan = insuranceService.GetInsuranceById(5)
                }
            };
        }
        public int LastKey
        {
            get
            {
                if(Patients.Any())
                {
                    return Patients.Select(x => x.Id).Max();
                }
                return 0;
            }
        }

        private List<Patient> patients; 
        public List<Patient> Patients { 
            get {
                return patients;
            }

            private set
            {
                if (patients != value)
                {
                    patients = value;
                }
            }
        }

        public /*async  Task<Patient?>*/void AddOrUpdatePatient(Patient patient)
        {
            bool isAdd = false;
            if (patient.Id <= 0)
            {
                patient.Id = LastKey + 1;
                isAdd = true;
            }
            if (isAdd)
            {
                Patients.Add(patient);
            }
            // var payload = await new WebRequestHandler().Post("/patient", patient);
            // var newPatient = JsonConvert.DeserializeObject<PatientDTO>(payload);
            // if(newPatient != null && newPatient.Id > 0 && patient.Id == 0)
            // {
            //     //new patient to be added to the list
            //     Patients.Add(newPatient);
            // } else if(newPatient != null && patient != null && patient.Id > 0 && patient.Id == newPatient.Id)
            // {
            //     //edit, exchange the object in the list
            //     var currentPatient = Patients.FirstOrDefault(p => p.Id == newPatient.Id);
            //     var index = Patients.Count;
            //     if (currentPatient != null)
            //     {
            //         index = Patients.IndexOf(currentPatient);
            //         Patients.RemoveAt(index);
            //     }
            //     Patients.Insert(index, newPatient);
            // }
        }

        public /*async*/ void DeletePatient(int id) {
            var patientToRemove = Patients.FirstOrDefault(p => p.Id == id);

            if (patientToRemove != null)
            {
                Patients.Remove(patientToRemove);
                // await new WebRequestHandler().Delete($"/Patient/{id}");
            }
        }

        
    }
}