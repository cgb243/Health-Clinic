using System;
using Library.Clinic.Models;



namespace Library.Clinic.Services;


public class AppointmentServiceProxy
{
    private static object _lock = new object();
       public static AppointmentServiceProxy Current
       {
           get
           {
               lock(_lock)
               {
                   if ( instance == null)
                   {
                       instance = new AppointmentServiceProxy();
                   }
               }
               return instance;
                  
           }
       }
       private static AppointmentServiceProxy? instance;

       private AppointmentServiceProxy()
       {
           //instance = null;

            var patients = PatientServiceProxy.Current.Patients;
            var physicians = PhysicianServiceProxy.Current.Physicians;
            var treatments = TreatmentServiceProxy.Current.Treatments;
            Appointments = new List<Appointment>
            {
                new Appointment
                {
                    Id = 1,
                    Title = "Check up",
                    StartTime = new DateTime(2024, 10, 9, 9, 0, 0), 
                    EndTime = new DateTime(2024, 10, 9, 9, 30, 0),  
                    patient = patients.FirstOrDefault(p => p.Id == 1),
                    physician = physicians.FirstOrDefault(p => p.Id == 1),
                    Treatments = new List<Treatment>()
                 
                    
                },
                new Appointment
                {
                    Id = 2,
                    Title = "Oral Exam",
                    StartTime = new DateTime(2024, 10, 9, 10, 0, 0), 
                    EndTime = new DateTime(2024, 10, 9, 10, 30, 0),  
                    patient = patients.FirstOrDefault(p => p.Id == 2),
                    physician = physicians.FirstOrDefault(p => p.Id == 2),
                    Treatments = new List<Treatment>()
                    
                }


            };
        
            var treatmentForCheckUp = treatments.FirstOrDefault(t => t.Id == 1); 
            var treatmentForOralExam1 = treatments.FirstOrDefault(t => t.Id == 2); 
            var treatmentForOralExam2 = treatments.FirstOrDefault(t => t.Id == 3); 
            var treatmentForOralExam3 = treatments.FirstOrDefault(t => t.Id == 6); 


            foreach (var appointment in Appointments)
            {
                appointment.CalculateTreatments();
            }

            if (treatmentForCheckUp != null)
            {
                Appointments[0].Treatments.Add(treatmentForCheckUp);
            }

            if (treatmentForOralExam1 != null)
            {
                Appointments[1].Treatments.Add(treatmentForOralExam1);
            }
           

            if (treatmentForOralExam2 != null)
            {
                Appointments[1].Treatments.Add(treatmentForOralExam2); 
            }
            if (treatmentForOralExam3 != null)
            {
                Appointments[1].Treatments.Add(treatmentForOralExam3); 
            }
       
       }


       public List<Appointment> Appointments {get; private set;} = new List<Appointment>();
      
      
       public int LastKey
       {
           get
           {
               if(Appointments.Any())
               {
                   return Appointments.Select(x => x.Id).Max();


               }
               return 0;
           }
       }


    public bool Conflicts(Appointment other)
    {
        if (!IsWithinBusinessHours(other.StartTime, other.EndTime))
        {
            return true; 
        }

        var relevantAppointments = Appointments
            .Where(appointment => appointment.Id != other.Id && 
                                (appointment.PatientId == other.PatientId || appointment.PhysicianId == other.PhysicianId));

        foreach (var appointment in relevantAppointments)
        {
            if (appointment.StartTime < other.EndTime && appointment.EndTime > other.StartTime)
            {
                return true; 
            }
        }
        return false; 
    }

    public Appointment? GetAppointmentById(int appointmentId)
    {
        return Appointments.FirstOrDefault(p => p.Id == appointmentId);
    }


    private bool IsWithinBusinessHours(DateTime? startTime, DateTime? endTime)
    {
        if (startTime < DateTime.Now)
        {
            return false;
        }
        if (startTime?.DayOfWeek == DayOfWeek.Saturday || startTime?.DayOfWeek == DayOfWeek.Sunday)
        {
            return false;
        }
        if (startTime?.TimeOfDay < TimeSpan.FromHours(8) || endTime?.TimeOfDay > TimeSpan.FromHours(17))
        {
            return false;
        }

        return true;
    }

    public async Task<bool> AddOrUpdateAppointment(Appointment appointment)
    {
        if (appointment == null)
            return false;

        bool hasConflict = await Task.Run(() => Conflicts(appointment));

        if (hasConflict)
        {
            return false;
        }
        else
        {
            if (appointment.Id > 0)
            {
                var existingAppointment = Appointments.FirstOrDefault(a => a.Id == appointment.Id);
                if (existingAppointment != null)
                {
                    existingAppointment.Title = appointment.Title;
                    existingAppointment.StartTime = appointment.StartTime;
                    existingAppointment.EndTime = appointment.EndTime;
                    existingAppointment.PatientId = appointment.PatientId;
                    existingAppointment.PhysicianId = appointment.PhysicianId;
                    existingAppointment.patient = appointment.patient;
                    existingAppointment.physician = appointment.physician;
                }
                else
                {
                    appointment.Id = LastKey + 1;
                    Appointments.Add(appointment);
                }
            }
            else
            {
                appointment.Id = LastKey + 1;
                Appointments.Add(appointment);
            }

            return true;
        }
    }



       public void DeleteAppointment(int id) {
           var appointmentToRemove = Appointments.FirstOrDefault(p => p.Id == id);
           if (appointmentToRemove != null)
           {
               Appointments.Remove(appointmentToRemove);
           }
       }
}
