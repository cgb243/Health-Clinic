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
            new Insurance{Title="Basic Insurance", insuranceId = 1}
        };

    }
}