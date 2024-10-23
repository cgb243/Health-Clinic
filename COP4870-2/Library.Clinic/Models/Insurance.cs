using System;

namespace Library.Clinic.Models;


public class InsuranceTreatmentPrice
{
    public Treatment Treatment { get; set; }
    public decimal CoveredPrice { get; set; }
}
public class Insurance
{

    public override string ToString()
    {
        return Display;
    }

    public string Display
    {
        get
        {
            return $"[{insuranceId}] {Title}";
        }
    }
    public string Title {get; set;}

    public int insuranceId {get; set;}

    public List<Patient> Patients {get; set;}

    public List<InsuranceTreatmentPrice> TreatmentPrices { get; set; }

    public decimal CoveragePercentage { get; set; }

    public decimal Deductible { get; set; }
    public decimal CoPay { get; set; }

    public decimal GetInsuredPrice(Treatment treatment)
    {
        return treatment.uninsuredPrice * (1 - CoveragePercentage);
    }

   
}
