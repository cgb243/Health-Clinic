using System;
using Library.Clinic.Services;
using System;
using System.Collections.Generic;
using Library.Clinic.Models;
namespace FirstProgram
{
   internal class Program
   {
       static void Main(string[] args)
       {
        
           bool isContinue = true;


           do
           {
               Console.WriteLine("Select an option");


               Console.WriteLine("A. Add a patient");
               Console.WriteLine("C. Add a physician");
               Console.WriteLine("T. Add an appointment");


               Console.WriteLine("D. Delete a patient");
               Console.WriteLine("W. Delete a physician");
               Console.WriteLine("I. Delete an appointment");
          
               Console.WriteLine("Q. Quit Program");
      
               string input = Console.ReadLine() ?? string.Empty;
              
               if (char.TryParse(input, out char choice))
               {
                   switch (choice)
                   {
                       case 'a':
                       case 'A':
                           patientCreation();
                           break;
                       case 'c':
                       case 'C':
                           physicianCreation();
                           break;
                       case 't':
                       case 'T':
                           appointmentCreation();
                           break;
                       case 'd':
                       case 'D':
                           Console.WriteLine("Enter patient ID you want to delete: ");
                           PatientServiceProxy.Current.Patients.ForEach(x => Console.WriteLine( $"{x.Id}. {x.Name}"));
                           int selectedPatient = int.Parse(Console.ReadLine() ?? "-1");
                           PatientServiceProxy.Current.DeletePatient(selectedPatient);
                           Console.WriteLine("Patient Removed!");
                           break;


                       case 'w':
                       case 'W':
                           Console.WriteLine("Enter Physician ID you want to delete: ");
                           PhysicianServiceProxy.Current.Physicians.ForEach(x => Console.WriteLine( $"{x.Id}. {x.Name}"));
                           int selectedPhysician= int.Parse(Console.ReadLine() ?? "-1");
                           PhysicianServiceProxy.Current.DeletePhysician(selectedPhysician);
                           Console.WriteLine("Physician Removed!");
                           break;


                       case 'i':
                       case 'I':
                           Console.WriteLine("Enter Appointment ID you want to delete: ");
                           AppointmentServiceProxy.Current.Appointments.ForEach(x => Console.WriteLine( $"{x.Id}. {x.Title}"));
                           int selectedAppointment = int.Parse(Console.ReadLine() ?? "-1");
                           AppointmentServiceProxy.Current.DeleteAppointment(selectedAppointment);
                           Console.WriteLine("Appointment Removed!");
                           break;


                       case 'q':
                       case 'Q':
                           isContinue = false;
                           break;


                       default:
                           Console.WriteLine("That is an invalid choice");
                           break;
                   }
               }
               else
               {
                   Console.WriteLine($"{choice} is not a char");
  
               }
           }while (isContinue);


           Console.WriteLine("Patients: ");
           foreach (var patient in PatientServiceProxy.Current.Patients)
           {
               Console.WriteLine($"Name: {patient.Name}");
           }
           Console.WriteLine("Physicians: ");
           foreach (var physician in PhysicianServiceProxy.Current.Physicians)
           {
               Console.WriteLine($"Name: {physician.Name}");
           }
           Console.WriteLine("Appointments: ");
           foreach (var appointment in AppointmentServiceProxy.Current.Appointments)
           {
               Console.WriteLine($"Title: {appointment.Title} Starts: {appointment.StartTime} Ends: {appointment.EndTime} Patient: {appointment.patient.Name} Physician: {appointment.physician.Name}");
           }
          
       }
 
       static void patientCreation()
       {
           Console.WriteLine("Enter Patient Name: ");
           var patientName = Console.ReadLine() ?? string.Empty;


           Console.WriteLine("Enter Patient Address: ");
           var patientAddress = Console.ReadLine() ?? string.Empty;


           Console.WriteLine("Enter Patient Birthday (e.g., MM/dd/yyyy): ");
           var birthdayInput = Console.ReadLine();
           DateTime patientBirthday;
           if (!DateTime.TryParse(birthdayInput, out patientBirthday))
           {
               Console.WriteLine("Invalid date format. Setting birthday to default value.");
               patientBirthday = DateTime.MinValue;
           }
           Console.WriteLine("Enter Patient SSN: ");
           var patientSSN = Console.ReadLine() ?? string.Empty;
           Console.WriteLine("Enter Patient Diagnoses: ");
           var patientDiagnoses = Console.ReadLine() ?? string.Empty;
           Console.WriteLine("Enter Patient Prescriptions: ");
           var patientPrescriptions = Console.ReadLine() ?? string.Empty;


           var newPatient = new Patient
           {
               Name = patientName,
               Address = patientAddress,
               Birthday = patientBirthday,
               SSN = patientSSN,
               Diagnoses= patientDiagnoses,
               Prescriptions= patientPrescriptions
           };
           PatientServiceProxy.Current.AddPatient(newPatient);
           Console.WriteLine("Patient Added!");
       }
      
