using System;
using Library.Clinic.Models;


namespace Library.Clinic.Services;

public class TreatmentServiceProxy
{
    private static object _lock = new object();
    public static TreatmentServiceProxy Current
    {
        get
        {
            lock(_lock)
            {
                if (instance == null)
                {
                    instance = new TreatmentServiceProxy();
                }
            }
            return instance;
        }
    }

    private static TreatmentServiceProxy? instance;

    private List<Treatment> treatments;

    public List<Treatment> Treatments { 
        get {
            return treatments;
        }

        private set
        {
            if (treatments != value)
            {
                treatments = value;
            }
        }
    }
    private TreatmentServiceProxy()
    {
        instance = null;

        Treatments = new List<Treatment>
        {
             new Treatment
                {
                    Title = "General Consultation",
                    Description = "A basic health check-up and consultation.",
                    uninsuredPrice = 100.00m,
                    Id = 1
                },
                new Treatment
                {
                    Title = "Dental Cleaning",
                    Description = "Professional cleaning and polishing of teeth.",
                    uninsuredPrice = 150.00m,
                    Id = 2
                },
                new Treatment
                {
                    Title = "X-Ray Examination",
                    Description = "Radiographic imaging for diagnostic purposes.",
                    uninsuredPrice = 200.00m,
                    Id = 3
                },
                new Treatment
                {
                    Title = "Physical Therapy Session",
                    Description = "One-hour session focused on rehabilitation exercises.",
                    uninsuredPrice = 120.00m,
                    Id = 4
                },
                new Treatment
                {
                    Title = "Blood Test",
                    Description = "Comprehensive blood analysis including CBC and metabolic panel.",
                    uninsuredPrice = 80.00m,
                    Id = 5
                },
                new Treatment
                {
                    Title = "MRI Scan",
                    Description = "Magnetic Resonance Imaging for detailed internal images.",
                    uninsuredPrice = 800.00m,
                    Id = 6
                },
                new Treatment
                {
                    Title = "Vaccination",
                    Description = "Administration of vaccines to prevent diseases.",
                    uninsuredPrice = 50.00m,
                    Id = 7
                },
                new Treatment
                {
                    Title = "Allergy Testing",
                    Description = "Tests to identify specific allergens causing reactions.",
                    uninsuredPrice = 250.00m,
                    Id = 8
                },
                new Treatment
                {
                    Title = "Chiropractic Adjustment",
                    Description = "Spinal manipulation to improve physical function.",
                    uninsuredPrice = 90.00m,
                    Id = 9
                },
                new Treatment
                {
                    Title = "Eye Examination",
                    Description = "Comprehensive vision and eye health evaluation.",
                    uninsuredPrice = 75.00m,
                    Id = 10
                }
        };

    }

    public Treatment? GetTreatmentById(int TreatmentId)
    {
        return Treatments.FirstOrDefault(i => i.Id == TreatmentId);
    }
}
