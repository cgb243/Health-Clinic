using System;
using System.ComponentModel;
using Library.Clinic.Models;


namespace Library.Clinic.Services;

public class InsuranceServiceProxy
{

    private static object _lock = new object();
    public static InsuranceServiceProxy Current
    {
        get
        {
            lock(_lock)
            {
                if (instance == null)
                {
                    instance = new InsuranceServiceProxy();
                }
            }
            return instance;
        }
    }

    private static InsuranceServiceProxy? instance;

    private List<Insurance> insurances;

    public List<Insurance> Insurances { 
        get {
            return insurances;
        }

        private set
        {
            if (insurances != value)
            {
                insurances = value;
            }
        }
    }
    private InsuranceServiceProxy()
    {
        instance = null;

        Insurances = new List<Insurance>
        {
            new Insurance
            {
                Title = "Basic Health Plan",
                insuranceId = 1,
                CoveragePercentage = 0.5m 
            },
            new Insurance
            {
                Title = "Premium Health Plan",
                insuranceId = 2,
                CoveragePercentage = 0.9m 
            },
            new Insurance
            {
                Title = "Standard Health Plan",
                insuranceId = 3,
                CoveragePercentage = 0.8m 
            },
            new Insurance
            {
                Title = "Family Health Plan",
                insuranceId = 4,
                CoveragePercentage = 0.95m 
            },
            new Insurance
            {
                Title = "Senior Health Plan",
                insuranceId = 5,
                CoveragePercentage = 0.4m
            }

        };

    }

    public Insurance? GetInsuranceById(int InsuranceId)
    {
        return Insurances.FirstOrDefault(i => i.insuranceId == InsuranceId);
    }
}