       static void physicianCreation()
       {
           Console.WriteLine("Enter Physician Name: ");
           var physicianName = Console.ReadLine() ?? string.Empty;


           Console.WriteLine("Enter Physician License Number: ");
           var physicianLicenseNumber = Console.ReadLine() ?? string.Empty;


           Console.WriteLine("Enter Physician Graduation Date (e.g., MM/dd/yyyy): ");
           var graduationDateInput = Console.ReadLine();
           DateTime physicianGraduationDate;
           if (!DateTime.TryParse(graduationDateInput, out physicianGraduationDate))
           {
               Console.WriteLine("Invalid date format. Setting graduation date to default value.");
               physicianGraduationDate = DateTime.MinValue;
           }


           Console.WriteLine("Enter Physician Specializations (comma-separated): ");
           var physicianSpecializations = Console.ReadLine() ?? string.Empty;


           var newPhysician = new Physician
           {
               Name = physicianName,
               licenseNumber = physicianLicenseNumber,
               graduationDate = physicianGraduationDate,
               Specializations = physicianSpecializations
           };


           PhysicianServiceProxy.Current.AddPhysician(newPhysician);
           Console.WriteLine("Physician Added!");


       }
      
      
       static void appointmentCreation()
       {
           // appointment details
           Console.WriteLine("Enter Appointment Title: ");
           var appointmentName = Console.ReadLine();
           Console.WriteLine("Enter Appointment Location: ");
           var appointmentLocation = Console.ReadLine();
           Console.WriteLine("Enter Appointment Description: ");
           var appointmentDescription = Console.ReadLine();
          
           // appointment start time
           Console.WriteLine("Please enter the appointment start date and time (e.g., MM/dd/yyyy HH:mm):");
           var appointmentTime = Console.ReadLine();
           DateTime appointmentStartTime;
           if (!DateTime.TryParse(appointmentTime, out appointmentStartTime))
           {
               Console.WriteLine("Invalid date and time format.");
               return;
           }


           // appointment length
           Console.WriteLine("Enter the length of the appointment (e.g., HH:mm): ");
           var appointmentlength = Console.ReadLine();
           TimeSpan appointmentLength;
           if (!TimeSpan.TryParse(appointmentlength, out appointmentLength))
           {
               Console.WriteLine("Invalid time format.");
               return;
           }


           //Calculate the end time
           DateTime appointmentEndTime = appointmentStartTime.Add(appointmentLength);


           //Select a patient
           Console.WriteLine("Enter patient ID for the appointment: ");
           PatientServiceProxy.Current.Patients.ForEach(x => Console.WriteLine($"{x.Id}. {x.Name}"));
           int selectedPatient = int.Parse(Console.ReadLine() ?? "-1");
           var patient = PatientServiceProxy.Current.Patients.FirstOrDefault(p => p.Id == selectedPatient);
           if (patient == null)
           {
               Console.WriteLine("Invalid patient ID.");
               return;
           }


           //Select a physician
           Console.WriteLine("Enter Physician ID for the appointment: ");
           PhysicianServiceProxy.Current.Physicians.ForEach(x => Console.WriteLine($"{x.Id}. {x.Name}"));
           int selectedPhysician = int.Parse(Console.ReadLine() ?? "-1");
           var physician = PhysicianServiceProxy.Current.Physicians.FirstOrDefault(p => p.Id == selectedPhysician);
           if (physician == null)
           {
               Console.WriteLine("Invalid physician ID.");
               return;
           }


           //Create the new appointment
           var newAppointment = new Appointment
           {
               Title = appointmentName ?? string.Empty,
               Location = appointmentLocation ?? string.Empty,
               Description = appointmentDescription ?? string.Empty,
               StartTime = appointmentStartTime,
               EndTime = appointmentEndTime,
               patient = patient,
               physician = physician
           };


           //Adding the appointment
           if (AppointmentServiceProxy.Current.AddAppointment(newAppointment))
           {
               Console.WriteLine("Appointment Added!");
           }
           else
           {
               Console.WriteLine("Appointment has conflicts or outside of Business hours. Failed to add Appointment.");
           }
       }


   }
}

