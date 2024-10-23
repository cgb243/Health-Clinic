using System;
using Library.Clinic.Models;


namespace Library.Clinic.Services
{
    public class PhysicianServiceProxy
    {
        private static object _lock = new object();
        public static PhysicianServiceProxy Current
        {
            get
            {
                lock(_lock)
                {
                    if (instance == null)
                    {
                        instance = new PhysicianServiceProxy();
                    }
                }
                return instance;
            }
        }

        private static PhysicianServiceProxy? instance;
        private PhysicianServiceProxy()
        {
            instance = null;


            Physicians = new List<Physician>
            {  
                new Physician
                {
                    Id = 1,
                    Name = "Dr. Jack Doedk",
                    licenseNumber = "ABC123456",
                    graduationDate = new DateTime(2010, 6, 15),
                    Specializations = "General Medicine"
                },
                new Physician
                {
                    Id = 2,
                    Name = "Dr. John Doep",
                    licenseNumber = "DEF789012",
                    graduationDate = new DateTime(2012, 7, 22),
                    Specializations = "Dentistry"
                },
                new Physician
                {
                    Id = 3,
                    Name = "Dr. Emily Black",
                    licenseNumber = "GHI345678",
                    graduationDate = new DateTime(2008, 5, 10),
                    Specializations = "Cardiology"
                },
                new Physician
                {
                    Id = 4,
                    Name = "Dr. Sarah White",
                    licenseNumber = "JKL901234",
                    graduationDate = new DateTime(2014, 8, 3),
                    Specializations = "Pediatrics"
                },
                new Physician
                {
                    Id = 5,
                    Name = "Dr. Michael Green",
                    licenseNumber = "MNO567890",
                    graduationDate = new DateTime(2011, 9, 30),
                    Specializations = "Orthopedics"
                }
            };
        }
        public int LastKey
        {
            get
            {
                if(Physicians.Any())
                {
                    return Physicians.Select(x => x.Id).Max();
                }
                return 0;
            }
        }

        public List<Physician> Physicians { get; private set;}

        public void AddOrUpdatePhysician(Physician physician)
        {
            bool isAdd = false;
            if (physician.Id <= 0)
            {
                physician.Id = LastKey + 1;
                isAdd = true;
            }
            if (isAdd)
            {
                Physicians.Add(physician);
            }
        
        }

        public void DeletePhysician(int id) {
            var physiciansToRemove = Physicians.FirstOrDefault(p => p.Id == id);

            if (physiciansToRemove != null)
            {
                Physicians.Remove(physiciansToRemove);
            }
        }
    }
}