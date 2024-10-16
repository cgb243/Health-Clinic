using System;
using API.Health.Clinic.Enterprise;
using Microsoft.AspNetCore.Mvc;
using Library.Clinic.Models;

namespace API.Health.Clinic.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController: ControllerBase
{
     private readonly ILogger<PatientController> _logger;

    public PatientController(ILogger<PatientController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<Patient> Get()
    {
       return new PatientEC().Patients;
    }

    [HttpGet("GetById")]
    public IEnumerable<Patient> GetById()
    {
       return new PatientEC().Patients;
    }

}


