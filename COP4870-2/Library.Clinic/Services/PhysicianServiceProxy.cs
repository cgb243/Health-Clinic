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
                new Physician{Id = 1, Name = "Jack Doedk"}
                , new Physician{Id = 2, Name = "John Doep"}
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