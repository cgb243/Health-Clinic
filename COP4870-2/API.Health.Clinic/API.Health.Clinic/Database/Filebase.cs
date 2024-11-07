using Library.Clinic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ToDoApplication.Persistence
{
    public class Filebase
    {
        private string _root;
        private string _patientRoot;
        private Filebase _instance;


        public Filebase Current
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new Filebase();
                }

                return _instance;
            }
        }

        private Filebase()
        {
            _root = "C:\temp";//change for mac
            _patientRoot=$"{_root}\\Patients";
            
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

        public Patient AddOrUpdate(Patient patient)
        {
            //set up a new Id if one doesn't already exist
            if(patient.Id <= 0)
            {
                patient.Id = LastKey + 1;
            }

            //go to the right place]
            string path = $"{_patientRoot}\\{patient.Id}.json";

            //if the item has been previously persisted
            if(File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }

            //write the file
            File.WriteAllText(path, JsonConvert.SerializeObject(patient));

            //return the item, which now has an id
            return patient;
        }

        public List<Patient> Patients
        {
            get
            {
                var root = new DirectoryInfo(_patientRoot);
                var _patients = new List<Patient>();
                foreach(var patientFile in root.GetFiles())
                {
                    var patient = JsonConvert.DeserializeObject<Patient>(File.ReadAllText(patientFile.FullName));
                    if(patient != null)
                    {
                        _patients.Add(patient);
                    }
                   
                }
                return _patients;
            }
        }

        // public List<Appointment> Appointments
        // {
        //     get
        //     {
        //         var root = new DirectoryInfo(_appointmentRoot);
        //         var _apps = new List<Appointment>();
        //         foreach (var appFile in root.GetFiles())
        //         {
        //             var app = JsonConvert.DeserializeObject<Appointment>(File.ReadAllText(appFile.FullName));
        //             _apps.Add(app);
        //         }
        //         return _apps;
        //     }
        // }

        public bool Delete(string type, string id)
        {
            //TODO: refer to AddOrUpdate for an idea of how you can implement this.
            return true;
        }
    }


    // ------------------- FAKE MODEL FILES, REPLACE THESE WITH A REFERENCE TO YOUR MODELS -------- //
    // public class Item
    // {
    //     public string Id { get; set; }
    // }

    // public class ToDo : Item
    // {

    // }

    // public class Appointment : Item
    // {

    // }
}
