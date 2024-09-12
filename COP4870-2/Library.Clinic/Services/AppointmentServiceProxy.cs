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
           instance = null;
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


       public bool Conflicts (Appointment other) {


           DateTime now = DateTime.Now;


           var relevantAppointments = AppointmentServiceProxy.Current.Appointments
           .Where(appointment => appointment.patient == other.patient || appointment.physician == other.physician);


           if (!IsWithinBusinessHours(other.StartTime, other.EndTime))
           {
               return false;
           }


           foreach (var appointment in relevantAppointments)
           {
               if (appointment.StartTime < other.EndTime && appointment.EndTime > other.StartTime)
               {
                   return false;
               }
           }
           return true;
          
           bool IsWithinBusinessHours(DateTime startTime, DateTime endTime)
           {
               if (startTime < now)
               {
                   return false;
               }
               if (startTime.DayOfWeek == DayOfWeek.Saturday || startTime.DayOfWeek == DayOfWeek.Sunday)
               {
                   return false;
               }
               if (startTime.TimeOfDay < TimeSpan.FromHours(8) || endTime.TimeOfDay > TimeSpan.FromHours(17))
               {
                   return false;
               }


               return true;
           }
       }
       public bool AddAppointment(Appointment appointment) {
           if (Conflicts(appointment))
           {
               if (appointment.Id <=0)
               {
                   appointment.Id = LastKey+1;
               }
               Appointments.Add(appointment);
               return true;
           }
           return false;
       }


       public void DeleteAppointment(int id) {
           var appointmentToRemove = Appointments.FirstOrDefault(p => p.Id == id);
           if (appointmentToRemove != null)
           {
               Appointments.Remove(appointmentToRemove);
           }
       }
}
